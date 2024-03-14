using BackEndASP.DTOs.ImageDTOs;
using BackEndASP.Interfaces;
using BackEndASP.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace BackEndASP.Services
{
    public class ImageService : IImageRepository
    {

        private readonly SystemDbContext _dbContext;

        public ImageService(SystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // salvar a imagem de usuário no banco de dados
        public async Task InsertImageForAUser(ImageUserInsertDTO dto, string userId)
        {
            User user = _dbContext.Users.AsNoTracking().FirstOrDefault(s => s.Id == userId)
                ?? throw new ArgumentException($"This id {userId} does not exist");

            // imagem aceitando apenas essas extensões
            string fileExtension = Path.GetExtension(dto.Image.FileName).ToLower();

            if (!MyImageExtensionAllowed.extensions.Contains(fileExtension))
            {
                throw new ArgumentException($"Only JPEG, JPG, and PNG images are allowed. Your image have {fileExtension} extension");
            }
            //

            // imagem com no max 2mb
            if (dto.Image.Length > 2 * 1024 * 1024) 
            {
                throw new ArgumentException("Image size must be less than 2MB.");
            }
            //

            using (var memoryStream = new MemoryStream())
            {
                await dto.Image.CopyToAsync(memoryStream);

                var imageEntity = new Image
                {
                    ImageData = memoryStream.ToArray(),
                    UserId = user.Id,
                };


                _dbContext.Images.Add(imageEntity);
                await _dbContext.SaveChangesAsync();

                user.ImageId = imageEntity.Id;
                _dbContext.Users.Update(user);
            }
        }


        // salvar a imagem de um building no banco de dados
        public async Task InsertImageForBuilding(ImageBuildingInsertDTO dto, string userId, int propertyId)
        {
            Owner user = await _dbContext.Owners.Include(o => o.Properties).OfType<Owner>().FirstOrDefaultAsync(s => s.Id == userId)
                ?? throw new ArgumentException($"User with id {userId} does not exist or is not an owner");

            // Check if the provided propertyId exists and belongs to the user
            Property property = user.Properties.FirstOrDefault(p => p.Id == propertyId);
            if (property == null)
            {
                throw new ArgumentException($"Property with id {propertyId} does not exist or does not belong to the user");
            }

            foreach (var imageFile in dto.Images)
            {
                // Validate image file extension
                string fileExtension = Path.GetExtension(imageFile.FileName).ToLower();
                if (!MyImageExtensionAllowed.extensions.Contains(fileExtension))
                {
                    throw new ArgumentException($"Only JPEG, JPG, and PNG images are allowed. Your image has {fileExtension} extension");
                }

                // Validate image file size
                if (imageFile.Length > 2 * 1024 * 1024) // 2MB in bytes
                {
                    throw new ArgumentException("Image size must be less than 2MB.");
                }

                using (var memoryStream = new MemoryStream())
                {
                    await imageFile.CopyToAsync(memoryStream);

                    var imageEntity = new Image
                    {
                        ImageData = memoryStream.ToArray(),
                        BuildingId = property.Id
                    };

                    _dbContext.Images.Add(imageEntity);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}


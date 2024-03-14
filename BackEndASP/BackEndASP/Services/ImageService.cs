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

        // salvar a imagem no banco de dados
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
    }
}


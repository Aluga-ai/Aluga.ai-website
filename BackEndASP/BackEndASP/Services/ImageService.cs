using BackEndASP.DTOs.ImageDTOs;
using BackEndASP.Interfaces;
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


            using (var memoryStream = new MemoryStream())
            {
                await dto.Image.CopyToAsync(memoryStream);

                // Save image data to the database
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


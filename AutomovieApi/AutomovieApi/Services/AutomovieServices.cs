using AutoMapper;
using AutomovieApi.Entities;
using AutomovieApi.Models;
using AutomovieApi.Models.Post;
using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using AutomovieApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using RestSharp;
using System.Collections.Immutable;
using AutomovieApi.Models.Filters;
using static System.Net.Mime.MediaTypeNames;

namespace AutomovieApi.Services
{
    public class AutomovieServices : IAutomovieServices
    {
        private readonly PlatformDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly BlobServiceClient _blobServiceClient;

        public AutomovieServices(PlatformDbContext dbContext, IMapper mapper, BlobServiceClient blobServiceClient)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _blobServiceClient = blobServiceClient;
        }

        public async Task<(bool IsSuccess, string Message)> AddToFavAnn(string userId, int id)
        {
            var announcement = await _dbContext.Announcements
                .Where(a => a.AnId == id)
                .FirstOrDefaultAsync();

            if (announcement == null)
            {
                return (false, "Announcement does not exist.");
            }

            var existingFavorite = await _dbContext.FavoriteAnnouncements
                .Where(fa => fa.UserId == int.Parse(userId) && fa.FavoriteAnnouncementId == id)
                .FirstOrDefaultAsync();

            if (existingFavorite != null)
            {
                return (false, "This announcement is already in your favorites.");
            }

            var newFavorite = new FavoriteAnnouncements
            {
                UserId = int.Parse(userId),
                FavoriteAnnouncementId = id,
                AnnouncementAnId = id

            };

            _dbContext.FavoriteAnnouncements.Add(newFavorite);
            await _dbContext.SaveChangesAsync();

            return (true, "Announcement added to favorites successfully.");
        }


        public async Task<AnnouncementDto> GetById(int id)
        {
            var announcement = await _dbContext.Announcements
                .Include(a => a.FavoriteAnnouncements)
        .Include(a => a.User)
        .Include(a => a.Comments)
            .ThenInclude(c => c.User)
        .Include(a => a.Images)
        .Include(a => a.Multimedia)
            .ThenInclude(m => m.MultimediaDataset)
        .Include(a => a.DriverAssistanceSystems)
            .ThenInclude(d => d.DriverAssistanceSystemsDataset)
        .Include(a => a.Safety)
            .ThenInclude(s => s.SafetyDataset)
        .Include(a => a.Performance)
            .ThenInclude(p => p.PerformanceDataset)
        .Include(a => a.Other)
            .ThenInclude(o => o.OtherDataset).AsQueryable().AsNoTracking()
        .FirstOrDefaultAsync(a => a.AnId == id);

            if (announcement == null) return null;

            var announcementDto = _mapper.Map<AnnouncementDto>(announcement);
            announcementDto.LikedBy = _dbContext.FavoriteAnnouncements.Where(s => s.AnnouncementAnId == id).Select(fa => fa.UserId).ToList();

            return announcementDto;
        }
        
        public async Task<AnnouncementDto> GetBySlug(string slug)
        {
            var announcement = await _dbContext.Announcements
                .Include(a => a.FavoriteAnnouncements)
        .Include(a => a.User)
        .Include(a => a.Comments)
            .ThenInclude(c => c.User)
        .Include(a => a.Images)
        .Include(a => a.Multimedia)
            .ThenInclude(m => m.MultimediaDataset)
        .Include(a => a.DriverAssistanceSystems)
            .ThenInclude(d => d.DriverAssistanceSystemsDataset)
        .Include(a => a.Safety)
            .ThenInclude(s => s.SafetyDataset)
        .Include(a => a.Performance)
            .ThenInclude(p => p.PerformanceDataset)
        .Include(a => a.Other)
            .ThenInclude(o => o.OtherDataset).AsQueryable().AsNoTracking()
        .FirstOrDefaultAsync(a => a.Slug == slug);

            if (announcement == null) return null;

            var announcementDto = _mapper.Map<AnnouncementDto>(announcement);

            var id = announcement.AnId;
            announcementDto.LikedBy = _dbContext.FavoriteAnnouncements.Where(s => s.AnnouncementAnId == id).Select(fa => fa.UserId).ToList();

            return announcementDto;
        }

        public async Task<UserDto> GetUsrById(int id)
        {
            var user = await _dbContext.Users.Where(f => f.UserId == id).FirstOrDefaultAsync();
        
            if (user == null) return null;

            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }

        public async Task<List<CommentDto>> GetCommentsByAnnId(int id)
        {
            var com = await _dbContext.Comments.Where(f => f.AnId == id).Include(a => a.User).AsQueryable().AsNoTracking()
                .ToListAsync();

            if (com == null) return null;

            var comDto = _mapper.Map<List<CommentDto>>(com);

            return comDto;
        }public async Task<List<AnnPreview>> GetAnnByUsrId(int id)
        {
            var anns = await _dbContext.Announcements
                .Include(a => a.FavoriteAnnouncements)
                .Include(a => a.User)
                .Include(a => a.Images)
                .Where(f => f.UserId == id).AsQueryable().AsNoTracking()
                .ToListAsync();

            if (anns == null) return null;

            var projectedResults = anns.Select(a => new AnnPreview
            {
                Id = a.AnId,
                Slug = a.Slug,
                Brand = a.Brand,
                Model = a.Model,
                Description = a.Description,
                summary = a.Summary,
                User = new UserDto
                {
                    UserId = a.User.UserId,
                    Name = a.User.Name,
                    Surname = a.User.Surname,
                    Phone = a.User.Phone,
                    Email = a.User.Email,
                    lan = a.User.lat,
                    lng = a.User.lng,
                    Voivodeship = a.User.Voivodeship,
                    City = a.User.City,
                },
                Price = a.Price,
                Power = a.Power,
                Engine = a.Engine ?? string.Empty,
                FuelType = a.FuelType,
                Mileage = a.Mileage,
                ProductionYear = a.ProductionYear,
                LikedBy = _dbContext.FavoriteAnnouncements
                            .Where(fa => fa.AnnouncementAnId == a.AnId)
                            .Select(fa => fa.UserId)
                            .ToList(),
                Images = a.Images.Select(i => new AnnouncementImagesDto
                {
                    AnId = i.AnId,
                    ImageUrl = i.ImageUrl
                }).ToList()
            }).ToList();

            return projectedResults;
        }
        
        public async Task<List<FavoriteAnnouncementsDto>> GetFvAnnsByUsrId(int id)
        {
            var com = await _dbContext.FavoriteAnnouncements.Where(f => f.UserId == id).AsQueryable().AsNoTracking()
                .ToListAsync();

            if (com == null) return null;

            var comDto = _mapper.Map<List<FavoriteAnnouncementsDto>>(com);

            return comDto;
        }


        public async Task<Comment> CreateCom(CommentCreateRequest commentCreateRequest, string usr)
        {
            var comment = new Comment()
            {
                UserId = int.Parse(usr),
                AnId = commentCreateRequest.AnId,
                CommentText = commentCreateRequest.CommentText
            };

            _dbContext.Comments.Add(comment);
            await _dbContext.SaveChangesAsync();


            return comment;
        }

        public async Task<bool> DeleteCom(int commentId, int userId)
        {
            var comment = await _dbContext.Comments.FindAsync(commentId);

            if (comment == null || comment.UserId != userId)
            {
                return false;
            }

            _dbContext.Comments.Remove(comment);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAnnFromFavorites(int AnnId, int userId)
        {
            
                var fvann = await _dbContext.FavoriteAnnouncements
                                            .FirstOrDefaultAsync(f => f.AnnouncementAnId == AnnId && f.UserId == userId);

                if (fvann == null)
                {
                    throw new ArgumentException("The specified announcement was not found in the user's favorites.");
                }

                _dbContext.FavoriteAnnouncements.Remove(fvann);
                await _dbContext.SaveChangesAsync();

                return true;
            
        }


        public async Task<Announcement> CreateAnn(AnnouncementCreateRequest request, string usr)
        {
            var slug = await GenerateUniqueSlugAsync(request.Brand, request.Model);

            var _brand = _dbContext.Brands.Where(f => f.Name.ToLower().Replace(" ", "-") == request.Brand.ToLower().Replace(" ", "-")).First().Name;
            if(_brand == null)
            {
                throw new BrandNotFoundException("Nie ma takiej marki w bazie");
            }

            var _model = _dbContext.Models.Where(f => f.Name.ToLower().Replace(" ", "-") == request.Model.ToLower().Replace(" ", "-")).First().Name;
            if (_model == null)
            {
                throw new ModelNotFoundException($"Nie ma takiego modelu w bazie dla marki {_brand}");
            }

            string title = " ";

            if(request.summary == null || request.summary == "")
            {
                title = " ";
            }
            else
            {
                title = request.summary;
            }
            

            var announcement = new Announcement
            {
                Slug = slug,
                UserId = int.Parse(usr),
                Brand = _brand,
                Model = _model,
                ProductionYear = request.ProductionYear,
                FuelType = request.FuelType,
                Mileage = request.Mileage,
                Price = request.Price,
                Description = request.Description,
                BodyType = request.BodyType,
                Power = request.Power,
                Engine = request.Engine,
                City = _dbContext.Users.Where(f=>f.UserId == int.Parse(usr)).FirstOrDefault().City,
                Summary = title,
                lat = _dbContext.Users.Where(f => f.UserId == int.Parse(usr)).FirstOrDefault().lat,
                lng = _dbContext.Users.Where(f => f.UserId == int.Parse(usr)).FirstOrDefault().lng,
                Comments = new List<Comment>(),
                Images = new List<AnnouncementImages>(),
                Multimedia = new List<Multimedia>(),
                DriverAssistanceSystems = new List<DriverAssistanceSystems>(),
                Safety = new List<Safety>(),
                Performance = new List<Performance>(),
                Other = new List<Other>()
            };

            _dbContext.Announcements.Add(announcement);
            await _dbContext.SaveChangesAsync();

            if (request.MultimediaFeatures != null)
            {
                foreach (var _featureId in request.MultimediaFeatures)
                {
                    var multimedia = new Multimedia
                    {
                        AnId = announcement.AnId,
                        featureId = _featureId,
                        feature = _dbContext.MultimediaDataset.Where(f => f.Id == _featureId).Select(f => f.feature).First()
                    };
                    _dbContext.Multimedia.Add(multimedia);
                    announcement.Multimedia.Add(multimedia);
                }
            }

            
            if (request.DriverAssistanceSystemsFeatures != null)
            {
                foreach (var _featureId in request.DriverAssistanceSystemsFeatures)
                {
                    var driverAssistanceSystem = new DriverAssistanceSystems
                    {
                        AnId = announcement.AnId,
                        featureId = _featureId,
                        feature = _dbContext.DriverAssistanceSystemsDataset.Where(f => f.Id == _featureId).Select(f => f.feature).First()

                    };
                    _dbContext.DriverAssistanceSystems.Add(driverAssistanceSystem);
                    announcement.DriverAssistanceSystems.Add(driverAssistanceSystem);
                }
            }

            if (request.SafetyFeatures != null)
            {
                foreach (var _featureId in request.SafetyFeatures)
                {
                    var safety = new Safety
                    {
                        AnId = announcement.AnId,
                        featureId = _featureId,
                        feature = _dbContext.SafetyDataset.Where(f => f.Id == _featureId).Select(f => f.feature).First()

                    };
                    _dbContext.Safety.Add(safety);
                    announcement.Safety.Add(safety);
                }
            }

            if (request.PerformanceFeatures != null)
            {
                foreach (var _featureId in request.PerformanceFeatures)
                {
                    var performance = new Performance
                    {
                        AnId = announcement.AnId,
                        featureId = _featureId,
                        feature = _dbContext.PerformanceDataset.Where(f => f.Id == _featureId).Select(f => f.feature).First()

                    };
                    _dbContext.Performance.Add(performance);
                    announcement.Performance.Add(performance);
                }
            }

            if (request.OtherFeatures != null)
            {
                foreach (var _featureId in request.OtherFeatures)
                {
                    var other = new Other
                    {
                        AnId = announcement.AnId,
                        featureId = _featureId,
                        feature = _dbContext.OtherDataset.Where(f => f.Id == _featureId).Select(f => f.feature).First()
                    };
                    _dbContext.Other.Add(other);
                    announcement.Other.Add(other);
                }
            }

            int imageCount = 0;

            foreach (var file in request.Images)
            {
                if (file.Length > 0)
                {
                    imageCount++;

                    var fileName = $"{imageCount}-{announcement.Slug}{Path.GetExtension(file.FileName)}";

                    var blobContainerClient = _blobServiceClient.GetBlobContainerClient("automovieimages");

                    var blobClient = blobContainerClient.GetBlobClient(imageCount + "_"+ slug);
                    using (var stream = file.OpenReadStream())
                    {
                        await blobClient.UploadAsync(stream);
                    }
                    
                    var image = new AnnouncementImages   
                    {
                        AnId = announcement.AnId,
                        ImageUrl = blobClient.Uri.ToString()  
                    };
                    _dbContext.AnnouncementImages.Add(image);   
                    announcement.Images.Add(image);
                       
                }
            }

            await _dbContext.SaveChangesAsync();
            return announcement;
        }

        public async Task<bool> DeleteAnnouncementAsync(int announcementId, int userId)
        {

            var announcement = await _dbContext.Announcements
                .Include(a => a.Comments)
                .Include(a => a.Images)
                .Include(a => a.Multimedia)
                .Include(a => a.DriverAssistanceSystems)
                .Include(a => a.Safety)
                .Include(a => a.Performance)
                .Include(a => a.Other)
                .Include(a => a.FavoriteAnnouncements)
                .FirstOrDefaultAsync(a => a.AnId == announcementId && a.UserId == userId);

            if (announcement == null)
            {
                return false; 
            }

            _dbContext.Comments.RemoveRange(announcement.Comments);
            _dbContext.AnnouncementImages.RemoveRange(announcement.Images);
            _dbContext.FavoriteAnnouncements.RemoveRange(announcement.FavoriteAnnouncements);
            _dbContext.Multimedia.RemoveRange(announcement.Multimedia);
            _dbContext.DriverAssistanceSystems.RemoveRange(announcement.DriverAssistanceSystems);
            _dbContext.Safety.RemoveRange(announcement.Safety);
            _dbContext.Performance.RemoveRange(announcement.Performance);
            _dbContext.Other.RemoveRange(announcement.Other);

 
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient("automovieimages");
            foreach (var image in announcement.Images)
            {
                var blobUri = new Uri(image.ImageUrl);
                var blobClient = new BlobClient(blobUri);
                var blobName = blobClient.Name;

                var containerBlobClient = blobContainerClient.GetBlobClient(blobName);
                await containerBlobClient.DeleteIfExistsAsync();
            }


            _dbContext.Announcements.Remove(announcement);

            await _dbContext.SaveChangesAsync();

            return true;
        }
            


        public async Task<string> GenerateUniqueSlugAsync(string brand, string model, int suffixLength = 8)
        {
            string slug;

            do
            {
                var numericSuffix = GenerateNumericSuffix(suffixLength);
                slug = $"{brand}-{model}-{numericSuffix}".ToLower();
            } while (await _dbContext.Announcements.AnyAsync(a => a.Slug == slug));

            return slug;
        }

        public string GenerateNumericSuffix(int length = 8)
        {
            var random = new Random();
            var suffix = new char[length];

            for (int i = 0; i < length; i++)
            {
                suffix[i] = (char)('0' + random.Next(0, 10));
            }

            return new string(suffix);
        }







    }
}

using AutomovieApi.Entities;
using AutomovieApi.Models;
using AutomovieApi.Models.Filters;
using AutomovieApi.Models.Post;
using Microsoft.AspNetCore.Mvc;

namespace AutomovieApi.Services
{
    public interface IAutomovieServices
    {
        Task<(bool IsSuccess, string Message)> AddToFavAnn(string userId, int id);
        Task<AnnouncementDto> GetById(int id);
        Task<AnnouncementDto> GetBySlug(string slug);
        Task<UserDto> GetUsrById(int id);
        Task<List<CommentDto>> GetCommentsByAnnId(int id);
        Task<List<AnnPreview>> GetAnnByUsrId(int id);
        Task<List<FavoriteAnnouncementsDto>> GetFvAnnsByUsrId(int id);
        Task<Announcement> CreateAnn(AnnouncementCreateRequest request, string usr);
        Task<Comment> CreateCom(CommentCreateRequest request, string usr);
        Task<bool> DeleteCom (int commentId, int userId);
        Task<bool> DeleteAnnouncementAsync(int announcementId, int userId);
        Task<bool> DeleteAnnFromFavorites(int annId, int userId);
    }
}
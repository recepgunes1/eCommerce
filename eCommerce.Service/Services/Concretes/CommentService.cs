using AutoMapper;
using eCommerce.Data.UnitOfWorks;
using eCommerce.Entity.Entities;
using eCommerce.Entity.ViewModels.Comment;
using eCommerce.Service.Extensions;
using eCommerce.Service.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace eCommerce.Service.Services.Concretes
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ClaimsPrincipal _user;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContext)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            _user = httpContext.HttpContext.User;
        }
        public async Task BlockCommentAsync(Guid id)
        {
            var comment = await unitOfWork.GetRepository<Comment>().GetByGuidAsync(id);
            comment.IsVisible = false;
            await unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<CommentViewModel>> GetAllCommentsDeletedAsync()
        {
            var comments = await unitOfWork.GetRepository<Comment>().GetAllAsync(p => p.IsDeleted, p => p.Product, p => p.User);
            var mappedComents = mapper.Map<IEnumerable<CommentViewModel>>(comments);
            return mappedComents;
        }

        public async Task<IEnumerable<CommentViewModel>> GetAllCommentsNonDeletedAsync()
        {
            var comments = await unitOfWork.GetRepository<Comment>().GetAllAsync(p => !p.IsDeleted, p => p.Product, p => p.User);
            var mappedComents = mapper.Map<IEnumerable<CommentViewModel>>(comments);
            return mappedComents;
        }

        public async Task DeleteCommentAsync(Guid id)
        {
            var comment = await unitOfWork.GetRepository<Comment>().GetByGuidAsync(id);
            comment.IsDeleted = true;
            await unitOfWork.SaveAsync();
        }

        public async Task RestoreCommentAsync(Guid id)
        {
            var comment = await unitOfWork.GetRepository<Comment>().GetByGuidAsync(id);
            comment.IsDeleted = false;
            await unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<CommentViewModel>> GetAllCommentsToUserIdNonDeletedAsync(Guid id)
        {
            var comments = await unitOfWork.GetRepository<Comment>().GetAllAsync(p => !p.IsDeleted && p.User.Id == id, p => p.Product, p => p.User);
            var mappedComents = mapper.Map<IEnumerable<CommentViewModel>>(comments);
            return mappedComents;
        }

        public async Task AddCommentAsync(AddCommentViewModel viewModel)
        {
            viewModel.UserId = _user.GetLoggedInUserId();
            var mappedComment = mapper.Map<Comment>(viewModel);
            mappedComment.IsVisible = true;
            await unitOfWork.GetRepository<Comment>().AddAsync(mappedComment);
            await unitOfWork.SaveAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MJ.Common.DTO;
using MJ.Common.Entities;
using MJ.Services.IServices;

namespace MJ.Services
{
    public class PostService : IPostService
    {
        public bool AddPost(PostsDto postsDto)
        {
            bool result = false;

            try
            {
                using (var db = new MJEntities())
                {

                    Post newPost = FillNewPostEntity(db.Posts.Create(), postsDto);
                    db.Posts.Add(newPost);

                    PostDetail newPostDetail = FillNewPostDetailEntity(db.PostDetails.Create(), postsDto.PostDetailsDto);
                    db.PostDetails.Add(newPostDetail);

                    db.SaveChanges();

                    result = true;
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {

                Exception exception = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);

                        //create a new exception inserting the current one
                        //as the InnerException
                        exception = new InvalidOperationException(message, exception);
                    }
                }
                throw exception;
            }

            return result;
        }

        public bool CheckPostTitle(string postTitle)
        {
            bool result = false;

            try
            {
                using (var db = new MJEntities())
                {
                    var query = (from a in db.Posts
                                 where a.postTitle == postTitle
                                 select a).FirstOrDefault();

                    if (query != null)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }

            return result;
        }

        #region FILL

        private Post FillNewPostEntity(Post newPost, PostsDto postsDto)
        {
            newPost.postId = postsDto.PostId;
            newPost.postTitle = postsDto.PostTitle;
            newPost.postDateTime = postsDto.PostDateTime;
            newPost.postDeleted = postsDto.PostDeleted;
            newPost.userId = postsDto.UserId;

            return newPost;
        }

        private PostDetail FillNewPostDetailEntity(PostDetail newPostDetail, PostDetailsDto postDetailsDto)
        {
            newPostDetail.postDetailId = postDetailsDto.PostDetailId;
            newPostDetail.postId = postDetailsDto.PostId;
            newPostDetail.postText = postDetailsDto.PostText;
            newPostDetail.postImage = postDetailsDto.PostImage;

            return newPostDetail;
        }

        #endregion

    }
}

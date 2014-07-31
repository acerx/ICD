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
    public class ReadPostService : IReadPostService
    {
        public IEnumerable<PostsDto> GetAllPosts()
        {
            try
            {
                using (var db = new MJEntities())
                {
                    var postList = new List<PostsDto>();

                    var posts = from a in db.Posts
                                select a;

                    foreach (var item in posts)
                    {
                        postList.Add(BuildPostDto(item));
                    }

                    return postList;
                }
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }

            throw new NotImplementedException();
        }

        #region PRIVATE

        private PostsDto BuildPostDto(Post posts)
        {
            AutoMapper.Mapper.CreateMap<Post, PostsDto>();
            AutoMapper.Mapper.CreateMap<PostDetail, PostDetailsDto>();
            PostsDto postsDto = AutoMapper.Mapper.Map<Post, PostsDto>(posts);
            postsDto.PostDetailsDto = AutoMapper.Mapper.Map<PostDetail, PostDetailsDto>(posts.PostDetails.FirstOrDefault());

            return postsDto;
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly DBContext _context;

        public PostController(DBContext context)
        {
            _context = context;
        }

        [HttpGet("{pageSize}/{searchtext}/{PageIndex}")]
        
        public async Task<ActionResult> GetPosts(int pageSize, string searchtext,  int PageIndex)
        {
            var Data =   await _context.Posts.ToListAsync();
            if (!string.IsNullOrEmpty(searchtext))
            {
                
                var query = (from p in _context.Posts
                                //where p.Country_name == "Sushil"
                            where p.PostText.Contains(searchtext)
                            select p).ToList();
                Data = query;
            }
            List<returnOBj> ReturnData = new List<returnOBj>();
            foreach (var dd in Data)
            {
                var tempComent = await _context.Comments.Where(x => x.PostId == dd.Id).ToListAsync();
                returnOBj oreturnOBj = new returnOBj();
                oreturnOBj.PostText = dd.PostText;
                oreturnOBj.UserName = "Admin";
                oreturnOBj.Comment = "";
                oreturnOBj.Backcolorname = "red";
                oreturnOBj.numberOfComments = tempComent.Count.ToString() + "Comments";
                oreturnOBj.IssueDateSt = dd.IssueDate.ToString("dd/MM/yyyyy");
                ReturnData.Add(oreturnOBj);

                
                foreach (var cc in tempComent)
                {
                    var like = await _context.Votes.Where(x => x.CommentId==cc.Id && x.IsLike== true  ).ToListAsync();
                    var Dislike = await _context.Votes.Where(x => x.CommentId == cc.Id && x.IsLike == false).ToListAsync();
                    oreturnOBj = new returnOBj();
                    oreturnOBj.PostText = "";
                    oreturnOBj.UserName = _context.Users.Where(x => x.Id == cc.UserId).FirstOrDefault().UserName;
                    oreturnOBj.Comment = cc.Comment;
                    oreturnOBj.Backcolorname = "orange";
                    oreturnOBj.Likes = like.Count();
                    oreturnOBj.DistLikes = Dislike.Count();
                    oreturnOBj.IssueDateSt = cc.IssueDate.ToString("dd/MM/yyyyy");
                  //  oreturnOBj.numberOfComments = (like.Count() + " " + Dislike.Count()).ToString();
                    ReturnData.Add(oreturnOBj);
                }

            }

            
            var c = ReturnData.Count();
            int first = pageSize * PageIndex;
            if (first > c)
            {
                return null;
            }
            int last = first + pageSize;
            last = c < last ? c : last;
            var taken = ReturnData.Skip(first).Take(pageSize).ToList();
            return new OkObjectResult (new { length = c, data= taken });
        }



      
    }

    public class returnOBj
    {
        
        public string PostText { get; set; }
        public string IssueDateSt { get; set; }
        public string Comment{ get; set; }
        public string UserName { get; set; }
        public int Likes { get; set; }
        public int DistLikes { get; set; }
        public string Backcolorname { get; set; }
        public string numberOfComments { get; set; }
    }
}

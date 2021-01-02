using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Vote
    {
        Vote()
        {
            Id = 0;
            CommentId = 0;
            UserId = 0;
            IsLike = false;
        }
        [Key]
        public int Id { get; set; }
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public bool IsLike { get; set; }



        
    }
}

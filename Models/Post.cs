using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Post
    {
        Post()
        {
            Id = 0;
            PostText = "";
            IssueDate = DateTime.Today; //yy-mm-dd
            Comments = new List<UserComment>();

        }
        [Key]
        public int Id { get; set; }
        public string PostText { get; set; }
        public DateTime IssueDate { get; set; }
        public List<UserComment> Comments { get; set; }
        #region Derived variable


        public string ActiveDateSt
        {
            get
            {

                return this.IssueDate.ToString("MM/dd/yyyy");
            }
        }
        #endregion
    }
}

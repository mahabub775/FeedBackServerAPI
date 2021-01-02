using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class UserComment
    {
        UserComment()
        {
            Id = 0;
            PostId = 0;
            UserId = 0;
            Comment = "";
            IssueDate = DateTime.Today; //yy-mm-dd
        }
        [Key]
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public DateTime IssueDate { get; set; }

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

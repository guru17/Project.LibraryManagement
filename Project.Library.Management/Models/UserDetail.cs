using System;

namespace Project.Library.Management.Models
{
    public class UserDetail
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int Status { get; set; }
    }
}

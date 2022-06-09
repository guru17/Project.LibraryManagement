using System;

namespace Project.Library.Management.Models
{
    public class BookDetail
    {
        public int BookID { get; set; }
        public string BookName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int Status { get; set; }
    }
}

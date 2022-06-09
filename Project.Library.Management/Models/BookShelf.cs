using System;

namespace Project.Library.Management.Models
{
    public class BookShelf
    {
        public int RowID { get; set; }
        public int BookID { get; set; }
        public int UserID { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}

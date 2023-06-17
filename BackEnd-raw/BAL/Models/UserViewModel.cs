using DAL.Entities;

namespace BAL.Models
{
    public class UserViewModel
    {
        public long ID { get; set; }
        public string Fullname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public char Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int Status { get; set; }
        public long IdRole { get; set; }
    }
}


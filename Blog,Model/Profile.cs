using System.ComponentModel.DataAnnotations;

namespace Blog_Model
{

    public class Profile
    {
        [Key]
        public int ProfileID { get; set; }
        public string FullName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Email {  get; set; } = string.Empty;
        public string FaceBook { get; set; } = string.Empty;
        public string Reddit { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public Image? ProfileImage { get; set; }
    }
    public class ProfileResponse
    { 
        public string FullName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Email {  get; set; } = string.Empty;
        public string FaceBook { get; set; } = string.Empty;
        public string Reddit { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public ImageResponse? ProfileImage { get; set; }
    }
    public class ProfileRequest
    {
        public string FullName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Email {  get; set; } = string.Empty;
        public string FaceBook { get; set; } = string.Empty;
        public string Reddit { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public ImageRequest? ProfileImage { get; set; }

    }
}

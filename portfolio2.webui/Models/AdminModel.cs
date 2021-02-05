using System.ComponentModel.DataAnnotations;

namespace portfolio.webui.Models
{
    public class AdminModel
    {
        [Required]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
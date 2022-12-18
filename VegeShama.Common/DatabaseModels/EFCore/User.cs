using System.ComponentModel.DataAnnotations;

namespace VegeShama.Common.DatabaseModels.EFCore
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public byte Type { get; set; }

        [Required]
        public virtual Customer Customer { get; set; }
    }
}

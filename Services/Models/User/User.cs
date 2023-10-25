using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Models.User
{
	public class User
	{

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		public string? Name { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public string? PhoneNumber { get; set; }
		public string Email { get; set; }
		public required string Password { get; set; }
		public virtual Role? Role { get; set; }
	}
}

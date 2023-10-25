namespace Services.API.RequestEntities
{
	public class PasswordUpdateRequest
	{
		public string OldPassword { get; set; }
		public string NewPassword { get; set; }
	}
}

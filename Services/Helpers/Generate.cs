using System.Security.Cryptography;
using System.Text;

namespace Services.Helpers
{
	public class Generate
	{
		public static string GeneratedSlug(string name){
			string slug = name.ToLower().Replace(" ", "-");
			slug = new string(slug?.Where(c => char.IsLetterOrDigit(c) || c == '-').ToArray());
            return slug.Trim('-');
		}

		public static string GenerateHashedPassword(string password)
		{
			return CaclculateSHA256(password);
		}

		public static string CaclculateSHA256(string data)
		{
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(data));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static string CalculateHMACSHA256(string secret, string data)
        {
            using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret)))
            {
                byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));

                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public static string CalculateHMACSHA512(string secret, string data)
        {
            var hash = new StringBuilder();
            byte[] keyBytes = Encoding.UTF8.GetBytes(secret);
            byte[] inputBytes = Encoding.UTF8.GetBytes(data);
            using (var hmac = new HMACSHA512(keyBytes)){
                byte[] hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }
            return hash.ToString();
        }
    }
}

using System.Security.Cryptography;
using System.Text;

namespace WeLearnOnine_Website.Models
{
    public class Helper
    {
        private readonly IConfiguration _configuration;

        public Helper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string GenerateBillCode(DateTime date, int count)
        {
            string datePart = date.ToString("yyyyMMdd");
            string countPart = (count + 1).ToString("D4"); // D4 sẽ đảm bảo có 4 chữ số
            return $"{datePart}-{countPart}";
        }

        public static String getSignature(String text, String key)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();

            Byte[] textBytes = encoding.GetBytes(text);
            Byte[] keyBytes = encoding.GetBytes(key);

            Byte[] hashBytes;

            using (HMACSHA256 hash = new HMACSHA256(keyBytes))
                hashBytes = hash.ComputeHash(textBytes);

            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}

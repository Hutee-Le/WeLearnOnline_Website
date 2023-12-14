using System.Security.Cryptography;
using System.Text;

namespace WeLearnOnine_Website.Models
{
    public class MoMoHelper
    {
        private readonly IConfiguration _configuration;

        public MoMoHelper(IConfiguration configuration)
        {
            _configuration = configuration;
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

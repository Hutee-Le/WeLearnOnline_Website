using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace WeLearnOnine_Website.Models
{
    public class Helper
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly DerekmodeWeLearnSystemContext _dbContext;

        public Helper(IConfiguration configuration, UserManager<IdentityUser> userManager, DerekmodeWeLearnSystemContext dbContext)
        {
            _configuration = configuration;
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task<int> GetUserId(ClaimsPrincipal claimsPrincipal)
        {

            var identityUser = await _userManager.GetUserAsync(claimsPrincipal);

            if (identityUser != null)
            {
                var email = identityUser.Email;
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
                if (user != null)
                {
                    return user.UserId;
                }
            }
            return 0;
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

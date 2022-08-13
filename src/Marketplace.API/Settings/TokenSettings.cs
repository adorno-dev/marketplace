using System.Text;

namespace Marketplace.API.Settings
{
    public class TokenSettings
    {
        public string SecretKey { get; set; } = string.Empty;

        public byte[] GetSecret() => Encoding.ASCII.GetBytes(SecretKey);
    }
}
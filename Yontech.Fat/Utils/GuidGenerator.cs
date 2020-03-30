using System;
using System.Security.Cryptography;
using System.Text;

namespace Yontech.Fat.Utils
{
    public static class GuidGenerator
    {
        private readonly static HashAlgorithm Hasher = SHA1.Create();
        public static Guid FromString(string data)
        {
            var hash = Hasher.ComputeHash(Encoding.Unicode.GetBytes(data));
            var b = new byte[16];
            Array.Copy((Array)hash, (Array)b, 16);
            var bb = new Guid(b);
            return bb;
        }
    }
}

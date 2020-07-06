using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UserTask.Objects
{
    public class SHA256Hasher 
    {
        SHA256Managed Hasher = new SHA256Managed();
        public bool CompareHash(string base64_hash1, string base64_hash2)
        {
            return base64_hash1.Equals(base64_hash2);
        }
        public bool CompareHash(string base64_hash1, byte[] data)
        {
            string base64_hash2 = this.GenerateHash(data);
            return base64_hash1.Equals(base64_hash2);
        }
        public string GenerateHash(byte[] data)
        {
            return Convert.ToBase64String(Hasher.ComputeHash(data));
        }
    }
}
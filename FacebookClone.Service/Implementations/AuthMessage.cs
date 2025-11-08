using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Service.Implementations
{
    public class AuthMessage
    {
        public string AccessToken { get; set; }
        public RefreshToken refreshToken { get; set; }
        public string Message { get; set; }
        public class RefreshToken
        {
            public string UserName { get; set; }
            public string TokenString { get; set; }
            public DateTime ExpireDate { get; set; }
        }
    }
}

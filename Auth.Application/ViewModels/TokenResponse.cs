using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.ViewModels
{
    public class TokenResponseViewModel
    {
        public string AccessToken { get; set; }
        public double ExpiresIn{ get; set; }
    }
}

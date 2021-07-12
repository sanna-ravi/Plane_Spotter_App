using Spotters.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Spotters.Service.Interface
{
    public interface IAUTHService
    {
        Task<TokenViewModel> LoginUser(SignInViewModel creds, String IPAddress);
    }
}

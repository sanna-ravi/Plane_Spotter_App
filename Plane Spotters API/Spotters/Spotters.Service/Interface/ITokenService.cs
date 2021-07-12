using Spotters.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Spotters.Service.Interface
{
    public interface ITokenService
    {
        Task<TokenViewModel> GenerateToken(String userId, String name, String role, String IPAddress);
    }
}

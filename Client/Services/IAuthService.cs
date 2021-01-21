using BlazorWasm.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWasm.Client.Services
{
  public interface IAuthService
  {
    Task<ServiceResponse<int>> Register(UserRegister request);
    Task<ServiceResponse<string>> Login(BlazorWasm.Shared.UserLogin request);
  }
}

﻿using BlazorWasm.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWasm.Server.Services
{
  public interface IUtilityService
  {
    Task<User> GetUser();
  }
}

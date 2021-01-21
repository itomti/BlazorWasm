﻿using BlazorWasm.Server.Data;
using BlazorWasm.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWasm.Server.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly IAuthRepository _authRepository;
    public AuthController(IAuthRepository authRepository)
    {
      _authRepository = authRepository; 
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegister request)
    {
      User user = new User
      {
        Username = request.Username,
        Email = request.Email,
        Bananas = request.Bananas,
        DateOfBirth = request.DateOfBirth,
        IsConfirmed = request.IsConfirmed
      };

      var response = await _authRepository.Register(user, request.Password, int.Parse(request.StartUnitId));

      if (!response.Success)
      {
        return BadRequest(response);
      }

      return Ok(response);
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLogin request)
    {
      var response = await _authRepository.Login(request.Email, request.Password);   

      if (!response.Success)
      {
        return BadRequest(response);
      }

      return Ok(response);
    }
  }
}
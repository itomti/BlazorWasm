using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorWasm.Shared
{
  public class UserLogin
  {
    [Required(ErrorMessage = "Please enter an e-mail address.")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Please enter a password.")]
    public string Password { get; set; }
  }
}

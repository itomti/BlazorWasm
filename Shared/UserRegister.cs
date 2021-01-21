using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorWasm.Shared
{
  public class UserRegister
  {
    [Required]
    public string Email { get; set; }
    [StringLength(16, ErrorMessage = "Your username is too long.")]
    public string Username { get; set; }
    public string Bio { get; set; }
    [Required, StringLength(100, MinimumLength = 6)]
    public string Password { get; set; }
    [Required, StringLength(100, MinimumLength = 6)]
    [Compare("Password", ErrorMessage = "The passwords do not match.")]
    public string ConfirmPassword { get; set; }
    [Range(0, 1000, ErrorMessage = "Please choose a number between 0 and 1000.")]
    public int Bananas { get; set; }
    public string StartUnitId { get; set; }
    public DateTime DateOfBirth { get; set; } = DateTime.Now;
    [Range(typeof(bool), "true", "true", ErrorMessage = "Only confirmed users can play!")]
    public bool IsConfirmed { get; set; } = true;
  }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorWasm.Shared
{
  public class Unit
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int Health { get; set; } = 100;
    public int Cost { get; set; }
  }
}

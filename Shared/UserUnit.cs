﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorWasm.Shared
{
  public class UserUnit
  {
    public int Id { get; set; }
    public int UserId { get; set; }
    public Unit Unit { get; set; }
    public int UnitId { get; set; }
    public int Health { get; set; }
  }
}

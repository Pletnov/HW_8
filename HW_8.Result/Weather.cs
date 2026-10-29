using System;
using System.Collections.Generic;
using System.Text;

namespace HW_8.Result;

public class Weather
{
    public double latitude { get; set; }
    public double longitude { get; set; }
    public HourlyData hourly { get; set; }
}

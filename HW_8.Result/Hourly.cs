using System;
using System.Collections.Generic;
using System.Text;

namespace HW_8.Result;

public class HourlyData
{
    public DateTime[] time { get; set; }
    public double[] temperature_2m { get; set; }
    public int[] relative_humidity_2m { get; set; }
    public double[] wind_speed_10m { get; set; }
}

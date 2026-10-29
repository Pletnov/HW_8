using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace HW_8.Result;

public class Request
{
    public double latitude { get; set; }
    public double longitude { get; set; }
    public string hourly { get; set; }

    public int forecastDays { get; set; }

    public string Convert()
    {
        string lat = latitude.ToString(CultureInfo.InvariantCulture);
        string lon = longitude.ToString(CultureInfo.InvariantCulture);
        return $"?latitude={lat}&longitude={lon}&hourly={hourly}&forecast_days={forecastDays}";
    }
}


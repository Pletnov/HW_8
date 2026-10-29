using System.Globalization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text.Json;


//1
/*using HW_8.Result;
Request request = new Request
{
    latitude = 49.98,
    longitude = 36.25,
    hourly = "temperature_2m,relative_humidity_2m,wind_speed_10m",
    forecastDays = 1
};

using var httpClient = new HttpClient();
var url = "https://api.open-meteo.com/v1/forecast";
var send = new HttpRequestMessage(HttpMethod.Get, url + request.Convert());
var response = await httpClient.SendAsync(send);
var jsonResponse = await response.Content.ReadAsStringAsync();
var weather = JsonSerializer.Deserialize<Weather>(jsonResponse);
Console.WriteLine("Kharkiv");
if (weather?.hourly != null)
{
    for (int i = 0; i < weather.hourly.time.Length; i++)
    {
        DateTime time = weather.hourly.time[i];
        double temp = weather.hourly.temperature_2m[i];
        int humidity = weather.hourly.relative_humidity_2m[i];
        double wind = weather.hourly.wind_speed_10m[i];

        Console.WriteLine($"time: {time}  | {temp}°C  | {humidity}%  | {wind} km/h");
    }
}
Console.WriteLine("========================================================");
string[,] centres = {
    { "Vinnytsia", "49.23", "28.47" },
    { "Dnipro", "48.46", "35.04" },
    { "Donetsk", "48.01", "37.80" },
    { "Zhytomyr", "50.25", "28.66" },
    { "Zaporizhzhia", "47.84", "35.14" },
    { "Ivano-Frankivsk", "48.92", "24.71" },
    { "Kyiv", "50.45", "30.52" },
    { "Kropyvnytskyi", "48.51", "32.26" },
    { "Luhansk", "48.57", "39.31" },
    { "Lutsk", "50.76", "25.34" },
    { "Lviv", "49.84", "24.03" },
    { "Mykolaiv", "46.98", "32.00" },
    { "Odesa", "46.48", "30.72" },
    { "Poltava", "49.59", "34.55" },
    { "Rivne", "50.62", "26.25" },
    { "Sumy", "50.91", "34.80" },
    { "Ternopil", "49.55", "25.59" },
    { "Uzhhorod", "48.62", "22.30" },
    { "Kharkiv", "49.99", "36.23" },
    { "Kherson", "46.63", "32.61" },
    { "Khmelnytskyi", "49.42", "27.00" },
    { "Cherkasy", "49.44", "32.06" },
    { "Chernivtsi", "48.29", "25.93" },
    { "Chernihiv", "51.49", "31.29" }
};
bool finish = true;
while (finish)
{
    Console.WriteLine("=== Menu ===");
    for (int i = 0; i < centres.GetLength(0); i++)
    {
        Console.WriteLine($"{i + 1}. {centres[i, 0]}");
    }
    Console.Write(">>> ");
    string option = Console.ReadLine();
    int index = int.Parse(option) - 1;
    if (index >= 0 && index < centres.GetLength(0))
    {
        string cityName = centres[index, 0];
        double lat = double.Parse(centres[index, 1], CultureInfo.InvariantCulture);
        double lon = double.Parse(centres[index, 2], CultureInfo.InvariantCulture);
        Request request2 = new Request
        {
            latitude = lat,
            longitude = lon,
            hourly = "temperature_2m,relative_humidity_2m,wind_speed_10m",
            forecastDays = 3
        };
        var send2 = new HttpRequestMessage(HttpMethod.Get, url + request2.Convert());
        var response2 = await httpClient.SendAsync(send2);
        var jsonResponse2 = await response2.Content.ReadAsStringAsync();
        var weather2 = JsonSerializer.Deserialize<Weather>(jsonResponse2);
        if (weather2?.hourly != null)
        {
            for (int i = 0; i < weather2.hourly.time.Length; i++)
            {
                DateTime time = weather2.hourly.time[i];
                double temp = weather2.hourly.temperature_2m[i];
                int humidity = weather2.hourly.relative_humidity_2m[i];
                double wind = weather2.hourly.wind_speed_10m[i];

                Console.WriteLine($"time: {time}  | {temp}°C  | {humidity}%  | {wind} km/h");
            }
        }
        Console.WriteLine("Do you want to search more?(yes/no)");
        string search = Console.ReadLine();
        if(search.ToLower() == "no")
        {
            finish = false;
        }
        if (search.ToLower() == "yes")
        {
            continue;
        }
    }
    else
    {
        Console.WriteLine("Error");
    }
}*/

//2
/*
using HtmlAgilityPack;

int maxHtmlPages = 7260;
var pageUrl = "https://infoshina.com.ua/uk/shiny";
using var httpClient = new HttpClient();
List<string[]> information = new List<string[]>();

Console.WriteLine("TO START DOWNLOADING INFORMATION PRESS 'ENTER'");
CancellationTokenSource cts = new CancellationTokenSource();
var cancellationToken = cts.Token;

_ = Task.Run(() =>
{
    while (!cancellationToken.IsCancellationRequested)
    {
        var key = Console.ReadKey();
        if (key.Key == ConsoleKey.Enter)
        {
            cts.Cancel();
            break;
        }
    }
});
try
{
    int i = 1;
    while (!cancellationToken.IsCancellationRequested && i < maxHtmlPages)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var request = new HttpRequestMessage(HttpMethod.Get, pageUrl + $"?start={i}");
        var response = await httpClient.SendAsync(request, cancellationToken);
        var pageContent = await response.Content.ReadAsStringAsync(cancellationToken);

        var filename = "page.html";
        await File.WriteAllTextAsync(filename, pageContent, cancellationToken);

        var htmlDocument = new HtmlDocument();
        htmlDocument.Load(filename);

        var productNodes = htmlDocument.QuerySelectorAll("div[data-cy='ProductCard']");
        Console.WriteLine($"PAGE: {i}");
        foreach (var productNode in productNodes)
        {

            var vendor = productNode.QuerySelector("a.product-card__name span").InnerText;

            var productName = productNode.QuerySelector("a.product-card__name").InnerText;

            var discountedPriceNode = productNode.QuerySelector(".Price_value__9eRgy.price-value.Price_discounted___Qw6O");
            var regularPriceNode = productNode.QuerySelector("div.df.aic.Price_value__9eRgy.price-value");
            var priceNode = discountedPriceNode ?? regularPriceNode;
            var price = priceNode?.InnerText.Replace('₴', ' ').Trim();

            var sku = productNode.QuerySelector("div.Sku_wrap__pCkJ8").InnerText.Substring(4).Trim();

            Console.WriteLine($"Product: {vendor}, {productName}, Price: {price}, SKU: {sku}");
            information.Add(new[]
            {
                vendor,
                productName,
                price,
                sku
            });
        }
        i++;
    }
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}

if(information.Count > 0)
{
    Console.WriteLine("Starting downloading...");
    var path = "products.csv";
    var lines = new List<string>
    {
        "Vendor,Name,Price,Sku"
    };
    foreach (var item in information)
    {
        var cleanVendor = $"\"{item[0].Replace("\"", "\"\"")}\"";
        var cleanName = $"\"{item[1].Replace("\"", "\"\"")}\"";
        var cleanPrice = $"\"{item[2].Replace("\"", "\"\"")}\"";
        var cleanSku = $"\"{item[3].Replace("\"", "\"\"")}\"";

        lines.Add($"{cleanVendor},{cleanName},{cleanPrice},{cleanSku}");
    }
    await File.WriteAllLinesAsync(path, lines);
    Console.WriteLine("Downloading finished!");
} else
{
    Console.WriteLine("Downloading failed");
}
*/
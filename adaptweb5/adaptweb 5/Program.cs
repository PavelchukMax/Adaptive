using adaptweb_5;

public class Program
{
    public static async Task Main(string[] args)
    {
        var httpClient = new HttpClient();
        var weatherApiClient = new Api(httpClient);

        var currentWeatherUrl = "https://api.weatherbit.io/v2.0/current?key=e343575257e7470e95873cadadaf45a6&city=Kyiv&country=UA";
        var forecastUrl = "https://api.weatherbit.io/v2.0/forecast/daily?key=e343575257e7470e95873cadadaf45a6&city=Kyiv&country=UA";

        var currentWeatherResponse = await weatherApiClient.GetAsync<WeatherData>(currentWeatherUrl);
        var forecastResponse = await weatherApiClient.GetAsync<WeatherForecast>(forecastUrl);

        Console.WriteLine($"Status Code: {currentWeatherResponse.HttpStatusCode}");
        Console.WriteLine($"Message: {currentWeatherResponse.Message}");

        if (currentWeatherResponse.Data != null)
        {
            Console.WriteLine("Weather Data:");
            Console.WriteLine($"City: {currentWeatherResponse.Data.City}");
            Console.WriteLine($"Country: {currentWeatherResponse.Data.Country}");
            Console.WriteLine($"Temperature: {currentWeatherResponse.Data.Temperature}°C");
            Console.WriteLine($"Weather Description: {currentWeatherResponse.Data.WeatherDescription}");
        }

        Console.WriteLine();

        Console.WriteLine("Weather Forecast for the next 7 days:");
        Console.WriteLine($"Status Code: {forecastResponse.HttpStatusCode}");
        Console.WriteLine($"Message: {forecastResponse.Message}");
        if (forecastResponse.Data != null)
        {
            foreach (var forecastDay in forecastResponse.Data.Data)
            {
                Console.WriteLine($"Date: {forecastDay.DateTime.ToShortDateString()}");
                Console.WriteLine($"City: {forecastDay.CityName}, Country: {forecastDay.CountryCode}");
                Console.WriteLine($"Temperature: {forecastDay.Temperature}°C");
                Console.WriteLine($"Weather Description: {forecastDay.WeatherDescription}");
                Console.WriteLine();
            }
        }
    }
}

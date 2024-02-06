using System.Text;
using System.Text.Json;
using Azure.Storage.Blobs;

namespace DotnetAspireDemo.ApiService.Weather;

public class WeatherForecastService(BlobServiceClient client)
{
    public async Task SaveWeatherForecast(WeatherForecast[] forecast)
    {
        var container = client.GetBlobContainerClient("forecasts");

        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        var blob = container.GetBlobClient($"{timestamp}.json");
        var json = JsonSerializer.Serialize(forecast);

        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
        await blob.UploadAsync(stream, true);
    }
}

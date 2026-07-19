using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

string openAiKey = builder.Configuration["OPENAI_API_KEY"]
    ?? Environment.GetEnvironmentVariable("OPENAI_API_KEY") ?? "";

app.MapGet("/api/health", () => Results.Ok(new { status = "alive" }));

app.MapGet("/api/session", async () =>
{
    if (string.IsNullOrEmpty(openAiKey))
        return Results.Problem("OPENAI_API_KEY not set on the server.");

    using var http = new HttpClient();
    http.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Bearer", openAiKey);

    // New GA endpoint shape: session config is nested under "session"
    var body = new
    {
        session = new
        {
            type = "realtime",
            model = "gpt-realtime",
            audio = new
            {
                output = new { voice = "marin" }
            }
        }
    };

    var content = new StringContent(
        JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

    // New endpoint: client_secrets, not sessions
    var resp = await http.PostAsync(
        "https://api.openai.com/v1/realtime/client_secrets", content);

    var json = await resp.Content.ReadAsStringAsync();

    if (!resp.IsSuccessStatusCode)
        return Results.Problem($"OpenAI error: {json}");

    return Results.Content(json, "application/json");
});

app.Run();

public partial class Program { }
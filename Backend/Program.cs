using Microsoft.Data.Sqlite;
using MiniApi;

await using var db = new SqliteConnection("Data source=_db.db");
db.Open();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton(db);
var app = builder.Build();
app.MapGet("/", () => "try api/alerts");

// retrieve a list of alerts
app.MapGet("/api/alerts", Alerts.All);
app.MapPost("/api/alerts", (Alerts.PostDTO req) =>
{
    Console.WriteLine($"Received POST request: {req}");

    if (!Enum.TryParse<Severity>(req.Severity, true, out var severity) || !Enum.IsDefined(typeof(Severity), severity))
    {
        Console.WriteLine($"Invalid severity level: {req.Severity}");
        return Results.BadRequest("Invalid severity level! There are 4 levels: Low, Medium, High, Critical");
    }
    Alerts.Post(req, db);
    return Results.Ok(req);
});



app.Run("http://localhost:3002");
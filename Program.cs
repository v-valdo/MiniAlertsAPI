using Microsoft.Data.Sqlite;

var db = new SqliteConnection("Data source=_db");

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton(db);
var app = builder.Build();

app.MapGet("/", () => "Mini-Alert API");

// retrieve a list of alerts
app.MapGet("/alerts", () => "Mini-Alert API");
// create new alert
app.MapPost("/alert", (alert) =>
{
    Results.Ok(new Alert(alert.Name, alert.severity, alert.description));
});


app.Run();
public record (string Name, Severity severity, string description);

public record State(SqliteConnection db);
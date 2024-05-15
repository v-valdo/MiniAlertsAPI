using Microsoft.Data.Sqlite;
using MiniApi;

await using var db = new SqliteConnection("Data source=_db.db");
db.Open();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton(db);
var app = builder.Build();

// retrieve a list of alerts
app.MapGet("/alerts", Alerts.All);

app.Run("http://localhost:3002");
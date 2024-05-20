namespace MiniApi;
using Microsoft.Data.Sqlite;

public static class Alerts
{
    public record AllDTO(int Id, string Name, string Description, Severity Severity, string TimeRecorded);
    public record PostDTO(string Name, string Description, string Severity);

    public static List<AllDTO> All(SqliteConnection db)
    {
        List<AllDTO> result = new();
        var cmd = db.CreateCommand();
        cmd.CommandText = "select * from alerts;";
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            int id = reader.GetInt32(0);
            string name = reader.GetString(1);
            string description = reader.GetString(2);
            string severityText = reader.GetString(3);
            Severity severity = Enum.Parse<Severity>(severityText, ignoreCase: true);
            string time = reader.GetString(4);

            result.Add(new(id, name, description, severity, time));
        }
        return result;
    }
    public static void Post(PostDTO alert, SqliteConnection db)
    {
        var cmd = db.CreateCommand();
        cmd.CommandText = "insert into alerts(name, description, severity, time_recorded) values ($1, $2, $3, $4)";
        cmd.Parameters.AddWithValue("$1", alert.Name);
        cmd.Parameters.AddWithValue("$2", alert.Description);
        cmd.Parameters.AddWithValue("$3", alert.Severity.ToString());
        cmd.Parameters.AddWithValue("$4", DateTime.Now.ToShortDateString());
        cmd.ExecuteNonQuery();
    }
}

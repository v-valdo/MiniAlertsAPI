namespace MiniApi;
using Microsoft.Data.Sqlite;

public static class Alerts
{
    public record AllDTO(int Id, string Name, string description, Severity severity);
    public record PostDTO(string Name, Severity severity, string description);

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

            result.Add(new(id, name, description, severity));
        }
        return result;
    }
}

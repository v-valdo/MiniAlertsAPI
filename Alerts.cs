namespace MiniApi;

public static class Alerts
{
    public record AllDTO(int Id, string Name, Severity severity, string description);
    public record PostDTO(string Name, Severity severity, string description);

    public static List<AllDTO> All(State state)
    {
        List<AllDTO> result = new();
        state.db.Open();
        var cmd = state.db.CreateCommand();
        cmd.CommandText = "select * from alerts;";
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            int id = reader.GetInt32(0);
            string name = reader.GetString(1);
            string severityText = reader.GetString(2);
            Severity severity = Enum.Parse<Severity>(severityText, ignoreCase: true);
            string description = reader.GetString(3);

            result.Add(new(id, name, severity, description));
        }
        return result;
    }
}

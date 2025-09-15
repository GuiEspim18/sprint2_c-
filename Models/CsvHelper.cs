using System.Globalization;
using System.Text;

namespace InvestYOU.Models;

/// <summary>
/// Classe auxiliar para manipulação de dados CSV.
/// Gerencia persistência de usuários e dados da IA.
/// </summary>
public static class CsvHelper
{
    private static readonly string UsersFile = Path.Combine(FileSystem.AppDataDirectory, "users.csv");
    private static readonly string IADataFile = Path.Combine(FileSystem.AppDataDirectory, "iadata.csv");

    // ------------------ USERS ------------------

    /// <summary>
    /// Salva um usuário no CSV de usuários.
    /// </summary>
    /// <param name="user">Usuário a ser salvo</param>
    public static async Task SaveUserAsync(User user)
    {
        var line = $"{user.Id};{user.UserName};{user.Email};{user.Password}";
        await File.AppendAllLinesAsync(UsersFile, new[] { line }, Encoding.UTF8);
    }

    /// <summary>
    /// Carrega todos os usuários do CSV.
    /// </summary>
    /// <returns>Lista de usuários</returns>
    public static async Task<List<User>> LoadUsersAsync()
    {
        if (!File.Exists(UsersFile)) return new List<User>();

        var lines = await File.ReadAllLinesAsync(UsersFile, Encoding.UTF8);

        return lines
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(line =>
            {
                var parts = line.Split(';');
                if (parts.Length < 4) return null;

                return new User
                {
                    Id = int.TryParse(parts[0], out var id) ? id : 0,
                    UserName = parts[1],
                    Email = parts[2],
                    Password = parts[3]
                };
            })
            .Where(u => u != null)
            .ToList()!;
    }

    // ------------------ IADATA ------------------

    /// <summary>
    /// Salva os dados da IA em CSV.
    /// </summary>
    /// <param name="data">Dados da IA</param>
    public static async Task SaveIADataAsync(IAData data)
    {
        var filesJoined = string.Join("|", data.Files);
        var line = $"{data.Prompt};{data.MaxTokens};{data.TextTemperature.ToString(CultureInfo.InvariantCulture)};{filesJoined}";

        // Salva apenas a última configuração
        await File.WriteAllTextAsync(IADataFile, line, Encoding.UTF8);
    }

    /// <summary>
    /// Carrega os dados da IA do CSV.
    /// </summary>
    /// <returns>Objeto IAData ou null caso não exista</returns>
    public static async Task<IAData?> LoadIADataAsync()
    {
        if (!File.Exists(IADataFile)) return null;

        var line = await File.ReadAllTextAsync(IADataFile, Encoding.UTF8);
        if (string.IsNullOrWhiteSpace(line)) return null;

        var parts = line.Split(';');
        if (parts.Length < 3) return null;

        return new IAData
        {
            Prompt = parts[0],
            MaxTokens = int.TryParse(parts[1], out var max) ? max : 0,
            TextTemperature = double.TryParse(parts[2], NumberStyles.Any, CultureInfo.InvariantCulture, out var temp) ? temp : 1.0,
            Files = parts.Length > 3 ? parts[3].Split('|').ToList() : new List<string>()
        };
    }
}

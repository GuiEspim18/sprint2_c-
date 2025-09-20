using System.Text.Json;

namespace InvestYOU.Models;

/// <summary>
/// Classe auxiliar para manipulação de dados JSON.
/// Gerencia persistência de usuários e dados da IA.
/// </summary>
public static class JsonHelper
{
    private static readonly string UsersFile = Path.Combine(FileSystem.AppDataDirectory, "users.json");
    private static readonly string IADataFile = Path.Combine(FileSystem.AppDataDirectory, "iadata.json");

    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true, // deixa o JSON legível
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    // ------------------ USERS ------------------

    /// <summary>
    /// Salva ou atualiza um usuário no JSON de usuários.
    /// </summary>
    public static async Task SaveUserAsync(User user)
    {
        var users = await LoadUsersAsync();
        // evita duplicação pelo Id
        var existing = users.FirstOrDefault(u => u.Id == user.Id);
        if (existing != null)
        {
            users.Remove(existing);
        }
        users.Add(user);

        var json = JsonSerializer.Serialize(users, Options);
        await File.WriteAllTextAsync(UsersFile, json);
    }

    /// <summary>
    /// Carrega todos os usuários do JSON.
    /// </summary>
    public static async Task<List<User>> LoadUsersAsync()
    {
        if (!File.Exists(UsersFile)) return new List<User>();

        var json = await File.ReadAllTextAsync(UsersFile);
        return JsonSerializer.Deserialize<List<User>>(json, Options) ?? new List<User>();
    }

    // ------------------ IADATA ------------------

    /// <summary>
    /// Salva os dados da IA em JSON.
    /// </summary>
    public static async Task SaveIADataAsync(IAData data)
    {
        var json = JsonSerializer.Serialize(data, Options);
        await File.WriteAllTextAsync(IADataFile, json);
    }

    /// <summary>
    /// Carrega os dados da IA do JSON.
    /// </summary>
    public static async Task<IAData?> LoadIADataAsync()
    {
        if (!File.Exists(IADataFile)) return null;

        var json = await File.ReadAllTextAsync(IADataFile);
        return JsonSerializer.Deserialize<IAData>(json, Options);
    }
}

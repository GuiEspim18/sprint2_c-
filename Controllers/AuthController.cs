using InvestYOU.Models;

namespace InvestYOU.Controllers;

/// <summary>
/// Controlador responsável pelo login, registro e manipulação de usuários.
/// Utiliza CsvHelper para persistência de dados.
/// </summary>
public class AuthController
{
    /// <summary>
    /// Verifica se o usuário existe e se a senha está correta.
    /// </summary>
    /// <param name="email">Email do usuário</param>
    /// <param name="password">Senha do usuário</param>
    /// <returns>True se login for válido, false caso contrário</returns>
    public async Task<bool> LoginAsync(string email, string password)
    {
        var users = await JsonHelper.LoadUsersAsync();
        return users.Any(u => u.Email == email && u.Password == password);
    }

    /// <summary>
    /// Registra um novo usuário.
    /// </summary>
    /// <param name="user">Objeto User contendo os dados do usuário</param>
    public async Task RegisterAsync(User user)
    {
        await JsonHelper.SaveUserAsync(user);
    }

    /// <summary>
    /// Executa uma ação após login bem-sucedido.
    /// </summary>
    /// <param name="onSuccess">Callback a ser executado</param>
    public void OnLoginSuccess(Action onSuccess)
    {
        onSuccess?.Invoke();
    }
}

using InvestYOU.Models;

namespace InvestYOU.Controllers;

public class AuthController
{
    public async Task<bool> LoginAsync(string email, string password)
    {
        var users = await CsvHelper.LoadUsersAsync();
        return users.Any(u => u.Email == email && u.Password == password);
    }

    public async Task RegisterAsync(User user)
    {
        await CsvHelper.SaveUserAsync(user);
    }

    public void OnLoginSuccess(Action onSuccess)
    {
        onSuccess?.Invoke();
    }
}

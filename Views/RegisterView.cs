using Microsoft.Maui.Controls;
using InvestYOU.Controllers;
using InvestYOU.Models;

namespace InvestYOU.Views;

public class RegisterView : ContentPage
{
    private readonly Entry UsernameEntry;
    private readonly Entry EmailEntry;
    private readonly Entry PasswordEntry;
    private readonly Label ErrorLabel;
    private readonly AuthController Controller;

    public RegisterView()
    {
        Controller = new AuthController();

        UsernameEntry = new Entry { Placeholder = "Usuário" };
        EmailEntry = new Entry { Placeholder = "Email" };
        PasswordEntry = new Entry { Placeholder = "Senha", IsPassword = true };

        var registerButton = new Button { Text = "Registrar" };
        var loginButton = new Button { Text = "Voltar para Login" };
        ErrorLabel = new Label { TextColor = Colors.Red, IsVisible = false };

        registerButton.Clicked += OnRegisterButtonClickedAsync;
        loginButton.Clicked += OnLoginButtonClicked;

        Content = new VerticalStackLayout
        {
            Padding = 20,
            Children = { UsernameEntry, EmailEntry, PasswordEntry, registerButton, loginButton, ErrorLabel }
        };
    }

    private async void OnRegisterButtonClickedAsync(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(UsernameEntry.Text) ||
            string.IsNullOrWhiteSpace(EmailEntry.Text) ||
            string.IsNullOrWhiteSpace(PasswordEntry.Text))
        {
            DisplayError("Todos os campos são obrigatórios.");
            return;
        }

        var user = new User
        {
            Id = Guid.NewGuid().GetHashCode(), // pode substituir por outro ID único se desejar
            UserName = UsernameEntry.Text.Trim(),
            Email = EmailEntry.Text.Trim(),
            Password = PasswordEntry.Text
        };

        try
        {
            await Controller.RegisterAsync(user);
            await DisplayAlert("Sucesso", "Usuário registrado com sucesso!", "OK");
            Application.Current.MainPage = new LoginView();
        }
        catch (Exception ex)
        {
            DisplayError($"Erro ao registrar usuário: {ex.Message}");
        }
    }

    private void OnLoginButtonClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new LoginView();
    }

    private void DisplayError(string message)
    {
        ErrorLabel.Text = message;
        ErrorLabel.IsVisible = true;
    }
}

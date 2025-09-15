using Microsoft.Maui.Controls;
using InvestYOU.Controllers;

namespace InvestYOU.Views;

public class LoginView : ContentPage
{
    private readonly Entry EmailEntry;
    private readonly Entry PasswordEntry;
    private readonly Label ErrorLabel;
    private readonly AuthController Controller;

    public LoginView()
    {
        Controller = new AuthController();

        EmailEntry = new Entry { Placeholder = "Email" };
        PasswordEntry = new Entry { Placeholder = "Senha", IsPassword = true };
        var loginButton = new Button { Text = "Login" };
        var registerButton = new Button { Text = "Registrar" };
        ErrorLabel = new Label { TextColor = Colors.Red, IsVisible = false };

        loginButton.Clicked += OnLoginButtonClickedAsync;
        registerButton.Clicked += OnRegisterButtonClicked;

        Content = new VerticalStackLayout
        {
            Padding = 20,
            Children = { EmailEntry, PasswordEntry, loginButton, registerButton, ErrorLabel }
        };
    }

    private async void OnLoginButtonClickedAsync(object sender, EventArgs e)
    {
        var email = EmailEntry.Text;
        var password = PasswordEntry.Text;

        if (await Controller.LoginAsync(email, password))
        {
            // Passa como callback a navegação
            Controller.OnLoginSuccess(() =>
            {
                Application.Current.MainPage = new HomeView();
            });
        }
        else
        {
            DisplayError("Usuário ou senha inválidos");
        }
    }

    private void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new RegisterView();
    }

    private void DisplayError(string message)
    {
        ErrorLabel.Text = message;
        ErrorLabel.IsVisible = true;
    }
}

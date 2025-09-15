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
        BackgroundColor = Color.FromArgb("#0D0D0D");

        var logo = new Image
        {
            Source = "investyou.png",
            HorizontalOptions = LayoutOptions.Center,
            HeightRequest = 120
        };

        UsernameEntry = new Entry
        {
            Placeholder = "Usuário",
            PlaceholderColor = Colors.Gray,
            TextColor = Colors.White,
            BackgroundColor = Color.FromArgb("#1E1E1E"),
            Margin = new Thickness(0,5)
        };

        EmailEntry = new Entry
        {
            Placeholder = "Email",
            PlaceholderColor = Colors.Gray,
            TextColor = Colors.White,
            BackgroundColor = Color.FromArgb("#1E1E1E"),
            Margin = new Thickness(0,5)
        };

        PasswordEntry = new Entry
        {
            Placeholder = "Senha",
            IsPassword = true,
            PlaceholderColor = Colors.Gray,
            TextColor = Colors.White,
            BackgroundColor = Color.FromArgb("#1E1E1E"),
            Margin = new Thickness(0,5)
        };

        var registerButton = new Button
        {
            Text = "Registrar",
            BackgroundColor = Color.FromArgb("#FFCE00"),
            TextColor = Colors.Black,
            CornerRadius = 8
        };

        var loginButton = new Button
        {
            Text = "Voltar para Login",
            BackgroundColor = Color.FromArgb("#1E1E1E"),
            TextColor = Color.FromArgb("#FFCE00"),
            CornerRadius = 8,
            BorderColor = Color.FromArgb("#FFCE00"),
            BorderWidth = 2
        };

        ErrorLabel = new Label
        {
            TextColor = Color.FromArgb("#F95555"),
            IsVisible = false,
            HorizontalOptions = LayoutOptions.Center
        };

        registerButton.Clicked += OnRegisterButtonClickedAsync;
        loginButton.Clicked += OnLoginButtonClicked;

        Content = new ScrollView
        {
            Content = new VerticalStackLayout
            {
                Padding = 30,
                Spacing = 15,
                Children = { logo, UsernameEntry, EmailEntry, PasswordEntry, registerButton, loginButton, ErrorLabel }
            }
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
            Id = Guid.NewGuid().GetHashCode(),
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

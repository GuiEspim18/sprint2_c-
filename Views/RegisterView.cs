using Microsoft.Maui.Controls;
using InvestYOU.Controllers;
using InvestYOU.Models;

namespace InvestYOU.Views;

/// <summary>
/// Tela de registro de usuário.
/// Permite criar novos usuários cadastrando nome, email e senha.
/// Campos e botões possuem largura fixa de 400 para consistência visual.
/// </summary>
public class RegisterView : ContentPage
{
    private readonly Entry UsernameEntry;    // Campo de nome de usuário
    private readonly Entry EmailEntry;       // Campo de email
    private readonly Entry PasswordEntry;    // Campo de senha
    private readonly Label ErrorLabel;       // Label para erros
    private readonly AuthController Controller; // Controller para registro

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

        // Campos de entrada com largura fixa
        UsernameEntry = new Entry
        {
            Placeholder = "Usuário",
            PlaceholderColor = Colors.Gray,
            TextColor = Colors.White,
            BackgroundColor = Color.FromArgb("#1E1E1E"),
            Margin = new Thickness(0,5),
            WidthRequest = 400,
            HorizontalOptions = LayoutOptions.Center
        };

        EmailEntry = new Entry
        {
            Placeholder = "Email",
            PlaceholderColor = Colors.Gray,
            TextColor = Colors.White,
            BackgroundColor = Color.FromArgb("#1E1E1E"),
            Margin = new Thickness(0,5),
            WidthRequest = 400,
            HorizontalOptions = LayoutOptions.Center
        };

        PasswordEntry = new Entry
        {
            Placeholder = "Senha",
            IsPassword = true,
            PlaceholderColor = Colors.Gray,
            TextColor = Colors.White,
            BackgroundColor = Color.FromArgb("#1E1E1E"),
            Margin = new Thickness(0,5),
            WidthRequest = 400,
            HorizontalOptions = LayoutOptions.Center
        };

        // Botões
        var registerButton = new Button
        {
            Text = "Registrar",
            BackgroundColor = Color.FromArgb("#FFCE00"),
            TextColor = Colors.Black,
            CornerRadius = 8,
            WidthRequest = 400,
            HorizontalOptions = LayoutOptions.Center
        };

        var loginButton = new Button
        {
            Text = "Voltar para Login",
            BackgroundColor = Color.FromArgb("#1E1E1E"),
            TextColor = Color.FromArgb("#FFCE00"),
            CornerRadius = 8,
            BorderColor = Color.FromArgb("#FFCE00"),
            BorderWidth = 2,
            WidthRequest = 400,
            HorizontalOptions = LayoutOptions.Center
        };

        // Label de erro
        ErrorLabel = new Label
        {
            TextColor = Color.FromArgb("#F95555"),
            IsVisible = false,
            HorizontalOptions = LayoutOptions.Center
        };

        registerButton.Clicked += OnRegisterButtonClickedAsync;
        loginButton.Clicked += OnLoginButtonClicked;

        // Layout principal
        Content = new ScrollView
        {
            Content = new VerticalStackLayout
            {
                Padding = 30,
                Spacing = 15,
                HorizontalOptions = LayoutOptions.Center,
                Children = { logo, UsernameEntry, EmailEntry, PasswordEntry, registerButton, loginButton, ErrorLabel }
            }
        };
    }

    /// <summary>
    /// Evento acionado ao clicar em Registrar
    /// </summary>
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

    /// <summary>
    /// Retorna à tela de login
    /// </summary>
    private void OnLoginButtonClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new LoginView();
    }

    /// <summary>
    /// Exibe mensagem de erro
    /// </summary>
    private void DisplayError(string message)
    {
        ErrorLabel.Text = message;
        ErrorLabel.IsVisible = true;
    }
}

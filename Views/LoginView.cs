using Microsoft.Maui.Controls;
using InvestYOU.Controllers;

namespace InvestYOU.Views;

/// <summary>
/// Tela de login do aplicativo.
/// Permite que o usuário insira email e senha para autenticação.
/// Possui botões para login e para navegar para a tela de registro.
/// </summary>
public class LoginView : ContentPage
{
    private readonly Entry EmailEntry;       // Campo de entrada do email
    private readonly Entry PasswordEntry;    // Campo de entrada da senha
    private readonly Label ErrorLabel;       // Label para exibir erros de login
    private readonly AuthController Controller; // Controller responsável pelo login

    public LoginView()
    {
        Controller = new AuthController();
        BackgroundColor = Color.FromArgb("#0D0D0D"); // Cor de fundo escura

        // Logo do aplicativo
        var logo = new Image
        {
            Source = "investyou.png",
            HorizontalOptions = LayoutOptions.Center,
            HeightRequest = 120
        };

        // Campo de email
        EmailEntry = new Entry
        {
            Placeholder = "Email",
            PlaceholderColor = Colors.Gray,
            TextColor = Colors.White,
            BackgroundColor = Color.FromArgb("#1E1E1E"),
            HorizontalOptions = LayoutOptions.Center,
            WidthRequest = 400,
            Margin = new Thickness(0,5)
        };

        // Campo de senha
        PasswordEntry = new Entry
        {
            Placeholder = "Senha",
            IsPassword = true,
            PlaceholderColor = Colors.Gray,
            TextColor = Colors.White,
            BackgroundColor = Color.FromArgb("#1E1E1E"),
            HorizontalOptions = LayoutOptions.Center,
            WidthRequest = 400,
            Margin = new Thickness(0,5)
        };

        // Botão de login
        var loginButton = new Button
        {
            Text = "Login",
            BackgroundColor = Color.FromArgb("#FFCE00"),
            TextColor = Colors.Black,
            CornerRadius = 8,
            HorizontalOptions = LayoutOptions.Center,
            WidthRequest = 400
        };

        // Botão de registro
        var registerButton = new Button
        {
            Text = "Registrar",
            BackgroundColor = Color.FromArgb("#1E1E1E"),
            TextColor = Color.FromArgb("#FFCE00"),
            CornerRadius = 8,
            BorderColor = Color.FromArgb("#FFCE00"),
            BorderWidth = 2,
            HorizontalOptions = LayoutOptions.Center,
            WidthRequest = 400
        };

        // Label de erro
        ErrorLabel = new Label
        {
            TextColor = Color.FromArgb("#F95555"), // vermelho para erros
            IsVisible = false,
            HorizontalOptions = LayoutOptions.Center
        };

        // Eventos de clique
        loginButton.Clicked += OnLoginButtonClickedAsync;
        registerButton.Clicked += OnRegisterButtonClicked;

        // Layout principal da tela
        Content = new ScrollView
        {
            Content = new VerticalStackLayout
            {
                Padding = 30,
                Spacing = 15,
                HorizontalOptions = LayoutOptions.Center,
                Children = { logo, EmailEntry, PasswordEntry, loginButton, registerButton, ErrorLabel }
            }
        };

        // Impede redimensionamento da janela no Windows
 
    }

    /// <summary>
    /// Evento acionado ao clicar em Login
    /// </summary>
    private async void OnLoginButtonClickedAsync(object sender, EventArgs e)
    {
        var email = EmailEntry.Text;
        var password = PasswordEntry.Text;

        if (await Controller.LoginAsync(email, password))
        {
            // Ao logar com sucesso, navega para a HomeView
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

    /// <summary>
    /// Evento acionado ao clicar em Registrar
    /// </summary>
    private void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new RegisterView();
    }

    /// <summary>
    /// Exibe uma mensagem de erro na tela
    /// </summary>
    private void DisplayError(string message)
    {
        ErrorLabel.Text = message;
        ErrorLabel.IsVisible = true;
    }
}

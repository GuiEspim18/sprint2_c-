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
        BackgroundColor = Color.FromArgb("#0D0D0D");

        var logo = new Image
        {
            Source = "investyou.png",
            HorizontalOptions = LayoutOptions.Center,
            HeightRequest = 120
        };

        EmailEntry = new Entry
        {
            Placeholder = "Email",
            PlaceholderColor = Colors.Gray,
            TextColor = Colors.White,
            BackgroundColor = Color.FromArgb("#1E1E1E"),
            HorizontalOptions = LayoutOptions.Center,
            WidthRequest = Application.Current.MainPage?.Width * 0.8 ?? 400,
            Margin = new Thickness(0,5)
        };

        PasswordEntry = new Entry
        {
            Placeholder = "Senha",
            IsPassword = true,
            PlaceholderColor = Colors.Gray,
            TextColor = Colors.White,
            BackgroundColor = Color.FromArgb("#1E1E1E"),
            HorizontalOptions = LayoutOptions.Center,
            WidthRequest = Application.Current.MainPage?.Width * 0.8 ?? 400,
            Margin = new Thickness(0,5)
        };

        var loginButton = new Button
        {
            Text = "Login",
            BackgroundColor = Color.FromArgb("#FFCE00"),
            TextColor = Colors.Black,
            CornerRadius = 8,
            HorizontalOptions = LayoutOptions.Center,
            WidthRequest = Application.Current.MainPage?.Width * 0.8 ?? 400
        };

        var registerButton = new Button
        {
            Text = "Registrar",
            BackgroundColor = Color.FromArgb("#1E1E1E"),
            TextColor = Color.FromArgb("#FFCE00"),
            CornerRadius = 8,
            BorderColor = Color.FromArgb("#FFCE00"),
            BorderWidth = 2,
            HorizontalOptions = LayoutOptions.Center,
            WidthRequest = Application.Current.MainPage?.Width * 0.8 ?? 400
        };

        ErrorLabel = new Label
        {
            TextColor = Color.FromArgb("#F95555"),
            IsVisible = false,
            HorizontalOptions = LayoutOptions.Center
        };

        loginButton.Clicked += OnLoginButtonClickedAsync;
        registerButton.Clicked += OnRegisterButtonClicked;

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

        // Impede redimensionamento da janela (Windows)
        #if WINDOWS
        this.HandlerChanged += (s, e) =>
        {
            var window = this.Handler.PlatformView as Microsoft.UI.Xaml.Window;
            if (window != null)
            {
                window.MinWidth = 500;
                window.MinHeight = 600;
                window.MaxWidth = 500;
                window.MaxHeight = 600;
            }
        };
        #endif
    }

    private async void OnLoginButtonClickedAsync(object sender, EventArgs e)
    {
        var email = EmailEntry.Text;
        var password = PasswordEntry.Text;

        if (await Controller.LoginAsync(email, password))
        {
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

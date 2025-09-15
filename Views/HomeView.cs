using Microsoft.Maui.Controls;
using InvestYOU.Controllers;
using InvestYOU.Models;

namespace InvestYOU.Views;

/// <summary>
/// Tela principal após login, permitindo a gestão da IA.
/// Contém campos para prompt (editor tipo textarea), slider de temperatura e max tokens.
/// Possui botão de logout que retorna à LoginView.
/// </summary>
public class HomeView : ContentPage
{
    private readonly Editor PromptEditor;       // Campo para digitar prompt
    private readonly Slider TemperatureSlider;  // Slider de temperatura
    private readonly Entry MaxTokensEntry;      // Entrada de max tokens
    private readonly HomeController Controller; // Controller para salvar e carregar dados
    private readonly Label SuccessLabel;        // Label para mostrar sucesso ao salvar

    public HomeView()
    {
        Controller = new HomeController();
        BackgroundColor = Color.FromArgb("#0D0D0D");

        // Logo
        var logo = new Image
        {
            Source = "investyou.png",
            HorizontalOptions = LayoutOptions.Start,
            HeightRequest = 100
        };

        // Botão Logout
        var logoutButton = new Button
        {
            Text = "Logout",
            BackgroundColor = Color.FromArgb("#1E1E1E"),
            TextColor = Color.FromArgb("#FFCE00"),
            CornerRadius = 8,
            BorderColor = Color.FromArgb("#FFCE00"),
            BorderWidth = 2,
            WidthRequest = 100,
            HeightRequest = 40,
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Center
        };
        logoutButton.Clicked += (s, e) =>
        {
            Application.Current.MainPage = new LoginView();
        };

        // Top layout com logo e logout
        var topLayout = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(120) }
            },
            Padding = new Thickness(0,10,0,20)
        };
        topLayout.Add(logo, 0, 0);
        topLayout.Add(logoutButton, 1, 0);

        // Editor grande tipo textarea para prompt
        PromptEditor = new Editor
        {
            Placeholder = "Digite o prompt...",
            PlaceholderColor = Colors.Gray,
            TextColor = Colors.White,
            BackgroundColor = Color.FromArgb("#1E1E1E"),
            HeightRequest = 150,
            WidthRequest = 800,
            HorizontalOptions = LayoutOptions.Center
        };

        // Slider de temperatura
        TemperatureSlider = new Slider(0, 1, 0.5)
        {
            ThumbColor = Color.FromArgb("#FFCE00"),
            MinimumTrackColor = Color.FromArgb("#FFCE00"),
            MaximumTrackColor = Colors.Gray,
            WidthRequest = 800,
            HorizontalOptions = LayoutOptions.Center
        };

        // Entrada de Max Tokens
        MaxTokensEntry = new Entry
        {
            Placeholder = "Max Tokens",
            Keyboard = Keyboard.Numeric,
            PlaceholderColor = Colors.Gray,
            TextColor = Colors.White,
            BackgroundColor = Color.FromArgb("#1E1E1E"),
            WidthRequest = 800,
            HorizontalOptions = LayoutOptions.Center
        };

        // Botão Salvar
        var saveButton = new Button
        {
            Text = "Salvar",
            BackgroundColor = Color.FromArgb("#FFCE00"),
            TextColor = Colors.Black,
            CornerRadius = 8,
            WidthRequest = 800,
            HorizontalOptions = LayoutOptions.Center
        };
        saveButton.Clicked += OnSaveClickedAsync;

        // Label de sucesso
        SuccessLabel = new Label
        {
            TextColor = Color.FromArgb("#0DFF00"),
            IsVisible = false,
            HorizontalOptions = LayoutOptions.Center
        };

        // Layout principal da página
        Content = new ScrollView
        {
            Content = new VerticalStackLayout
            {
                Padding = 20,
                Spacing = 15,
                HorizontalOptions = LayoutOptions.Center,
                Children =
                {
                    topLayout,
                    new Label { Text = "Prompt:", TextColor = Colors.White },
                    PromptEditor,
                    new Label { Text = "Temperatura:", TextColor = Colors.White },
                    TemperatureSlider,
                    new Label { Text = "Max Tokens:", TextColor = Colors.White },
                    MaxTokensEntry,
                    saveButton,
                    SuccessLabel
                }
            }
        };

        // Impede redimensionamento da janela no Windows
        #if WINDOWS
        this.HandlerChanged += (s, e) =>
        {
            var window = this.Handler.PlatformView as Microsoft.UI.Xaml.Window;
            if (window != null)
            {
                window.MinWidth = 600;
                window.MinHeight = 600;
                window.MaxWidth = 600;
                window.MaxHeight = 800;
            }
        };
        #endif
    }

    /// <summary>
    /// Carrega os dados da IA ao abrir a tela
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var data = await Controller.LoadAsync();
        PromptEditor.Text = data.Prompt;
        TemperatureSlider.Value = data.TextTemperature;
        MaxTokensEntry.Text = data.MaxTokens.ToString();
    }

    /// <summary>
    /// Salva os dados do prompt, temperatura e max tokens
    /// </summary>
    private async void OnSaveClickedAsync(object sender, EventArgs e)
    {
        var data = new IAData
        {
            Prompt = PromptEditor.Text ?? "",
            MaxTokens = int.TryParse(MaxTokensEntry.Text, out int tokens) ? tokens : 0,
            TextTemperature = TemperatureSlider.Value
        };

        await Controller.SaveAsync(data);

        SuccessLabel.Text = "Dados salvos com sucesso!";
        SuccessLabel.IsVisible = true;
    }
}

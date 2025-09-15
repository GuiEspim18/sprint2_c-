using Microsoft.Maui.Controls;
using InvestYOU.Controllers;
using InvestYOU.Models;

namespace InvestYOU.Views;

public class HomeView : ContentPage
{
    private readonly Editor PromptEditor;
    private readonly Slider TemperatureSlider;
    private readonly Entry MaxTokensEntry;
    private readonly HomeController Controller;
    private readonly Label SuccessLabel;

    public HomeView()
    {
        Controller = new HomeController();
        BackgroundColor = Color.FromArgb("#0D0D0D");

        var logo = new Image
        {
            Source = "investyou.png",
            HorizontalOptions = LayoutOptions.Center,
            HeightRequest = 100
        };

        PromptEditor = new Editor
        {
            Placeholder = "Digite o prompt...",
            PlaceholderColor = Colors.Gray,
            TextColor = Colors.White,
            BackgroundColor = Color.FromArgb("#1E1E1E")
        };

        TemperatureSlider = new Slider(0, 1, 0.5)
        {
            ThumbColor = Color.FromArgb("#FFCE00"),
            MinimumTrackColor = Color.FromArgb("#FFCE00"),
            MaximumTrackColor = Colors.Gray
        };

        MaxTokensEntry = new Entry
        {
            Placeholder = "Max Tokens",
            Keyboard = Keyboard.Numeric,
            PlaceholderColor = Colors.Gray,
            TextColor = Colors.White,
            BackgroundColor = Color.FromArgb("#1E1E1E")
        };

        var saveButton = new Button
        {
            Text = "Salvar",
            BackgroundColor = Color.FromArgb("#FFCE00"),
            TextColor = Colors.Black,
            CornerRadius = 8
        };
        saveButton.Clicked += OnSaveClickedAsync;

        SuccessLabel = new Label
        {
            TextColor = Color.FromArgb("#0DFF00"),
            IsVisible = false,
            HorizontalOptions = LayoutOptions.Center
        };

        Content = new ScrollView
        {
            Content = new VerticalStackLayout
            {
                Padding = 20,
                Spacing = 15,
                Children =
                {
                    logo,
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
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var data = await Controller.LoadAsync();
        PromptEditor.Text = data.Prompt;
        TemperatureSlider.Value = data.TextTemperature;
        MaxTokensEntry.Text = data.MaxTokens.ToString();
    }

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

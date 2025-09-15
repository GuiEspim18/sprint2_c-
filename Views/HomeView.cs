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

    public HomeView()
    {
        Controller = new HomeController();

        Title = "Home";

        PromptEditor = new Editor { Placeholder = "Digite o prompt..." };
        TemperatureSlider = new Slider(0, 1, 0.5);
        MaxTokensEntry = new Entry { Keyboard = Keyboard.Numeric, Placeholder = "Max Tokens" };

        var saveButton = new Button { Text = "Salvar" };
        saveButton.Clicked += OnSaveClickedAsync;

        Content = new ScrollView
        {
            Content = new VerticalStackLayout
            {
                Padding = 20,
                Spacing = 15,
                Children =
                {
                    new Label { Text = "Prompt:", TextColor = Colors.White },
                    PromptEditor,
                    new Label { Text = "Temperatura:", TextColor = Colors.White },
                    TemperatureSlider,
                    new Label { Text = "Max Tokens:", TextColor = Colors.White },
                    MaxTokensEntry,
                    saveButton
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

        await DisplayAlert("Sucesso", "Dados salvos no CSV!", "OK");
    }
}

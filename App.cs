using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace InvestYOU;

public class App : Application
{
    public App()
    {
        MainPage = new ContentPage
        {
            BackgroundColor = Colors.White,
            Content = new VerticalStackLayout
            {
                Padding = 30,
                Children =
                {
                    new Label {
                        Text = "InvestYOU sem XAML!",
                        HorizontalOptions = LayoutOptions.Center
                    },
                    new Button
                    {
                        Text = "Clique aqui",
                        Command = new Command(() => MainPage.DisplayAlert("Oi", "Funcionou!", "OK"))
                    }
                }
            }
        };
    }
}

using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using InvestYOU.Views.Components;

namespace InvestYOU.Views;

public class MainPage : ContentPage
{
    public MainPage()
    {
        var grid = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = new GridLength(300) }, // menu 100px
                new ColumnDefinition { Width = GridLength.Star }      // resto da tela
            },
            BackgroundColor = Color.FromArgb("#0D0D0D")
        };

        grid.Add(new SideMenu(), 0, 0);
        grid.Add(
            new Label { Text = "Conteúdo principal", TextColor = Colors.White, 
                        HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center },
            1, 0
        );

        Content = grid;
    }
}

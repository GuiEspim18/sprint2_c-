using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace InvestYOU.Views;

class LoginPage : ContentPage
{
    public LoginPage()
    {
        BackgroundColor = Color.FromArgb("#0D0D0D");

        var grid = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
            }
        };

        grid.Add(new VerticalStackLayout
        {
            Background = new LinearGradientBrush(
                    new GradientStopCollection
                    {
                        new GradientStop { Color = Color.FromArgb("#FFCE00"), Offset = 0.0f }, // Amarelo claro
                        new GradientStop { Color = Color.FromArgb("#d4aa00ff"), Offset = 1.0f }  // Amarelo mais forte
                    },
                    new Point(0, 0),  // início (topo-esquerda)
                    new Point(1, 1)   // fim (diagonal para baixo-direita)
            ),


        }, 0, 0);
        grid.Add(new HorizontalStackLayout
        {
            BackgroundColor = Color.FromArgb("#1E1E1E")
        }, 1, 0);

        Content = grid;

    }
}
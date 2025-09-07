using Microsoft.Maui.Controls;

namespace InvestYOU.Views.Components;

class SideMenu : VerticalStackLayout
{
    public SideMenu()
    {
        VerticalOptions = LayoutOptions.Fill; // ocupa toda a altura
        BackgroundColor = Color.FromArgb("#1E1E1E");
        Padding = 10;

        Children.Add(new Label
        {
            Text = "InvestYOU",
            TextColor = Color.FromArgb("#FFCE00"),
            FontSize = 30,
            HorizontalTextAlignment = TextAlignment.Center
        });
        Children.Add(new Button { Text = "Menu 1", TextColor = Colors.White });
        Children.Add(new Button { Text = "Menu 2", TextColor = Colors.White });
        Children.Add(new Button { Text = "Menu 3", TextColor = Colors.White });
    }
}

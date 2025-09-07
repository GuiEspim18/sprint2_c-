using Microsoft.Maui.Controls;

namespace InvestYOU.Views.Components;

class SideMenu : VerticalStackLayout
{
    public SideMenu()
    {
        VerticalOptions = LayoutOptions.Fill; // ocupa toda a altura
        BackgroundColor = Color.FromArgb("#1E1E1E");

        Children.Add(new Button { Text = "Menu 1", TextColor = Colors.White });
        Children.Add(new Button { Text = "Menu 2", TextColor = Colors.White });
        Children.Add(new Button { Text = "Menu 3", TextColor = Colors.White });
    }
}

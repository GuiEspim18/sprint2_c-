using Microsoft.Maui.Controls;
using InvestYOU.Views;

namespace InvestYOU;

public class App : Application
{
    public App()
    {
        // Tela inicial
        MainPage = new NavigationPage(new LoginView())
        {
            BarBackgroundColor = Colors.Black,
            BarTextColor = Colors.White
        };
    }
}

using System.Windows;

namespace ConnectDbWithDotEnv.MVVM.Views;

/// <summary>
/// Interaction logic for MainView.xaml
/// </summary>
public partial class MainView : Window
{
  public MainView(TestView testView)
  {
    InitializeComponent();

    MainFrame.NavigationService.Navigate(testView);
  }
}
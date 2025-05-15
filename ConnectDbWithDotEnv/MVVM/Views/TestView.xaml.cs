using ConnectDbWithDotEnv.MVVM.ViewModels;

namespace ConnectDbWithDotEnv.MVVM.Views;

public partial class TestView
{
  public TestView(TestViewModel viewModel)
  {
    InitializeComponent();
    
    DataContext = viewModel;
  }
}
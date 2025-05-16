using ConnectDbWithDotEnv.MVVM.ViewModels;

namespace ConnectDbWithDotEnv.MVVM.Views;

public partial class ErrorView
{
  public ErrorView(ErrorViewModel viewModel)
  {
    InitializeComponent();

    DataContext = viewModel;
  }
}
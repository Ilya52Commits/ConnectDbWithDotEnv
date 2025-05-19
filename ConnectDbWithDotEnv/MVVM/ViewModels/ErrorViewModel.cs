using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConnectDbWithDotEnv.Services;

namespace ConnectDbWithDotEnv.MVVM.ViewModels;

public partial class ErrorViewModel : ObservableObject
{
  [RelayCommand]
  private void NavigateToMainView()
  {
    
  }
}
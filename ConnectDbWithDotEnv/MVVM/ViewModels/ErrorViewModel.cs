using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using ConnectDbWithDotEnv.EntityFramework.Models;
using ConnectDbWithDotEnv.Repositories.Interfaces;

namespace ConnectDbWithDotEnv.MVVM.ViewModels;

public partial class ErrorViewModel() : ObservableObject
{
  [ObservableProperty] private string _errorMessage = string.Empty;

  
}
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using ConnectDbWithDotEnv.Model;
using ConnectDbWithDotEnv.Repositories.Interfaces;

namespace ConnectDbWithDotEnv.MVVM.ViewModels;

public partial class TestViewModel(IRepository<Test> repository) : ObservableObject
{
  [ObservableProperty] private ObservableCollection<Test> _tests = [];
}
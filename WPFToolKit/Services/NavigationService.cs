using WPFToolKit.Stores;
using WPFToolKit.ViewModels;
using WPFToolKit.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPFToolKit.Services
{
    public class NavigationService<TViewModel> : INavigationService<TViewModel>
        where TViewModel : ViewModelBase
    {
        public NavigationStore NavigationStore { get; }
        public Func<object?, ViewModelBase> ViewModelFactory { get; }

        public NavigationService(NavigationStore navigationStore, Func<object?,TViewModel> viewModelFactory)
        {
            NavigationStore = navigationStore;
            ViewModelFactory = viewModelFactory;
        }

        public void Navigate(object? param)
        {
            NavigationStore.CurrentViewModel = ViewModelFactory(param);
        }
    }
}

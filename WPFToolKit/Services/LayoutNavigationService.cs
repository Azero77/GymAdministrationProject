﻿using WPFToolKit.Stores;
using WPFToolKit.ViewModels;
using WPFToolKit.ComponentsViewModels;
using WPFToolKit.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFToolKit.Services
{
    public class LayoutNavigationService<TViewModel>
        : INavigationService<TViewModel>
        where TViewModel : ViewModelBase
    {
        public LayoutNavigationService(
                                       NavigationStore navigationStore,
                                       Func<object?, TViewModel> contentViewModelFactory,
                                       Func<object?, NavigationBarViewModel> navigationBarViewModelFactory,
                                       Func<object?, MessageViewModel> messageViewModelFactory)
        {
            ContentViewModelFactory = contentViewModelFactory;
            NavigationStore = navigationStore;
            NavigationBarViewModelFactory = navigationBarViewModelFactory;
            MessageViewModelFactory = messageViewModelFactory;
        }

        public Func<object?, TViewModel> ContentViewModelFactory { get; }
        public Func<object?, NavigationBarViewModel> NavigationBarViewModelFactory { get; }
        public Func<object?, MessageViewModel> MessageViewModelFactory { get; }
        public NavigationStore NavigationStore { get; }

        public void Navigate(object? param)
        {
            NavigationStore.CurrentViewModel = new LayoutViewModel(NavigationBarViewModelFactory(param),ContentViewModelFactory(param),MessageViewModelFactory(param));
        }
    }
}

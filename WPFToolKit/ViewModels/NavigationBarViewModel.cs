using WPFToolKit.Commands;
using WPFToolKit.Services;
using WPFToolKit.Stores;
using WPFToolKit.ViewModels;
using WPFToolKit.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace WPFToolKit.ViewModels
{
    public class NavigationBarViewModel : ViewModelBase
    {
        public ICommand HomePageNavigationCommand { get; }
        public ICommand AllAppointmentsNavigationCommand { get; }
        public ICommand AllClientsNavigaitonCommand { get; }
        public NavigationStore NavigationStore { get; }
        public ViewModelBase CurrentViewModel => NavigationStore.CurrentViewModel;
        public NavigationBarViewModel(
            NavigationStore navigationStore,
            INavigationService homePageNavigationService,
            INavigationService allAppointmentsNavigationService,
            INavigationService allClientsNavigationService
            )
        {
            NavigationStore = navigationStore;
            NavigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            HomePageNavigationCommand = new NavigationCommand(homePageNavigationService);
            AllAppointmentsNavigationCommand = new NavigationCommand(allAppointmentsNavigationService);
            AllClientsNavigaitonCommand = new NavigationCommand(allClientsNavigationService);
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}

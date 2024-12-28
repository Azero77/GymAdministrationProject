using WPFToolKit.Commands;
using WPFToolKit.Services;
using WPFToolKit.ComponentsViewModels;
using System.Windows.Input;

namespace WPFToolKit.ViewModels
{
    public class VirtualizedViewModel<T> : ViewModelBase
    {
        public VirtualizedCollectionComponentViewModel<T> ComponentViewModel { get; }
        public ICommand EditItemNavigationCommand { get; }
        public VirtualizedViewModel(
            VirtualizedCollectionComponentViewModel<T> componentViewModel,
            INavigationService editItemNavigationService
            )
        {
            ComponentViewModel = componentViewModel;
            EditItemNavigationCommand = new NavigationCommand(editItemNavigationService);
        }
        public override void Dispose()
        {
            ComponentViewModel.Dispose();
            base.Dispose();
        }
    }
   /* public class AllAppointmentsViewModel : VirtualizedViewModel<Appointment>
    {
        public AllAppointmentsViewModel(VirtualizedCollectionComponentViewModel<Appointment> componentViewModel, INavigationService addItemNavigationService ) : base(componentViewModel, addItemNavigationService)
        {
        }

        
    }

    public class AllClientsViewModel : VirtualizedViewModel<Client>
    {
        public AllClientsViewModel(VirtualizedCollectionComponentViewModel<Client> componentViewModel, INavigationService addItemNavigationService,INavigationService viewItemNavigationService) : base(componentViewModel, addItemNavigationService)
        {
            ViewItemNavigationCommand = new NavigationCommand(viewItemNavigationService);
        }

        public ICommand ViewItemNavigationCommand { get; }
    }*/
}

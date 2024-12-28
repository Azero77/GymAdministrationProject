using WPFToolKit.Services;

namespace WPFToolKit.Commands
{
    public class NavigationCommand : CommandBase
    {
        public INavigationService NavigationService { get; }

        public NavigationCommand(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public override void Execute(object? parameter)
        {
                NavigationService.Navigate(parameter);
        }
    }
}

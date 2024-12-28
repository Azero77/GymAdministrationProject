using WPFToolKit.ViewModels;
using WPFToolKit.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFToolKit.Services
{
    public class CloseModalNavigationService : INavigationService
    {
        public ModalNavigationStore ModalNavigationStore { get; }

        public CloseModalNavigationService(ModalNavigationStore modalNavigationStore)
        {
            ModalNavigationStore = modalNavigationStore;
        }

        public void Navigate(object? param)
        {
            ModalNavigationStore.Close();
        }
    }
}

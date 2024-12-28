using WPFToolKit.ViewModels;
using WPFToolKit.Stores;
using System.Windows.Input;
using WPFToolKit.Commands;

namespace WPFToolKit.ComponentsViewModels
{
    public class MessageViewModel : ViewModelBase
    {
        public MessageStore MessageStore { get; }
        public string Message => MessageStore.CurrentMessage;
        public MessageType MessageType => MessageStore.CurrentMessageType;
        public bool HasMessage => !string.IsNullOrEmpty(Message);
        public ICommand CloseCommand { get; }
        public MessageViewModel(MessageStore messageStore)
        {
            MessageStore = messageStore;
            MessageStore.CurrentMessageChanged += OnCurrentMessageChanged;
            MessageStore.CurrentMessageTypeChanged += OnCurrentMessageTypeChanged;
            CloseCommand = new RelayCommand<object>(
                (o) => MessageStore.ClearMessage()
                );
        }

        private void OnCurrentMessageTypeChanged()
        {
            OnPropertyChanged(nameof(MessageType));
        }

        private void OnCurrentMessageChanged()
        {
            OnPropertyChanged(nameof(Message));
            OnPropertyChanged(nameof(HasMessage));
        }
    }
}

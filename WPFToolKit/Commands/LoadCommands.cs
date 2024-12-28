using WPFToolKit.ViewModels;

namespace WPFToolKit.Commands
{
    public class LoadCommand<T> : AsyncCommandBase
    {
        public CollectionViewModelBase<T> CollectionViewModelBase { get; }
        public LoadCommand(CollectionViewModelBase<T> collectionViewModelBase)
        {
            CollectionViewModelBase = collectionViewModelBase;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                await CollectionViewModelBase.LoadViewModel();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

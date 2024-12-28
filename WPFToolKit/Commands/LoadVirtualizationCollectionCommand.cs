﻿using WPFToolKit.ComponentsViewModels;
using WPFToolKit.Stores;

namespace WPFToolKit.Commands
{
    /// <summary>
    /// Command for loading virtualization
    /// i could be the same for both virtualized and non virtualized if all view models have IsLoading property
    /// and Colleciton.Load() 
    /// </summary>
    //depending on the constructor it may have a store or not
    public class LoadVirtualizationCollectionCommand<T> : AsyncCommandBase
    {
        public LoadVirtualizationCollectionCommand(VirtualizedCollectionComponentViewModel<T> viewModel, ICollectionStore<T>? collectionStore)
        {
            _viewModel = viewModel;
            _collectionStore = collectionStore;
        }

        public VirtualizedCollectionComponentViewModel<T> _viewModel { get; }
        public ICollectionStore<T>? _collectionStore { get; }
        public override async Task ExecuteAsync(object? parameter)
        {
            _viewModel.IsLoading = true;
            try
            {
                if (_collectionStore is not null)
                {
                    await _collectionStore.Load();

                }
                await _viewModel.LoadViewModel();
                
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _viewModel.IsLoading = false;
            }
        }
    }
}

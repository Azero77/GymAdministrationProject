﻿using WPFToolKit.Commands;
using WPFToolKit.ViewModels;
using WPFToolKit.Services;
using WPFToolKit.Services.DataProvider;
using WPFToolKit.Stores;
using WPFToolKit.VirtualizationCollections;
using System.Collections.Specialized;
using System.Windows.Input;

namespace WPFToolKit.ComponentsViewModels
{
    public class VirtualizedCollectionComponentViewModel<T> : CollectionViewModelBase<T>
    {
        private VirtualizationCollection<T> _collection;


        //maybe null if the collection does not have to be stored
        public ICollectionStore<T>? CollectionStore { get; private set; }
        
        public override Task OnProviderChanged()
        {
            _collection.Reload();
            CollectionStore?.ChangeProvider();
            return LoadViewModel();
        }

        public VirtualizedCollectionComponentViewModel(
            VirtualizationCollection<T> collection,
            MessageService messageService,
            IProviderHelper<T> providerHelper,
            ICollectionStore<T>? collectionStore = null)
            : base(collection.ItemsProvider,providerHelper)
        {
            _collection = collection;
            Collection = _collection;
            CollectionStore = collectionStore;
            _collection.CollectionChanged += _collection_CollectionChanged;
            _collection.PropertyChanged += OnPropertyChanged;
            /*if (CollectionStore is not null)
                CollectionStore.CollectionChanged += OnCollectionReset;*/
            Move = new VirtualizationCollectionMoveCommand<T>(this,collection);
            MoveNext = new VirtualizationCollectionMoveCommand<T>(this,collection, moveValue: MoveValue.Next);
            MovePrevious = new VirtualizationCollectionMoveCommand<T>(this,collection, moveValue: MoveValue.Previous);
            ProviderChangerService = new(collection.ItemsProvider,OnProviderChanged);
            SearchCommand = new SearchCommand<T>(this,ProviderChangerService,messageService);
            ResetCommand = new RelayCommand<object>(ResetDelegate);
        }

        private void ResetDelegate(object? obj)
        {
            CollectionProvider.ResetProvider();
            OnProviderChanged();
        }



        #region events

        private void _collection_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(string.Empty);
        }
        private void OnPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e?.PropertyName ?? string.Empty);
        }

        #endregion

        
        #region commands
        public ICommand? Move { get; set; }
        public ICommand? MoveNext { get; set; }
        public ICommand? MovePrevious { get; set; }
        public ICommand? SearchCommand { get; set; }
        public ICommand? OrderCommand { get; set; }
        public ICommand? ResetCommand { get; set; }
        public int CurrentPageIndex => _collection.CurrentPageIndex;
        public int PageSize
        {
            get => _collection.PageSize;
            set
            {
                _collection.PageSize = value;
            }
        }
        public int PagesCount => _collection?.PagesCount ?? 0;
        public List<int> PagesIndexers =>
            MakePageIndexers();
        #endregion

        private List<int> MakePageIndexers()
        {
            List<int> result = new();
            int left = 2;
            int right = 2;

            // Add the current page
            result.Add(CurrentPageIndex);

            // Add numbers from behind if available
            for (int i = 1; i <= left; i++)
            {
                if (CurrentPageIndex - i >= 0)
                {
                    result.Add(CurrentPageIndex - i);
                }
                else if (CurrentPageIndex + right + 1 < PagesCount) // No more behind, add ahead
                {
                    result.Add(CurrentPageIndex + right + 1);
                    right++;
                }
            }

            // Add numbers ahead if available
            for (int i = 1; i <= right; i++)
            {
                if (CurrentPageIndex + i < PagesCount)
                {
                    result.Add(CurrentPageIndex + i);
                }
                else if (CurrentPageIndex - left - 1 >= 0) // No more ahead, add behind
                {
                    result.Add(CurrentPageIndex - left - 1);
                    left++;
                }
            }

            // Always add the last page if it's not already included
            if (!result.Contains(PagesCount - 1))
            {
                result.Add(PagesCount - 1);
            }
            if (!result.Contains(0))
            {
                result.Add(0);
            }
            // Sort and return unique values
            result = result.Distinct().OrderBy(x => x).ToList();

            return result;
        }

        public override void Dispose()
        {
            _collection.CollectionChanged -= _collection_CollectionChanged;
            _collection.PropertyChanged -= OnPropertyChanged;
            (Move as IDisposable)?.Dispose();
            (MoveNext as IDisposable)?.Dispose();
            (MovePrevious as IDisposable)?.Dispose();
            ProviderChangerService.ProviderChanged -= OnProviderChanged;
            base.Dispose();
        }
        //initializing a new virtualization view model since there is no constuctor 
        // can be used to pass the view model when view is made
        /// <summary>
        /// Factory Method For Corresponding ViewModel
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>


        public static VirtualizedCollectionComponentViewModel<T> LoadVirtualizedCollectionComponentViewModel(
            VirtualizationCollection<T> collection,
            MessageService messageService,
            IProviderHelper<T> providerHelper,
            ICollectionStore<T>? collectionStore = null
            )
        {
            VirtualizedCollectionComponentViewModel<T> vm = new(collection,messageService,providerHelper,collectionStore);
            return (VirtualizedCollectionComponentViewModel<T>) LoadCollectionViewModel(vm);
        }

        public override async Task LoadViewModel()
        {
            IsLoading = true;
            if (CollectionStore is null)
                await _collection.Load();
            else
                await CollectionStore.Load();
            IsLoading = false;
        }

    }
    /*public class VirtualizedClientsComponentViewModel : VirtualizedCollectionComponentViewModel<Client>
    {
        public VirtualizedClientsComponentViewModel(VirtualizationCollection<Client> collection, MessageService messageService,IProviderHelper<Client> ph ,ICollectionStore<Client>? collectionStore = null) : base(collection, messageService,ph, collectionStore)
        {
        }
    }*/
}

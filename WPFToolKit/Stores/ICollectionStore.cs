﻿using WPFToolKit.Services.DataProvider;

namespace WPFToolKit.Stores
{

    public interface ICollectionStore<T>
    {
        /// <summary>
        /// Collection To Store in the app lifeCycle
        /// </summary>
        public IEnumerable<T> Collection { get;}

        /// <summary>
        /// Provider to get the collection by loading and storing and manipulatin
        /// </summary>
        public IProvider<T> Provider { get; }


        /// <summary>
        /// Event raised by the collection to Notify view models to update
        /// </summary>
        event Action CollectionChanged;
        /// <summary>
        /// EventHandler for IDataManipulator.DataManipulated to raise THe collectionChangedEvent
        /// </summary>
        void OnDataManipulated();

        /// <summary>
        /// Changing provider for searching and sorting or grouping
        /// </summary>
        void ChangeProvider();

        Task Load();
    }
}

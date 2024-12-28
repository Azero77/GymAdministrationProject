﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFToolKit.Services.DataProvider
{
    public interface IVirtualizationItemsProvider<T> : IProvider<T>
    {
        /// <summary>
        /// Method to return number of elements in the context
        /// </summary>
        /// <returns></returns>
        Task<int> FetchCount();

        /// <summary>
        /// Fetches from <paramref name="start"/> a <paramref name="size"/> of items
        /// </summary>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        Task<IList<T>> FetchRange(int start, int size);

        /// <summary>
        /// Method for getting new provider and adding whereClause for searching
        /// </summary>
        /// <param name="sqlFetchCount"></param>
        /// <param name="sqlFetchRange"></param>
        /// <returns></returns>
        
    }
}

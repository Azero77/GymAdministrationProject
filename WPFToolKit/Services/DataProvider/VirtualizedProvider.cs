﻿using AutoMapper;
using Configurations.DataContext;
using Dapper;
using WPFToolKit.Stores;
using System.Data.SQLite;

namespace WPFToolKit.Services.DataProvider
{
    public abstract class VirtualizedProvider<T, TDTO>
        : IVirtualizationItemsProvider<T>
    {
        public DbContext DataContext { get; }
        public MessageService MessageService { get; }

        public readonly IMapper _mapper;
        protected string? whereClause = null;
        protected string? orderByClause = null;
        Lazy<Task<int>> _initializeCount;
        public VirtualizedProvider(DbContext dbContext,
                                    IMapper mapper,
                                    MessageService messageService,
                                    string? whereClause = null,
                                    string? orderByClause = null)
        {
            _initializeCount = new Lazy<Task<int>>(InitializeCount);
            DataContext = dbContext;
            _mapper = mapper;
            MessageService = messageService;
            this.whereClause = whereClause ?? this.whereClause;
            this.orderByClause = orderByClause ?? this.orderByClause;
            //if the sql was not provided then the provider will the table of T and get all items virtualized
        }



        protected virtual async Task<int> InitializeCount()
        {
            string tableName = typeof(T).Name + "s";

            string sql = $"SELECT COUNT(*) FROM {tableName} " +
                $"WHERE {whereClause};";
            return await RunSqlCount(sql);
        }

        protected async Task<int> RunSqlCount(string sql)
        {
            return await DataContext.RunAsync<int>(async conn =>
            {
                try
                {
                    return await conn.ExecuteScalarAsync<int>(sql);
                }
                catch (SQLiteException)
                {
                    MessageService.SetMessage("Search Schema is not availabe",
                        MessageType.Error);
                    return 0;
                }
            });
        }

        public async Task<int> FetchCount()
        {
            return await _initializeCount.Value;
        }

        public async Task<IList<T>> FetchRange(int start, int size)
        {
            return (await GetItems(start, size)).ToList();
        }
        

        public async Task<IEnumerable<T>> GetItems()
        {
            return await FetchRange(0, 20);
        }

        public virtual async Task<IEnumerable<T>> GetItems(int start, int size)
        {
            string tableName = typeof(T).Name + "s";
            string sql = $"SELECT * FROM {tableName} " +
                $"WHERE {whereClause} " +
                $"ORDER BY {orderByClause} " +
                $"LIMIT @size OFFSET @start;";
            return await RunSqlRange(start, size, sql);
        }

        protected async Task<IEnumerable<T>> RunSqlRange(int start, int size, string sql)
        {
            object param = new
            {
                start = start,
                size = size
            };
            IEnumerable<TDTO> typeDTOs = await DataContext.RunAsync<IEnumerable<TDTO>>(async conn =>
            {
                try
                {
                    return await conn.QueryAsync<TDTO>(sql, param);
                }
                catch (SQLiteException)
                {
                    MessageService.SetMessage("Search Schema is not allowed",
                        MessageType.Error);
                    return Enumerable.Empty<TDTO>();
                }
            });
            return typeDTOs.Select(cDTO => _mapper.Map<T>(cDTO));
        }

        //change provider for both order by or where clause depending on what is null
        public virtual void ChangeProvider
            (string? whereClause,
            string? orderByClause)
        {
            this.whereClause = whereClause ?? this.whereClause;
            this.orderByClause = orderByClause ?? this.orderByClause;
            _initializeCount = new(InitializeCount);
        }
        public void ResetProvider()
        {
            whereClause = null;
            orderByClause = null;
            _initializeCount = new Lazy<Task<int>>(InitializeCount);
        }

        //no need for provider single item for search in Virtualization Provider
        public Task<T?> GetItem(int id)
        {
            throw new NotImplementedException();
        }
    }
}

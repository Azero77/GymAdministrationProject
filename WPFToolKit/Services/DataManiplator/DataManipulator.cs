using Configurations.DataContext;
using Dapper;
using System.Runtime.Serialization;

namespace WPFToolKit.Services.DataManiplator
{
    public class DataManipulator : IDataManipulator
    {
        public DbContext DbContext { get; }

        public DataManipulator(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public event Action? DataManipulated;

        
        public async Task Manipulate(string sql, object param)
        {
            await DbContext.RunAsync<int>(async conn =>
            {
                int result = await conn.ExecuteAsync(sql, param);
                if (result == 1)
                {
                    OnDataManipulated();
                    return result;
                }
                throw new InvalidDataContractException();
            });
        }

        public void OnDataManipulated()
        {
            DataManipulated?.Invoke();
        }
    }
}

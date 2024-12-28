using Configurations.DataContext;

namespace WPFToolKit.Services.DataManiplator
{
    //one interface and one manipulator for the lifecycle of the app
    public interface IDataManipulator
    {
        DbContext DbContext { get; }
        Task Manipulate(string sql,object param);
        event Action? DataManipulated;

    }

}

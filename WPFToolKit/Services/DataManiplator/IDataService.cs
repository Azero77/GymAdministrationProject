namespace WPFToolKit.Services.DataManiplator
{
    public interface IDataService<T>
    {
        IDataManipulator Manipulator { get; }
        Task CreateAsync(T item);
        Task EditAsync(T item);
        Task DeleteAsync(T item);

    }
}

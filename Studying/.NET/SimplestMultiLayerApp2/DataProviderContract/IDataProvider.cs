namespace DataProviderContract
{
    public interface IDataProvider<T>
    {
        void Write(T data);
        T Read();
    }
}

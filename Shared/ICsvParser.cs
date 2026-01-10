namespace HomeAppliancesStore.Shared
{
    public interface ICsvParser<T>
    {
        T Parse(string line);
        string ToCsv(T entity);
    }
}
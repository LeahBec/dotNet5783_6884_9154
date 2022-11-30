namespace DalApi;

/// <summary>
/// the interface implement all base CRUD functions for the all 3 main entities.
/// </summary>
/// <typeparam name="T">template, Product or Order or OrderItem</typeparam>

public interface ICrud<T>
{
    public int Add(T obj);
    public void Delete(int id);
    public void Update(T obj);
    public IEnumerable<T> GetAll();
    public T Get(int id);
}

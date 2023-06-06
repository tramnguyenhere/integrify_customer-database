namespace CustomerDatabaseManagement;

interface IBase
{
    public int Id { get; set; }
}

class CustomerDatabase<T>
where T : IBase
{
    private List<T> _customerCollection;

    public CustomerDatabase()
    {
        _customerCollection = new List<T>();
    }

    public T Insert(T customer)
    {
        var id = 0;
        if (_customerCollection.Count > 0)
        {
            id = _customerCollection[^1].Id + 1;
        }
        customer.Id = id;
        _customerCollection.Add(customer);
        Console.WriteLine("Customer is added successfully!");
        return customer;
    }

    public bool Delete(int id)
    {
        var customer = _customerCollection.Find(customer => customer.Id == id);
        if (customer != null)
        {
            _customerCollection.Remove(customer);
            Console.WriteLine("Customer is removed successfully!");
            return true;
        }
        else
        {
            Console.WriteLine("Customer has been deleted already!");
            return false;
        }
    }

    public override string ToString()
    {
        var result = "";
        foreach (T customer in _customerCollection)
        {
            result += customer.ToString();
        }
        return result;
    }
}
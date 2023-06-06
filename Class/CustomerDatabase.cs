namespace DatabaseManagement;

class CustomerDatabase<T>
where T : ICustomer
{
    private List<T> _customerCollection;

    public CustomerDatabase()
    {
        _customerCollection = new List<T>();
    }

    public T Insert(T customer)
    {
        bool isEmailUnique = !_customerCollection.Any(customer => customer.Email == customer.Email);
        var id = 0;

        if (isEmailUnique)
        {
            if (_customerCollection.Count > 0)
            {
                id = _customerCollection[^1].Id + 1;
            }
            customer.Id = id;
            _customerCollection.Add(customer);
            Console.WriteLine("Customer is added successfully!");
            return customer;
        }
        else
        {
            throw new Exception("The customer's email already exists");
        }
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

    public T GetCustomerById(int id)
    {
        var customer = _customerCollection.FirstOrDefault(customer => customer.Id == id);
        if (customer != null)
        {
            return customer;
        }
        else
        {
            throw new Exception("The customer cannot be found!");
        }
    }

    public T SearchCustomersById(int id)
    {
        var searchResults = _customerCollection.Find(customer => customer.Id == id);
        if (searchResults != null)
        {
            return searchResults;
        }
        else
        {
            throw new Exception("The customer cannot be found!");
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
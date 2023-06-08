namespace DatabaseManagement;

class CustomerDatabase<T>
where T : ICustomer
{
    private List<T> _customerCollection;
    private Stack<T[]> _undoStack;
    private Stack<T[]> _redoStack;
    private const string FilePath = "customers.csv";

    public CustomerDatabase()
    {
        _customerCollection = new List<T>();
        _undoStack = new Stack<T[]>();
        _redoStack = new Stack<T[]>();
    }

    public void Insert(T customer)
    {
        var id = 0;

        if (!_customerCollection.Any(customer => customer.Email == customer.Email))
        {
            if (_customerCollection.Count > 0)
            {
                id = _customerCollection[^1].Id + 1;
            }
            customer.Id = id;
            _customerCollection.Add(customer);
            Console.WriteLine("Customer is added successfully!");
        }
        else
        {
            ExceptionHandler.HandleException("Email must be unique.");
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
            ExceptionHandler.HandleException("The customer has already removed!");
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
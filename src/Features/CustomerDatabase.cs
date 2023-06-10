using System.Collections;

namespace DatabaseManagement;

class CustomerDatabase<T> : IEnumerable<T>
where T : ICustomer
{
    private List<T> _customerCollection;
    private Stack<List<T>> _undoStack;
    private Stack<List<T>> _redoStack;

    public CustomerDatabase()
    {
        _customerCollection = new List<T>();
        _undoStack = new Stack<List<T>>();
        _redoStack = new Stack<List<T>>();
    }

    public async Task Insert(T customer)
    {
        var lines = File.ReadAllLines("customers.csv");
        int id = 0;
        if (File.Exists("customers.csv"))
        {
            if (lines.Length > 0)
            {
                var lastLine = lines[^1];
                var lastId = int.Parse(lastLine.Split(',')[0]);
                id = lastId + 1;
            }
        }

        foreach (string line in lines)
        {
            if (line.Contains(customer.Email))
            {
                ExceptionHandler.FileException("Email must be unique.");
            }
            else
            {
                customer.Id = id;
                _customerCollection.Add(customer);
                Console.WriteLine("Customer is added successfully!");
                await AddNewCustomer($"{id}, {customer.FirstName}, {customer.LastName}, {customer.Email}, {customer.Address}");
                ClearRedoStack();
                _undoStack.Push(new List<T>(_customerCollection));
            }
        }
        // foreach (string line in lines)
        // {
        //     if (line.Contains(customer.Email))
        //     {
        //         ExceptionHandler.FileException("Email must be unique.");
        //     }
        //     else
        //     {
        //         customer.Id = id;
        //         _customerCollection.Add(customer);
        //         Console.WriteLine("Customer is added successfully!");
        //         await AddNewCustomer($"{id}, {customer.FirstName}, {customer.LastName}, {customer.Email}, {customer.Address}");
        //         ClearRedoStack();
        //         _undoStack.Push(new List<T>(_customerCollection));
        //     }
        // }
    }

    public static async Task AddNewCustomer(string newCustomer)
    {
        var fh = new FileHelper("customers.csv");
        fh.GetAll();
        await fh.AddNewAsync(newCustomer);
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
            ExceptionHandler.FileException("The customer has already removed!");
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

    private void ClearRedoStack()
    {
        _redoStack.Clear();
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (T customer in _customerCollection)
        {
            yield return customer;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
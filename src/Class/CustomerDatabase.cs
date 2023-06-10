using System.Collections;

namespace DatabaseManagement;

class CustomerDatabase<T> : IEnumerable<T>
where T : ICustomer
{
    private List<T> _customerCollection;
    private Stack<T> _undoStack;
    private Stack<T> _redoStack;

    public CustomerDatabase()
    {
        _customerCollection = new List<T>();
        _undoStack = new Stack<T>();
        _redoStack = new Stack<T>();
    }


    public void Insert(T customer)
    {
        var lines = File.ReadAllLines("customers.csv");
        var inputEmail = customer.Email;

        bool emailExists = lines.Any(line => line.Split(',')[3] == inputEmail);
        if (emailExists)
        {
            ExceptionHandler.HandleException(new Exception("Email must be unique."));
            return;
        }

        int newId = lines.Length > 0 ? lines.Length : 0;
        customer.Id = newId;

        string newLine = $"{newId},{customer.FirstName},{customer.LastName},{customer.Email},{customer.Address}";

        var newLines = lines.ToList();
        newLines.Add(newLine);

        File.WriteAllLines("customers.csv", newLines);
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
            ExceptionHandler.HandleException(new Exception("The customer has already removed!"));
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
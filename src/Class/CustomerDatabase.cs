namespace DatabaseManagement;
using System.Collections;

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
        var inputEmail = customer.Email;
        var lines = File.ReadAllLines("customers.csv");
        bool emailExists = lines.Any(line => line.Split(',')[3] == inputEmail);
        if (emailExists)
        {
            Console.WriteLine("hui");
            ExceptionHandler.UpdateDataException("Email must be unique.");
            return;
        }

        int newId = lines.Length > 0 ? Convert.ToInt32(lines.Last().Split(',')[0]) + 1 : 0;
        customer.Id = newId;

        string newLine = $"{newId},{customer.FirstName},{customer.LastName},{customer.Email},{customer.Address}";

        var newLines = lines.ToList();
        newLines.Add(newLine);

        File.WriteAllLines("customers.csv", newLines);
    }

    public void Update(int customerId, T updatedCustomer)
    {
        var inputEmail = updatedCustomer.Email;
        var lines = File.ReadAllLines("customers.csv");
        // bool emailExists = lines.Any(line => line.Split(',')[3] == inputEmail);

        // if (emailExists)
        // {
        //     ExceptionHandler.UpdateDataException("Email must be unique.");
        //     return;
        // }

        int lineIndex = -1;
        for (int i = 0; i < lines.Length; i++)
        {
            var parts = lines[i].Split(',');
            if (parts.Length == 5 && int.Parse(parts[0]) == customerId)
            {
                lineIndex = i;
                break;
            }
        }

        if (lineIndex != -1)
        {
            lines[lineIndex] = $"{customerId},{updatedCustomer.FirstName},{updatedCustomer.LastName},{updatedCustomer.Email},{updatedCustomer.Address}";

            File.WriteAllLines("customers.csv", lines);
        }
        else
        {
            ExceptionHandler.UpdateDataException("Customer not found.");
        }
    }

    public bool Delete(int customerId)
    {
        var customer = _customerCollection.Find(customer => customer.Id == customerId);
        if (customer != null)
        {
            _customerCollection.Remove(customer);
            Console.WriteLine("Customer is removed successfully!");
            return true;
        }
        else
        {
            ExceptionHandler.UpdateDataException("The customer has already removed!");
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
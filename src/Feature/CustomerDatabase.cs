namespace DatabaseManagement;
using System.Collections;

class CustomerDatabase<T> : IEnumerable<T>
where T : ICustomer
{
    private List<T> _customerCollection;
    private List<string> _lines;

    public CustomerDatabase()
    {
        _customerCollection = new List<T>();
        _lines = File.ReadAllLines("customers.csv").ToList();
    }

    public void Insert(T customer)
    {
        var inputEmail = customer.Email;
        bool emailExists = _lines.Any(line => line.Split(',')[3] == inputEmail);
        if (emailExists)
        {
            ExceptionHandler.UpdateDataException("Email must be unique.");
            return;
        }

        int newId = _lines.Count > 0 ? int.Parse(_lines.Last().Split(',')[0]) + 1 : 0;
        customer.Id = newId;
        _customerCollection.Add(customer);

        string newLine = $"{newId},{customer.FirstName},{customer.LastName},{customer.Email},{customer.Address}";

        _lines.Add(newLine);
        FileHelper.SaveCustomerToFile(_lines);
    }

    public void Update(int customerId, T updatedCustomer)
    {
        var inputEmail = updatedCustomer.Email;
        bool emailExists = _lines.Any(line => line.Split(',')[3] == inputEmail && Convert.ToInt32(line.Split(',')[0]) != customerId);

        if (emailExists)
        {
            ExceptionHandler.UpdateDataException("Email must be unique.");
            return;
        }

        int lineIndex = -1;
        for (int i = 0; i < _lines.Count; i++)
        {
            var parts = _lines[i].Split(',');
            if (parts.Length == 5 && int.Parse(parts[0]) == customerId)
            {
                lineIndex = i;
                break;
            }
        }

        if (lineIndex != -1)
        {
            _lines[lineIndex] = $"{customerId},{updatedCustomer.FirstName},{updatedCustomer.LastName},{updatedCustomer.Email},{updatedCustomer.Address}";

            foreach (var customer in _customerCollection)
            {
                if (customer.Id == customerId)
                {
                    customer.FirstName = updatedCustomer.FirstName;
                    customer.LastName = updatedCustomer.LastName;
                    customer.Email = updatedCustomer.Email;
                    customer.Address = updatedCustomer.Address;
                }
            }
            FileHelper.SaveCustomerToFile(_lines);
        }
        else
        {
            ExceptionHandler.UpdateDataException("Customer not found.");
            return;
        }
    }

    public void Delete(int customerId)
    {
        int lineIndex = -1;
        for (int i = 0; i < _lines.Count; i++)
        {
            var parts = _lines[i].Split(',');
            if (int.Parse(parts[0]) == customerId)
            {
                lineIndex = i;
                break;
            }
        }

        if (lineIndex != -1)
        {
            _lines.RemoveAt(lineIndex);
            FileHelper.SaveCustomerToFile(_lines);
        }
        else
        {
            ExceptionHandler.UpdateDataException("Customer not found.");
            return;
        }
    }


    public string GetCustomerById(int id)
    {
        var searchResult = _lines.First(line => Convert.ToInt32(line.Split(",")[0]) == id);
        if (searchResult != null)
        {
            Console.WriteLine(searchResult);
            return searchResult;
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
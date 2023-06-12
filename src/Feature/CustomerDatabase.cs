namespace DatabaseManagement;
using System.Collections;

class CustomerDatabase<Customer>
{
    private List<ICustomer> _customerCollection;
    private List<string> _lines;

    public CustomerDatabase()
    {
        _customerCollection = new List<ICustomer>();
        _lines = File.ReadAllLines("customers.csv").ToList();
    }

    public void Insert(ICustomer customer)
    {
        customer.Id = Utils.GenerateId(_lines);
        if (Utils.IsEmailAvailable(_lines, customer.Email))
        {
            ExceptionHandler.UpdateDataException("Error! Email must be unique.");
            return;
        }

        _customerCollection.Add(customer);

        _lines.Add(customer?.ToString() ?? string.Empty);
        FileHelper.SaveCustomerToFile(_lines);
    }

    public void Update(int customerId, ICustomer updatedCustomer)
    {
        if (Utils.IsEmailAvailable(_lines, updatedCustomer.Email, customerId))
        {
            ExceptionHandler.UpdateDataException("Error! Email must be unique.");
            return;
        }

        int lineIndex = Utils.FindLineIndex(_lines, customerId);

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
        int lineIndex = Utils.FindLineIndex(_lines, customerId);

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
        foreach (ICustomer customer in _customerCollection)
        {
            result += customer.ToString();
        }
        return result;
    }
}
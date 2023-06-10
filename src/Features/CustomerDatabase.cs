using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DatabaseManagement;

class CustomerDatabase
{
    private List<ICustomer> _customerCollection;
    private Stack<List<ICustomer>> _undoStack;
    private Stack<List<ICustomer>> _redoStack;
    private const string FilePath = "customers.csv";

    public CustomerDatabase()
    {
        _customerCollection = new List<ICustomer>();
        _undoStack = new Stack<List<ICustomer>>();
        _redoStack = new Stack<List<ICustomer>>();

        LoadCustomersFromFile();
    }
    public void AddCustomer(Customer customer)
    {
        if (_customerCollection.Any(c => c.Email == customer.Email))
        {
            ExceptionHandler.FileException("Customer with the same email already exists.");
        }

        int newId = GenerateNewId();
        customer.Id = newId;

        _customerCollection.Add(customer);
        _undoStack.Push(new List<ICustomer>(_customerCollection));
        _redoStack.Clear();

        SaveCustomersToFile();
    }

    public void UpdateCustomer(int customerId, ICustomer updatedCustomer)
    {
        var existingCustomer = _customerCollection.FirstOrDefault(c => c.Id == customerId);
        if (existingCustomer == null)
        {
            ExceptionHandler.FileException("Customer not found.");
        }
        else
        {
            if (existingCustomer?.Email != updatedCustomer.Email &&
                _customerCollection.Any(c => c.Email == updatedCustomer.Email))
            {
                ExceptionHandler.FileException("Another customer with the same email already exists.");
            }
            else
            {
                existingCustomer!.FirstName = updatedCustomer.FirstName;
                existingCustomer.LastName = updatedCustomer.LastName;
                existingCustomer.Email = updatedCustomer.Email;
                existingCustomer.Address = updatedCustomer.Address;

                _undoStack.Push(new List<ICustomer>(_customerCollection));
                _redoStack.Clear();

                SaveCustomersToFile();
            }

        }

    }

    public void DeleteCustomer(int customerId)
    {
        var existingCustomer = _customerCollection.FirstOrDefault(c => c.Id == customerId);
        if (existingCustomer == null)
        {
            ExceptionHandler.FileException("Customer not found.");
        }
        else
        {
            _customerCollection.Remove(existingCustomer);
            _undoStack.Push(new List<ICustomer>(_customerCollection));
            _redoStack.Clear();

            SaveCustomersToFile();
        }

    }

    public ICustomer GetCustomerById(int customerId)
    {
        return _customerCollection?.FirstOrDefault(c => c.Id == customerId);
    }

    public IEnumerable<ICustomer> SearchCustomersById(int customerId)
    {
        return _customerCollection.Where(c => c.Id == customerId);
    }

    public void Undo()
    {
        if (_undoStack.Count == 0)
        {
            throw new InvalidOperationException("Nothing to undo.");
        }

        var previousState = _undoStack.Pop();
        _redoStack.Push(new List<ICustomer>(_customerCollection));
        _customerCollection = previousState;

        SaveCustomersToFile();
    }

    public void Redo()
    {
        if (_redoStack.Count == 0)
        {
            throw new InvalidOperationException("Nothing to redo.");
        }

        var nextState = _redoStack.Pop();
        _undoStack.Push(new List<ICustomer>(_customerCollection));
        _customerCollection = nextState;

        SaveCustomersToFile();
    }

    private void LoadCustomersFromFile()
    {
        if (File.Exists(FilePath))
        {
            try
            {
                var lines = File.ReadAllLines(FilePath);
                foreach (var line in lines)
                {
                    var customerData = line.Split(',');
                    if (customerData.Length == 5)
                    {
                        var customer = new Customer(Convert.ToInt32(customerData[0]), customerData[1], customerData[2], customerData[3], customerData[4]);
                        _customerCollection.Add(customer);
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandler.FetchDataException(exception.Message);
            }
        }
    }

    private void SaveCustomersToFile()
    {
        try
        {
            using (var writer = new StreamWriter(FilePath))
            {
                foreach (var customer in _customerCollection)
                {
                    var line = $"{customer.Id},{customer.FirstName},{customer.LastName},{customer.Email},{customer.Address}";
                    writer.WriteLine(line);
                }
            }
        }
        catch (Exception exception)
        {
            ExceptionHandler.UpdateDataException(exception.Message);
        }
    }

    private int GenerateNewId()
    {
        var id = _customerCollection.Count > 0 ? _customerCollection[^1].Id + 1 : 0;
        return id;
    }
}


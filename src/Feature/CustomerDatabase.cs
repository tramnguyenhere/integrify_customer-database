namespace DatabaseManagement;

class CustomerDatabase
{
    private List<Customer> _customerCollection;
    private List<string> _lines;
    private Stack<Step> _undoStack;
    private Stack<Step> _redoStack;

    public CustomerDatabase()
    {
        _customerCollection = new List<Customer>();
        _lines = File.ReadAllLines("customers.csv").ToList();
        _undoStack = new Stack<Step>();
        _redoStack = new Stack<Step>();
    }

    public void Insert(Customer customer)
    {
        int generatedId = Utils.GenerateId(_lines);
        customer = new Customer(generatedId, customer.FirstName, customer.LastName, customer.Email, customer.Address);


        if (Utils.IsEmailAvailable(_lines, customer.Email))
        {
            Console.WriteLine("Error! Email must be unique.");
            return;
        }

        _customerCollection.Add(customer);

        FileHelper.InsertToFile(customer, _lines);

        _undoStack.Push(new Step(Action.Insert, customer));
        _redoStack.Clear();
    }

    public void Update(int customerId, Customer updatedCustomer)
    {
        if (Utils.IsEmailAvailable(_lines, updatedCustomer.Email, customerId))
        {
            Console.WriteLine("Error! Email must be unique.");
            return;
        }

        int lineIndex = Utils.FindLineIndex(_lines, customerId);

        if (lineIndex != -1)
        {
            string originalData = _lines[lineIndex];

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

            FileHelper.UpdateInFile(customerId, updatedCustomer, _lines);

            Customer originalCustomer = new Customer(customerId, originalData.Split(',')[1], originalData.Split(',')[2], originalData.Split(',')[3], originalData.Split(',')[4]);
            _undoStack.Push(new Step(Action.Update, originalCustomer));
            _redoStack.Clear();
        }
        else
        {
            Console.WriteLine("Customer not found.");
        }
    }

    public void Delete(int customerId)
    {
        int lineIndex = Utils.FindLineIndex(_lines, customerId);

        if (lineIndex != -1)
        {
            string deletedData = _lines[lineIndex];

            _lines.RemoveAt(lineIndex);

            FileHelper.DeleteFromFile(customerId, _lines);

            Customer deletedCustomer = new Customer(customerId, deletedData.Split(',')[1], deletedData.Split(',')[2], deletedData.Split(',')[3], deletedData.Split(',')[4]);
            _undoStack.Push(new Step(Action.Delete, deletedCustomer));
            _redoStack.Clear();
        }
        else
        {
            Console.WriteLine("Customer not found.");
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

    public void Undo()
    {
        if (_undoStack.Count > 0)
        {
            Step stepToUndo = _undoStack.Pop();
            switch (stepToUndo.Action)
            {
                case Action.Insert:
                    Delete(stepToUndo.Customer.Id);
                    FileHelper.DeleteFromFile(stepToUndo.Customer.Id, _lines);
                    break;
                case Action.Update:
                    Update(stepToUndo.Customer.Id, stepToUndo.Customer);
                    FileHelper.UpdateInFile(stepToUndo.Customer.Id, stepToUndo.Customer, _lines);
                    break;
                case Action.Delete:
                    Insert(stepToUndo.Customer);
                    FileHelper.InsertToFile(stepToUndo.Customer, _lines);
                    break;
            }
            _redoStack.Push(stepToUndo);
        }
        else
        {
            Console.WriteLine("Nothing to undo.");
        }
    }

    public void Redo()
    {
        if (_redoStack.Count > 0)
        {
            Step stepToRedo = _redoStack.Pop();
            switch (stepToRedo.Action)
            {
                case Action.Insert:
                    Insert(stepToRedo.Customer);
                    FileHelper.InsertToFile(stepToRedo.Customer, _lines);
                    break;
                case Action.Update:
                    Update(stepToRedo.Customer.Id, stepToRedo.Customer);
                    FileHelper.UpdateInFile(stepToRedo.Customer.Id, stepToRedo.Customer, _lines);
                    break;
                case Action.Delete:
                    Delete(stepToRedo.Customer.Id);
                    FileHelper.DeleteFromFile(stepToRedo.Customer.Id, _lines);
                    break;
            }
            _undoStack.Push(stepToRedo);
        }
        else
        {
            Console.WriteLine("Nothing to redo.");
        }
    }


    public override string ToString()
    {
        var result = "";
        foreach (Customer customer in _customerCollection)
        {
            result += customer.ToString() + Environment.NewLine;
        }
        return result;
    }
}
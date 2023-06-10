namespace DatabaseManagement;

class FileHelper
{
    private const string FilePath = "customers.csv";
    public static void CreateCustomerFile()
    {
        if (!File.Exists(FilePath))
        {
            File.Create(FilePath).Close();
        }
    }

    public static CustomerDatabase<ICustomer> ReadCustomersFromFile()
    {
        var customerCollection = new CustomerDatabase<ICustomer>();

        try
        {
            string[] lines = File.ReadAllLines(FilePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                var customer = new Customer(parts[1], parts[2], parts[3], parts[4]);
                customerCollection.Insert(customer);
            }

        }
        catch (Exception exception)
        {
            ExceptionHandler.HandleException(new Exception($"Error while reading customer file: {exception}"));
        }

        return customerCollection;
    }

    public static void WriteCustomersToFile(CustomerDatabase<ICustomer> customerCollection)
    {
        try
        {
            List<string> lines = new List<string>();
            foreach (ICustomer customer in customerCollection)
            {
                string line = $"Customer Id: {customer.Id}, Name: {customer.FirstName} {customer.LastName}, Email: {customer.Email}, Address: {customer.Address}";
                lines.Add(line);
            }

            File.WriteAllLines(FilePath, lines);
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Error while writing customers to the file: {exception.Message}");
        }
    }
}
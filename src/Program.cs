using DatabaseManagement;

// var fileInfo = new FileInfo("customers.csv");
// var customerDatabase = new CustomerDatabase<ICustomer>();
// var customer1 = new Customer("Tram", "Nguyen", "tram@mail.com", "Olympiakatu 12 C1");
// customerDatabase.Insert(customer1);
// // Console.WriteLine(customerDatabase);

// FileHelper.CreateCustomerFile();
// FileHelper.ReadCustomersFromFile();

public class Program
{
    public static async Task Main()
    {
        var customerDatabase = new CustomerDatabase<ICustomer>();
        var customer1 = new Customer("Tram", "Nguyen", "tram@mail.com", "Olympiakatu 12 C1");
        // var customer2 = new Customer("Phuc", "Le", "phuc@mail.com", "Olympiakatu 12 C1");

        await customerDatabase.Insert(new Customer("Phuc", "Le", "phuc@mail.com", "Olympiakatu 12 C1"));
        // await customerDatabase.Insert(new Customer("Phuc", "Le", "phuc@mail.com", "Olympiakatu 12 C1"));
        var data = File.ReadAllLines("customers.csv");
        foreach (var line in data)
        {
            Console.WriteLine(line);
        }

    }

}
using DatabaseManagement;
public class Program
{
    public static async Task Main()
    {
        var customerDatabase = new CustomerDatabase();
        var customer1 = new Customer(0, "Phuc", "Le", "phuc@mail.com", "Olympiakatu 12 C1");
        var customer2 = new Customer(0, "Phuc", "Le", "phuc@mail.com", "Olympiakatu 12 C1");

        customerDatabase.AddCustomer(customer1);
        customerDatabase.AddCustomer(customer2);
        customerDatabase.UpdateCustomer(2, new Customer(2, "Tram", "Nguyen", "tram@mail.com", "Vuorikatu"));
        var data = File.ReadAllLines("customers.csv");
        foreach (var line in data)
        {
            Console.WriteLine(line);
        }

    }

}
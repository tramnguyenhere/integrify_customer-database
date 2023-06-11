using DatabaseManagement;

var customerDatabase = new CustomerDatabase<ICustomer>();
var customer1 = new Customer("Tram", "Nguyen", "tram@mail.com", "Olympiakatu 12 C1");
var customer2 = new Customer("Phuc", "Le", "phuc@mail.com", "Olympiakatu 12 C1");
var customer3 = new Customer("Hung", "Tran", "hung@mail.com", "Olympiakatu 12 C1");
var customer4 = new Customer("Mai", "Ngo", "mai@mail.com", "Olympiakatu 12 C1");

// customerDatabase.Insert(customer1);
// customerDatabase.Insert(customer2);
// customerDatabase.Insert(customer3);
customerDatabase.Insert(customer4);
// customerDatabase.Update(1, new Customer("Miu", "Nguyen", "tramle@mail.com", "Strombergintie"));
// customerDatabase.Delete(3);
// customerDatabase.GetCustomerById(1);
// Console.WriteLine(customerDatabase);

var data = File.ReadAllLines("customers.csv");
// FileHelper.ReadCustomersFromFile();
foreach (var line in data)
{
    Console.WriteLine(line);
}

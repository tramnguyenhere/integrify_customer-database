using DatabaseManagement;

var customerDatabase = new CustomerDatabase<ICustomer>();
var customer1 = new Customer("Trammmm", "Nguyewuieghuiqwn", "tra2m@mail.com", "Olympiakatu 12 C1");
var customer2 = new Customer("Phuc", "LEEE", "phuc23561235678@mail.com", "Olympiakatu 12 C1");
var customer3 = new Customer("Phuc", "Nguyen", "phuc@mail.com", "Olympiakatu 12 C1");

// customerDatabase.Insert(customer1);
// customerDatabase.Insert(customer2);
// customerDatabase.Insert(customer3);
customerDatabase.Update(0, new Customer("Le Ngoc Tram", "Nguyen", "tram@mail.com", "Strombergintie"));
// Console.WriteLine(customerDatabase);

var data = File.ReadAllLines("customers.csv");
// FileHelper.ReadCustomersFromFile();
foreach (var line in data)
{
    Console.WriteLine(line);
}

using DatabaseManagement;

var customerDatabase = new CustomerDatabase<ICustomer>();
var customer1 = new Customer("Tram", "Nguyen", "tram@mail.com", "Olympiakatu 12 C1");
customerDatabase.Insert(customer1);
Console.WriteLine(customerDatabase);

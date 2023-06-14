using DatabaseManagement;

class Program
{
    static void Main(string[] args)
    {
        CustomerDatabase customerDatabase = new CustomerDatabase();

        Customer customer1 = new Customer(0, "John", "Doe", "john.doe@example.com", "123 Main St");
        customerDatabase.Insert(customer1);
        Console.WriteLine("After insert operation:");
        Console.WriteLine(customerDatabase);

        // Update the customer
        Customer updatedCustomer1 = new Customer(0, "John", "Smith", "john.smith@example.com", "456 Main St");
        customerDatabase.Update(0, updatedCustomer1);
        Console.WriteLine("After update operation:");
        Console.WriteLine(customerDatabase);

        // Delete the customer
        customerDatabase.Delete(0);
        Console.WriteLine("After delete operation:");
        Console.WriteLine(customerDatabase);

        // Perform undo operation
        customerDatabase.Undo();
        Console.WriteLine("After undo operation:");
        Console.WriteLine(customerDatabase);

        // // // Perform redo operation
        // customerDatabase.Redo();
        // Console.WriteLine("After redo operation:");
        // Console.WriteLine(customerDatabase);
    }
}

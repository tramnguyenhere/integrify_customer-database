﻿using DatabaseManagement;

class Program
{
    static void Main(string[] args)
    {
        CustomerDatabase customerDatabase = new CustomerDatabase();

        Customer customer1 = new Customer(0, "John", "Doe", "john.doe@example.com", "123 Main St");
        Customer customer2 = new Customer(1, "Tram", "Nguyen", "tram.nguyen@example.com", "123 Main St");

        // Insert the customers
        customerDatabase.Insert(customer1);
        Console.WriteLine("After insert operation:");
        FileHelper.PrintCustomerFile();

        customerDatabase.Insert(customer2);
        Console.WriteLine("After the second insert operation:");
        FileHelper.PrintCustomerFile();

        // Update the customer
        Customer updatedCustomer1 = new Customer(0, "John", "Smith", "johnsmith@example.com", "456 Main St");
        customerDatabase.Update(0, updatedCustomer1);
        Console.WriteLine("After update operation:");
        FileHelper.PrintCustomerFile();

        // Delete the customer
        customerDatabase.Delete(0);
        Console.WriteLine("After delete operation:");
        FileHelper.PrintCustomerFile();

        // Perform undo operation
        customerDatabase.Undo();
        Console.WriteLine("After undo operation:");
        FileHelper.PrintCustomerFile();

        // Perform redo operation
        customerDatabase.Redo();
        Console.WriteLine("After redo operation:");
        FileHelper.PrintCustomerFile();
    }
}

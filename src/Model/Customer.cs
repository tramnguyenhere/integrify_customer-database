namespace DatabaseManagement;

interface ICustomer
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
}
class Customer : ICustomer
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }

    public Customer(string firstName, string lastName, string email, string address)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Address = address;
    }

    public override string ToString()
    {
        return $"{Id},{FirstName},{LastName},{Email},{Address}";
    }
}
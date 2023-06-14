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

    public static void SaveCustomerToFile(List<string> lines)
    {
        File.WriteAllLines(FilePath, lines);
    }

    public static void WriteCustomersToFile(List<Customer> customerCollection)
    {
        try
        {
            List<string> lines = new List<string>();
            foreach (var customer in customerCollection)
            {
                string line = $"{customer.Id},{customer.FirstName},{customer.LastName},{customer.Email},{customer.Address}";
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
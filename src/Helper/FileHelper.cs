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

    internal static void InsertToFile(Customer customer, List<string> lines)
    {
        lines.Add(customer.ToString());
        File.WriteAllLines(FilePath, lines);
    }

    internal static void UpdateInFile(int customerId, Customer updatedCustomer, List<string> lines)
    {
        int lineIndex = Utils.FindLineIndex(lines, customerId);
        if (lineIndex != -1)
        {
            lines[lineIndex] = updatedCustomer.ToString();
            File.WriteAllLines(FilePath, lines);
        }
    }

    internal static void DeleteFromFile(int customerId, List<string> lines)
    {
        int lineIndex = Utils.FindLineIndex(lines, customerId);
        if (lineIndex != -1)
        {
            lines.RemoveAt(lineIndex);
            File.WriteAllLines(FilePath, lines);
        }
    }
}
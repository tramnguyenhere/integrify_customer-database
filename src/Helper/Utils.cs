namespace DatabaseManagement;

class Utils
{
    public static bool IsEmailAvailable(List<string> customers, string email, int? customerId = null)
    {
        if (customerId != null)
        {
            if (customers.Any(customer => customer.Split(',')[3] == email && Convert.ToInt32(customer.Split(',')[0]) != customerId))
            {
                return true;
            }
        }
        else
        {
            if (customers.Any(customer => customer.Split(',')[3] == email))
            {
                return true;
            }
        }
        return false;
    }

    public static int GenerateId(List<string> customers)
    {
        int newId = 0;

        if (customers.Count > 0)
        {
            newId = Convert.ToInt32(customers.Last().Split(',')[0]) + 1;
        }
        else
        {
            newId = 0;
        }

        return newId;
    }

    public static int FindLineIndex(List<string> customers, int customerId)
    {
        int lineIndex = -1;
        for (int i = 0; i < customers.Count; i++)
        {
            var parts = customers[i].Split(',');
            if (parts.Length == 5 && int.Parse(parts[0]) == customerId)
            {
                lineIndex = i;
                break;
            }
        }
        return lineIndex;
    }
}
namespace DatabaseManagement;

public class Customer
{
    private int _id;
    private string? _firstName;
    private string? _lastName;
    private string? _email;
    private string? _address;

    public int Id
    {
        get
        {
            return _id;
        }
    }

    public string FirstName
    {
        get
        {
            if (_firstName != null)
            {
                return _firstName;
            }
            else
            {
                throw new Exception("First name is null");
            }
        }
        set
        {
            if (value == null || value == "")
            {
                ExceptionHandler.InputException("First name cannot be empty");
            }
            else
            {
                _firstName = value;
            }
        }
    }
    public string LastName
    {
        get
        {
            if (_lastName != null)
            {
                return _lastName;
            }
            else
            {
                throw new Exception("Last name is null");
            }
        }
        set
        {
            if (value == null || value == "")
            {
                ExceptionHandler.InputException("Last name cannot be empty");
            }
            else
            {
                _lastName = value;
            }
        }
    }
    public string Email
    {
        get
        {
            if (_email != null)
            {
                return _email;
            }
            else
            {
                throw new Exception("Email is null");
            }
        }
        set
        {
            if (value == null || value == "")
            {
                ExceptionHandler.InputException("Email cannot be empty");
            }
            else
            {
                if (Utils.IsValidEmail(value))
                {
                    _email = value;
                }
                else
                {
                    ExceptionHandler.InputException("Email is not valid!");
                }
            }
        }
    }
    public string Address
    {
        get
        {
            if (_address != null)
            {
                return _address;
            }
            else
            {
                throw new Exception("Address is null");
            }
        }
        set
        {
            if (value == null || value == "")
            {
                ExceptionHandler.InputException("Address cannot be empty");
            }
            else
            {
                _address = value;
            }
        }
    }
    public Customer(int id, string firstName, string lastName, string email, string address)
    {
        _id = id;
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
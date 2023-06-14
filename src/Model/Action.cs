namespace DatabaseManagement;
public enum Action
{
    Insert,
    Update,
    Delete
}

public class Step
{
    public Action Action { get; }
    public Customer Customer { get; }

    public Step(Action action, Customer customer)
    {
        Action = action;
        Customer = customer;
    }
}
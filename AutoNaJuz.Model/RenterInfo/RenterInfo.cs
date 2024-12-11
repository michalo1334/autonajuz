namespace AutoNaJuz.Model.RenterInfo;

public class RenterInfo
{
    public RenterInfo(
        int id, 
        string phone,
        string email,
        string fullName)
    {
        Id = id;
        Phone = phone;
        Email = email;
        FullName = fullName;
    }

    public static RenterInfo Create(
        string phone,
        string email,
        string fullName)
    {
        return new RenterInfo(
            0,
            phone,
            email,
            fullName
        );
    }

    public void Update(
        string phone,
        string email,
        string fullName)
    {
        Phone = phone;
        Email = email;
        FullName = fullName;
    }
    public int Id { get; private set; }
    
    public string Phone { get; private set; }
    public string Email { get; private set; }
    public string FullName { get; private set; }
}
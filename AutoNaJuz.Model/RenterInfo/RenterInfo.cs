namespace AutoNaJuz.Model;

public class RenterInfo
{
    public RenterInfo(
        int id,
        string? driversLicenseIdent,
        string pesel,
        DateTime birthDate,
        string firstName,
        string lastName,
        string street,
        string buildingNumber,
        string? apartmentNumber,
        string city,
        string postalCode)
    {
        Id = id;
        DriversLicenseIdent = driversLicenseIdent;
        Pesel = pesel;
        BirthDate = birthDate;
        FirstName = firstName;
        LastName = lastName;
        Street = street;
        BuildingNumber = buildingNumber;
        ApartmentNumber = apartmentNumber;
        City = city;
        PostalCode = postalCode;
    }

    public static RenterInfo Create(
        string? driversLicenseIdent,
        string pesel,
        DateTime birthDate,
        string firstName,
        string lastName,
        string street,
        string buildingNumber,
        string? apartmentNumber,
        string city,
        string postalCode)
    {
        return new RenterInfo(
            0,
            driversLicenseIdent,
            pesel,
            birthDate,
            firstName,
            lastName,
            street,
            buildingNumber,
            apartmentNumber,
            city,
            postalCode
        );
    }

    public void Update(
        string? driversLicenseIdent,
        string pesel,
        DateTime birthDate,
        string firstName,
        string lastName,
        string street,
        string buildingNumber,
        string? apartmentNumber,
        string city,
        string postalCode)
    {
        DriversLicenseIdent = driversLicenseIdent;
        Pesel = pesel;
        BirthDate = birthDate;
        FirstName = firstName;
        LastName = lastName;
        Street = street;
        BuildingNumber = buildingNumber;
        ApartmentNumber = apartmentNumber;
        City = city;
        PostalCode = postalCode;
    }

    public bool IsPeselValid() => true;

    public int Id { get; private set; }
    
    public string? DriversLicenseIdent { get; private set; }
    public string Pesel { get; private set; }
    public DateTime BirthDate { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Street { get; private set; }
    public string BuildingNumber { get; private set; }
    public string? ApartmentNumber { get; private set; }
    public string City { get; private set; }
    public string PostalCode { get; private set; }
}
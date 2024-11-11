namespace AutoNaJuz.ViewModels.RenterInfo;

public record CreateRenterInfoVm(
    string? DrivingLicenseIdent,
    string Pesel,
    DateTime BirthDate,
    string FirstName,
    string LastName,
    string Street,
    string BuildingNumber,
    string? ApartmentNumber,
    string City,
    string PostalCode
    );
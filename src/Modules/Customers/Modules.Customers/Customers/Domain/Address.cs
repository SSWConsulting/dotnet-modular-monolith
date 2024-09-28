namespace Modules.Customers.Customers.Domain;

internal record Address
{
    internal string Line1 { get; }
    internal string? Line2 { get; }
    internal string City { get; }
    public string State { get; }
    public string ZipCode { get; }
    public string Country { get; }

    internal Address(string line1, string? line2, string city, string state, string zipCode, string country)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(line1);
        ArgumentException.ThrowIfNullOrWhiteSpace(city);
        ArgumentException.ThrowIfNullOrWhiteSpace(state);
        ArgumentException.ThrowIfNullOrWhiteSpace(zipCode);
        ArgumentException.ThrowIfNullOrWhiteSpace(country);

        Line1 = line1;
        Line2 = line2;
        City = city;
        State = state;
        ZipCode = zipCode;
        Country = country;
    }
}

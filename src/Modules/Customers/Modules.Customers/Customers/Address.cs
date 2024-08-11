using Throw;

namespace Modules.Customers.Customers;

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
        line1.Throw().IfEmpty();
        city.Throw().IfEmpty();
        state.Throw().IfEmpty();
        zipCode.Throw().IfEmpty();
        country.Throw().IfEmpty();

        Line1 = line1;
        Line2 = line2;
        City = city;
        State = state;
        ZipCode = zipCode;
        Country = country;
    }
}

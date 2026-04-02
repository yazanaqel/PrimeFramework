using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects;

public class Address : ValueObject<Address>
{
    public string Street { get; }
    public string City { get; }
    public string Country { get; }

    public Address(string street,string city,string country)
    {
        if(string.IsNullOrWhiteSpace(street))
            throw new ArgumentException("Street cannot be empty");

        if(string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("City cannot be empty");

        if(string.IsNullOrWhiteSpace(country))
            throw new ArgumentException("Country cannot be empty");

        Street = street.Trim();
        City = city.Trim();
        Country = country.Trim();
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Street;
        yield return City;
        yield return Country;
    }
}

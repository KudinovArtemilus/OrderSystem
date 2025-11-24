using System;
using System.Collections.Generic;
using System.Text;

namespace OrderSystem.Domain.ValueObjects;

public class Address
{
    public string Country { get; }
    public string City { get; }
    public string Street { get; }
    public string House { get; }
    public string? Apartment { get; }

    public Address(string country, string city, string street, string house, string? apartment = null)
    {
        Country = country ?? string.Empty;
        City = city ?? string.Empty;
        Street = street ?? string.Empty;
        House = house ?? string.Empty;
        Apartment = apartment;
    }

    public override string ToString()
    {
        return $"{City}, {Street} {House}" + (Apartment != null ? $", кв. {Apartment}" : "");
    }
}

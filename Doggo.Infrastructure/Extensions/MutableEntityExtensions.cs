namespace Doggo.Infrastructure.Extensions;

using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

public static class MutableEntityExtensions
{
    public static void PropertyNameToSnakeCase(this IMutableEntityType entity)
    {
        foreach (var property in entity.GetProperties())
        {
            var propertyName = property.GetColumnName();

            var noLeadingUnderscore = Regex.Replace(propertyName, @"^_", "");

            var snakeCase = Regex.Replace(noLeadingUnderscore, @"(?:(?<l>[a-z0-9])(?<r>[A-Z])|(?<l>[A-Z])(?<r>[A-Z][a-z0-9]))", "${l}_${r}")
                .ToLower();
            property.SetColumnName(snakeCase);
        }
    }
}
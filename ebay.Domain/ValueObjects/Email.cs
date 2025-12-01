using System.Text.RegularExpressions;

namespace ebay.Domain.ValueObjects
{
    public class Email
    {
        private const string EmailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        private static readonly Regex EmailRegex = new Regex(EmailPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public string Value { get; private set; }

        private Email() { } // For EF Core

        public Email(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentException("Email address cannot be null or empty.", nameof(address));
            }

            if (!EmailRegex.IsMatch(address))
            {
                throw new ArgumentException($"Invalid email address format: {address}", nameof(address));
            }

            Value = address.ToLowerInvariant(); // Normalize to lowercase
        }

        public override string ToString()
        {
            return Value;
        }

        // Value Object pattern: implement equality
        public override bool Equals(object? obj)
        {
            if (obj is null || obj.GetType() != GetType())
                return false;

            var other = (Email)obj;
            return Value == other.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(Email? left, Email? right)
        {
            if (left is null && right is null)
                return true;
            if (left is null || right is null)
                return false;
            return left.Equals(right);
        }

        public static bool operator !=(Email? left, Email? right)
        {
            return !(left == right);
        }

        // Factory method for better readability
        public static Email Create(string address)
        {
            return new Email(address);
        }
    }
}
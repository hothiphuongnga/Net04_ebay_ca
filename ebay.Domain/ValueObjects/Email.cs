namespace ebay.Domain.ValueObjects
{
    public class Email
    {
        public string Value { get; private set; }
        
        // khai báo biến regex bắt lỗi email

        public Email(string address)
        {
            if (string.IsNullOrWhiteSpace(address) || !address.Contains("@"))
            {
                throw new ArgumentException("Invalid email address.");
            }

            // regex

            Value = address;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
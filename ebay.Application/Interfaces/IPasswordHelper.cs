namespace ebay.Application.Interfaces;

public interface IPasswordHelper
{
    public string HashPassword(string password, int workFactor = 12);
    public bool VerifyPassword(string password = "", string hashedPassword = "");
}
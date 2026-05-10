namespace UniversityHistory.Application.Interfaces.Auth;

public interface IPasswordGenerator
{
    string Generate(int length = 14);
}

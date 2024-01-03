using System.Security;

namespace AuroraGuard.Core.Interfaces;

/// <summary>
/// An interface for a class that can provide a secure password
/// </summary>
public interface IPasswordContainer
{
    SecureString SecurePassword { get; }
}
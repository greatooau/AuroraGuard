using System.Runtime.InteropServices;
using System.Security;

namespace AuroraGuard.Core.Security;

/// <summary>
/// Helpers for the <see cref="SecureString"/> class
/// </summary>
public static class SecureStringHelpers
{
    public static string? Unsecure(this SecureString? secureString)
    {
        if (secureString is null) return string.Empty;

        var unmanagedString = nint.Zero;

        try
        {
            unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
            return Marshal.PtrToStringUni(unmanagedString);
        }
        finally
        {
            Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
        }
    }
}
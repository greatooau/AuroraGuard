using System.Security;
using AuroraGuard.Core.Interfaces;

namespace AuroraGuard.Tests.ViewModels.HelperClasses;

public class Parameter : IPasswordContainer, IHandleWindowNavigation, IResizableWindow
{
	public Parameter(string password)
	{
		SecurePassword = new SecureString();

		foreach (var @char in password) 
			SecurePassword.AppendChar(@char);
	}

	public Parameter()
	{
		
	}

	public SecureString SecurePassword { get; } = null!;

	public void Navigate()
	{
		
	}

	public void Close()
	{
		throw new NotImplementedException();
	}

	public void MaximizeRestore()
	{
		throw new NotImplementedException();
	}

	public void Minimize()
	{
		throw new NotImplementedException();
	}
}
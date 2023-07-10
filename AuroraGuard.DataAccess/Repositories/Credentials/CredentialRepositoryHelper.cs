using AuroraGuard.DTOs.Credentials;

namespace AuroraGuard.DataAccess.Repositories.Credentials;

public static class CredentialRepositoryHelper
{
	internal static object GenerateCreateCredentialParam(CreateCredentialDto createCredentialDto)
	{
		return new 
		{
			Id = Guid.NewGuid().ToString(),
			createCredentialDto.AccessUser,
			createCredentialDto.AccessPassword,
			CreatedAt = DateTime.Now,
			ModifiedAt = DateTime.Now
		};
	}

	internal static object GenerateGetCredentialByUserIdParam(string userId) => new { UserId = userId };
	
	internal static object GenerateGetCredentialByIdParam(string id) => new { Id = id };
}
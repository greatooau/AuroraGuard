using AuroraGuard.Core.DTO.Credentials;
using AuroraGuard.Core.Models;

namespace AuroraGuard.DataAccess.Repositories.Credentials;

public static class CredentialRepositoryHelper
{
	public static object GenerateCreateParam(CreateCredentialDto createCredentialDto)
	{
		return new Credential
		{
			Id = createCredentialDto.Id.ToString(),
			AccessUser = createCredentialDto.AccessUser,
			AccessPassword = createCredentialDto.AccessPassword,
			CreatedAt = DateTime.Now,
			ModifiedAt = DateTime.Now
		};
	}

	public static object GenerateGetByIdParam(Guid id) => new { Id = id.ToString() };
}
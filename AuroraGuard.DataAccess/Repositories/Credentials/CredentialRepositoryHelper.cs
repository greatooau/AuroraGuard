using AuroraGuard.Core.DTO.Credentials;
using AuroraGuard.Core.Models;

namespace AuroraGuard.DataAccess.Repositories.Credentials;

public static class CredentialRepositoryHelper
{
	public static object GenerateCreateParam(CreateCredentialDto createCredentialDto)
	{
		return new Credential
		{
			Id = createCredentialDto.Id,
			AccessUser = createCredentialDto.AccessUser,
			AccessPassword = createCredentialDto.AccessPassword,
			AppName = createCredentialDto.AppName,
			ImagePath = createCredentialDto.ImagePath,
			CreatedAt = DateTime.Now,
			UpdatedAt = DateTime.Now
		};
	}

	public static object GenerateGetByIdParam(Guid id) => new { Id = id };

    public static object GenerateUpdateParam(Guid id, UpdateCredentialDto updateCredentialDto)
    {
        return new 
        {
            Id = id,
            updateCredentialDto.AccessUser,
            updateCredentialDto.AccessPassword,
            updateCredentialDto.AppName,
            updateCredentialDto.ImagePath,
            UpdatedAt = DateTime.Now
        };
    }
}
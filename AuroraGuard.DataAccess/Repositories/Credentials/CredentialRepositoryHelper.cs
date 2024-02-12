using AuroraGuard.Core.DTO.Credentials;
using AuroraGuard.Core.Models;

namespace AuroraGuard.DataAccess.Repositories.Credentials;

public static class CredentialRepositoryHelper
{
	public static object GenerateCreateParam(CreateCredentialDto createCredentialDto)
	{
		return new 
		{
			createCredentialDto.Id,
			createCredentialDto.AccessUser,
			createCredentialDto.AccessPassword,
			createCredentialDto.AppName,
			createCredentialDto.Notes,
			createCredentialDto.ImagePath,
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
			updateCredentialDto.Notes,
            UpdatedAt = DateTime.Now
        };
    }
}
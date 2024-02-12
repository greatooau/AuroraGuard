using AuroraGuard.Core.Abstract;
using AuroraGuard.Core.Models;

namespace AuroraGuard.Core.Interfaces;

public interface IHandleCredentialCreationEdition
{
    Credential? CreateCredential(ViewModel viewModel);
    bool EditCredential(ViewModel viewModel);
}
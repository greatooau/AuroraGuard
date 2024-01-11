using AuroraGuard.Core.Models;

namespace AuroraGuard.UserInterface.ViewModels.EventArgsTypes;

public class AlteredItemEventArgs : EventArgs
{
    public Guid Id { get; set; }
    public Credential Credential { get; set; }
}
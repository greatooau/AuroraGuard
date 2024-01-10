using System.Collections;

namespace AuroraGuard.Core.Interfaces;

public interface ISelectedEventArgs
{
    IList<object> AddedElements { get; }
}
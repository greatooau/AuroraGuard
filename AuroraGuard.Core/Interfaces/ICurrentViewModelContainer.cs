using AuroraGuard.Core.Abstract;

namespace AuroraGuard.Core.Interfaces;

public interface ICurrentViewModelContainer
{
	ViewModel CurrentViewModel { get; set; }
}
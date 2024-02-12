using AuroraGuard.Core.Enum;

namespace AuroraGuard.Core.Interfaces;

public interface IResizableWindow : IClosableWindow
{
	WindowCurrentState MaximizeRestore();
	void Minimize();
}
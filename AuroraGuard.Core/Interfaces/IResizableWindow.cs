namespace AuroraGuard.Core.Interfaces;

public interface IResizableWindow : IClosableWindow
{
	void MaximizeRestore();
	void Minimize();
}
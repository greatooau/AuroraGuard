using System.Windows;
using AuroraGuard.Core.Interfaces.Services;

namespace AuroraGuard.UserInterface.WPF.Services;

public class ClipboardService : IClipboardService
{
    public void CopyText(string text) => Clipboard.SetText(text);
}
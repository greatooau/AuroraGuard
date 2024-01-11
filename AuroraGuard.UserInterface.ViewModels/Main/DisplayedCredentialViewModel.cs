﻿using System.Windows.Input;
using AuroraGuard.Core.Abstract;
using AuroraGuard.Core.Interfaces;
using AuroraGuard.Core.Interfaces.Repositories;
using AuroraGuard.Core.Interfaces.Services;
using AuroraGuard.UserInterface.ViewModels.EventArgsTypes;

namespace AuroraGuard.UserInterface.ViewModels.Main;

public class DisplayedCredentialViewModel : ViewModel
{
    private readonly ICredentialRepository _credentialRepository;
    private readonly IDialogService _dialogService;

    public DisplayedCredentialViewModel(ICredentialRepository credentialRepository, IDialogService dialogService)
    {
        _credentialRepository = credentialRepository;
        _dialogService = dialogService;
        DeleteCommand = new AsyncRelayCommand(Delete);
        EditCredentialCommand = new AsyncRelayCommand(EditCredential);
    }

    private string? _appName;
    public string? AppName
    {
        get => _appName;
        set => SetField(ref _appName, value);
    }

    private string? _username;
    public string? Username
    {
        get => _username;
        set => SetField(ref _username, value);
    }

    private string? _password;
    public string? Password
    {
        get => _password;
        set => SetField(ref _password, value);
    }

    private string? _notes;
    public string? Notes
    {
        get => _notes;
        set => SetField(ref _notes, value);
    }
    public Guid Id { private get; init; }

    public ICommand DeleteCommand { get; }
    private async Task Delete(object? parameter, CancellationToken token)
    {
        var canBeDeleted = _dialogService.ShowConfirmation($"¿Quieres borrar la credencial de {AppName}?", "Eliminación de credencial");

        if (!canBeDeleted) return;

        try
        {
            await _credentialRepository.Delete(Id);

            CredentialDeleted?.Invoke(this, new AlteredItemEventArgs { Id = Id });
        }
        catch (Exception e)
        {
            _dialogService.ShowError(e, "Error al borrar");
        }
    }
    public event EventHandler<AlteredItemEventArgs>? CredentialDeleted;

    public ICommand EditCredentialCommand { get; }
    public async Task EditCredential(object? parameter, CancellationToken token)
    {
        var handler = (IHandleCredentialCreationEdition)parameter!;

        var selectedCredential = await _credentialRepository.GetById(Id);

        handler.EditCredential(_credentialRepository, selectedCredential);
        
        selectedCredential = await _credentialRepository.GetById(Id);

        CredentialEdited?.Invoke(this, new AlteredItemEventArgs { Credential = selectedCredential, Id  = Id});
    }
    public event EventHandler<AlteredItemEventArgs>? CredentialEdited;
}
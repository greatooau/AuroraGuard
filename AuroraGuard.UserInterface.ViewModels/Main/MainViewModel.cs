using System.Collections.ObjectModel;
using System.Windows.Input;
using AuroraGuard.Core.Abstract;
using AuroraGuard.Core.DTO.Credentials;
using AuroraGuard.Core.Enum;
using AuroraGuard.Core.Interfaces;
using AuroraGuard.Core.Interfaces.Repositories;
using AuroraGuard.Core.Interfaces.Services;
using AuroraGuard.Core.Models;
using AuroraGuard.UserInterface.ViewModels.EventArgsTypes;

namespace AuroraGuard.UserInterface.ViewModels.Main;

public class MainViewModel : ViewModel
{
	private readonly ICredentialRepository _credentialRepository;
    private readonly Func<WindowType, IShowDialog> _windowResolver;
    private readonly IDialogService _dialogService;
    private List<Credential>? Credentials { get; set; }

	public MainViewModel(ICredentialRepository credentialRepository, Func<WindowType, IShowDialog> windowResolver, IDialogService dialogService)
	{
		_credentialRepository = credentialRepository;
        _windowResolver = windowResolver;
        _dialogService = dialogService;

        DisplayItemCommand = new AsyncRelayCommand(DisplaySelectedItem);
        LoadedCommand = new AsyncRelayCommand(LoadAsync);
        AddCredentialCommand = new RelayCommand(AddCredential);
    }

	#region Bindable Properties

    private DisplayedCredentialViewModel? _displayedCredential;
    public DisplayedCredentialViewModel? DisplayedCredential
    {
        get => _displayedCredential;
        set => SetField(ref _displayedCredential, value);
    }

    private bool _isDisplayedCredentialLoaded;
    public bool IsDisplayedCredentialLoaded
    {
        get => _isDisplayedCredentialLoaded;
        set => SetField(ref _isDisplayedCredentialLoaded, value);
    }

    private CredentialItemViewModel? _selectedCredential;
	public CredentialItemViewModel? SelectedCredential
    {
        get => _selectedCredential;
        set => SetField(ref _selectedCredential, value);
    }

	private ObservableCollection<CredentialItemViewModel>? _credentialList;
	public ObservableCollection<CredentialItemViewModel>? CredentialList
	{
		get => _credentialList;
		set => SetField(ref _credentialList, value);
	}

	private bool _areCredentialsLoaded;
	public bool AreCredentialsLoaded
	{
		get => _areCredentialsLoaded;
		set => SetField(ref _areCredentialsLoaded, value);
	}
	
	private bool _isCredentialsListEmpty;
	public bool IsCredentialsListEmpty
	{
		get => _areCredentialsLoaded;
		set => SetField(ref _isCredentialsListEmpty, value);
	}

    #region LoadedCommand

    public ICommand LoadedCommand { get; }

    private async Task LoadAsync(object? parameter, CancellationToken cancellationToken)
    {
        var credentials = await _credentialRepository.GetAll();
        Credentials = credentials.ToList();

        var credentialViewModels = Credentials
            .Select(credential => new CredentialItemViewModel(credential.Id.ToString(), credential.ImagePath, credential.AppName, credential.CreatedAt))
            .ToList();

        CredentialList = [..credentialViewModels];

        AreCredentialsLoaded = true;
        IsCredentialsListEmpty = false;
    }

    #endregion

    #region DisplayItemCommand

    public ICommand DisplayItemCommand { get; }

    private async Task DisplaySelectedItem(object? parameter, CancellationToken token)
    {
        if (SelectedCredential is null) return;

        var id = Guid.Parse(SelectedCredential.Id);

        var credential = await _credentialRepository.GetById(id);

        DisplayedCredential = new DisplayedCredentialViewModel(_credentialRepository, _dialogService)
        {
            Id = id,
            AppName = credential.AppName,
            Notes = credential.Notes,
            Password = credential.AccessPassword,
            Username = credential.AccessUser
        };
        
        DisplayedCredential.CredentialDeleted += DisplayedCredentialOnCredentialDeleted;
    }

    private void DisplayedCredentialOnCredentialDeleted(object? sender, AlteredItemEventArgs e)
    {
        var itemToRemove = CredentialList!.Single(c => c.Id == e.Id.ToString());
        CredentialList!.Remove(itemToRemove);

        DisplayedCredential!.CredentialDeleted -= DisplayedCredentialOnCredentialDeleted;
        DisplayedCredential = null;
    }

    #endregion

    #region AddCommand

    public ICommand AddCredentialCommand { get; }

    public void AddCredential(object? parameter)
    {
        var handler = (IHandleCredentialCreation)parameter!;

        var createdCredential = handler.CreateCredential(_credentialRepository);

        if (createdCredential is null)
        {
            _dialogService.ShowMessage("No se pudo crear la credencial");
            return;
        }

        var createdCredentialItem = new CredentialItemViewModel(createdCredential.Id.ToString(), createdCredential.ImagePath, createdCredential.AppName,
            createdCredential.CreatedAt);

        CredentialList?.Add(createdCredentialItem);
    }

    #endregion

    #endregion
}
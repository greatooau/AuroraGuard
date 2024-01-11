using System.Collections.ObjectModel;
using System.Windows.Input;
using AuroraGuard.Core.Abstract;
using AuroraGuard.Core.Interfaces;
using AuroraGuard.Core.Interfaces.Repositories;
using AuroraGuard.Core.Interfaces.Services;
using AuroraGuard.UserInterface.ViewModels.EventArgsTypes;

namespace AuroraGuard.UserInterface.ViewModels.Main;

public class MainViewModel : ViewModel
{
	private readonly ICredentialRepository _credentialRepository;
    private readonly IDialogService _dialogService;

	public MainViewModel(ICredentialRepository credentialRepository, IDialogService dialogService)
	{
		_credentialRepository = credentialRepository;
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
        var credentialsEnumerable = await _credentialRepository.GetAll();
        var credentials = credentialsEnumerable.ToList();

        var credentialViewModels = credentials
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
        DisplayedCredential.CredentialEdited += DisplayedCredential_CredentialEdited;
    }

    private void DisplayedCredential_CredentialEdited(object? sender, AlteredItemEventArgs e)
    {
        var oldCredentialItem = CredentialList!.Single(c => c.Id == e.Id.ToString());
        var index = CredentialList!.IndexOf(oldCredentialItem);

        DisplayedCredential!.CredentialEdited -= DisplayedCredential_CredentialEdited;

        DisplayedCredential = new DisplayedCredentialViewModel(_credentialRepository, _dialogService)
        {
            Id = e.Id,
            AppName = e.Credential.AppName,
            Notes = e.Credential.Notes,
            Password = e.Credential.AccessPassword,
            Username = e.Credential.AccessUser
        };

        DisplayedCredential.CredentialEdited += DisplayedCredential_CredentialEdited;

        CredentialList[index] = new CredentialItemViewModel(e.Id.ToString(), e.Credential.ImagePath, e.Credential.AppName, e.Credential.CreatedAt);
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
        var handler = (IHandleCredentialCreationEdition)parameter!;

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
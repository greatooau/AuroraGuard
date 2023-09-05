using AuroraGuard.Core.Abstract;
using AuroraGuard.ViewModels.Services;
using AuroraGuard.ViewModels.Stores;

namespace AuroraGuard.Tests.Services;

public class NavigationServiceTests
{
	[Fact]
	public void NavigateTo_ThrowsException_WhenT_IsNotAViewModelBase()
	{
		//Arrange
		var navigationStore = new NavigationStore();
		var sut = new NavigationService(navigationStore);
		
		// Act
		// Assert
		var exception = Assert.Throws<Exception>(() =>
		{
			sut.NavigateTo<NavigationStore>();
		});
		
		Assert.Equal($"{typeof(NavigationStore)} is not of Type ViewModelBase", exception.Message);
	}

	[Fact]
	public void NavigateTo_AddsViewModelToStackAndChangeCurrentViewModel()
	{
		// Arrange
		var navigationStore = new NavigationStore();
		var sut = new NavigationService(navigationStore);

	
		// Act
		sut.NavigateTo<A>();

		// Assert
		Assert.Single(navigationStore.ViewModelsStack);
		Assert.IsType<A>(navigationStore.ViewModelsStack.First());
		Assert.Equal(0, navigationStore.CurrentViewModelStackIndex);
	}
	
	[Fact]
	public void GoBack_ChangesCurrentViewModel()
	{
		// Arrange
		var navigationStore = new NavigationStore();
		var sut = new NavigationService(navigationStore);
		
		// Act
		sut.NavigateTo<A>();
		sut.NavigateTo<B>();
		sut.GoBack();

		// Assert
		Assert.Equal(2, navigationStore.ViewModelsStack.Count);
		Assert.Equal(1, navigationStore.CurrentViewModelStackIndex);
		Assert.IsType<A>(navigationStore.CurrentViewModel);
	}
	
	[Fact]
	public void GoForward_ChangesCurrentViewModel()
	{
		// Arrange
		var navigationStore = new NavigationStore();
		var sut = new NavigationService(navigationStore);
		
		// Act
		sut.NavigateTo<A>();
		sut.NavigateTo<B>();
		sut.GoBack();
		sut.GoForward();

		// Assert
		Assert.Equal(2, navigationStore.ViewModelsStack.Count);
		Assert.Equal(0, navigationStore.CurrentViewModelStackIndex);
		Assert.IsType<B>(navigationStore.CurrentViewModel);
	}

	private class A : ViewModelBase {}	
	private class B : ViewModelBase {}
	
}
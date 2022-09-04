using burnbook.Contracts.ViewModels;
using burnbook.Core.Contracts.Services;
using burnbook.Core.Models;
using burnbook.Core.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace burnbook.ViewModels;

public class FocusButtonViewModel : ObservableRecipient, INavigationAware
{
    private readonly IFocusButtonService _focusButtonService;

    public FocusButtonModel currentDayData => _focusButtonService.GetCurrentDayData();
    public int currentDayNumber => _focusButtonService.GetDayCount();

    public void UpdateDistractionCounter(int incrimentValue) 
        => _focusButtonService.UpdateCurrentDay(
            _focusButtonService.GetCurrentDayData().DistractionCounter +
            incrimentValue);

    public FocusButtonViewModel(IFocusButtonService focusButtonService)
    {
        _focusButtonService = focusButtonService;
    }

    public void OnNewDay()
    {
        _focusButtonService.OnNewDay();
    }

    public void OnNavigatedTo(object parameter)
    {
    }
    public void OnNavigatedFrom()
    {
        _focusButtonService.SaveDay();
    }

}

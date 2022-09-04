using burnbook.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using burnbook.Core.Services;
using burnbook.Core.Models;
using burnbook.Contracts.ViewModels;

namespace burnbook.Views;

public sealed partial class FocusButtonPage : Page
{
    public FocusButtonViewModel ViewModel
    {
        get;
    }

    public FocusButtonPage()
    {
        ViewModel = App.GetService<FocusButtonViewModel>();

        InitializeComponent();
        Update();
    }

    public void Update()
    {
        CounterButtonText.Text = $"Distracted {ViewModel.currentDayData.DistractionCounter} Times Today.";
        DayNumber.Text = $"Day #{ViewModel.currentDayNumber}";
    }

    public void ClickCounter(object sender, RoutedEventArgs e)
    {
        ViewModel.UpdateDistractionCounter(incrimentValue: 1);
        CounterButtonText.Text = $"Distracted {ViewModel.currentDayData.DistractionCounter} Times Today.";
    }

    private async void NewDay(object sender, RoutedEventArgs e)
    {
        ContentDialog dialog = new ContentDialog();

        // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
        dialog.XamlRoot = this.XamlRoot;
        dialog.Style = App.Current.Resources["DefaultContentDialogStyle"] as Style;
        dialog.Title = "Start new day?";
        dialog.PrimaryButtonText = "Start";
        dialog.CloseButtonText = "Cancel";
        dialog.DefaultButton = ContentDialogButton.Close;

        var result = await dialog.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            ViewModel.OnNewDay();
            Update();
        }
        //ViewModel.focusButtonModel.DistractionCounter = 0;
    }
}

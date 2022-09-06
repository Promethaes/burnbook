using burnbook.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using burnbook.Core.Services;
using burnbook.Core.Models;
using burnbook.Contracts.ViewModels;

namespace burnbook.Views;

public sealed partial class FocusButtonPage : Page
{
    List<CheckBox> skincareCheckboxes = new();
    bool skincareStateDirty = false;
    public FocusButtonViewModel ViewModel
    {
        get;
    }

    public FocusButtonPage()
    {
        ViewModel = App.GetService<FocusButtonViewModel>();
        InitializeComponent();
        UpdateCounterText();

        skincareCheckboxes.Add(ShaveCheckBox);
        skincareCheckboxes.Add(AcneTreatmentCheckBox);
        skincareCheckboxes.Add(TonerCheckBox);
        skincareCheckboxes.Add(MoistuerizerCheckBox);
        skincareCheckboxes.Add(SunscreenCheckBox);

        foreach (var check in skincareCheckboxes)
            check.IsChecked = ViewModel.GetCheckboxValue(check.Name);
        ShowerCheckBox.IsChecked = ViewModel.GetCheckboxValue(ShowerCheckBox.Name);
        YogaCheckBox.IsChecked = ViewModel.GetCheckboxValue(YogaCheckBox.Name);
        BreakfastCheckBox.IsChecked = ViewModel.GetCheckboxValue(BreakfastCheckBox.Name);

        WakeupTime.Time = ViewModel.currentDayData.WakeupTime;
    }

    public void UpdateCounterText()
    {
        CounterButtonText.Text = $"Distracted {ViewModel.currentDayData.DistractionCounter} Times Today.";
        DayNumber.Text = $"Day #{ViewModel.currentDayNumber}";
    }

    public void ClickCounter(object sender, RoutedEventArgs e)
    {
        ViewModel.UpdateDistractionCounter(incrimentValue: 1);
        UpdateCounterText();
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
            foreach (var check in skincareCheckboxes)
                check.IsChecked = false;
            ShowerCheckBox.IsChecked = false;
            YogaCheckBox.IsChecked = false;
            BreakfastCheckBox.IsChecked = false;
            UpdateCounterText();
        }
        //ViewModel.focusButtonModel.DistractionCounter = 0;
    }



    private void SkinCareCheckBox_Checked(object sender, RoutedEventArgs e)
    {
        //foreach (var check in skincareCheckboxes)
        //    check.IsChecked = true;

    }

    private void SkinCareCheckBox_Unchecked(object sender, RoutedEventArgs e)
    {
        //foreach (var check in skincareCheckboxes)
        //    check.IsChecked = false;
    }

    private void SkinCareCheckBox_Indeterminate(object sender, RoutedEventArgs e)
    {
        //skincareCheckboxes.ForEach(delegate (CheckBox x)
        //{
        //    if ((bool)x.IsChecked)
        //    {
        //        SkinCareCheckBox.IsChecked = false;
        //        foreach (var check in skincareCheckboxes)
        //            check.IsChecked = false;
        //        return;
        //    }
        //});
    }

    void SetSkinCareCheckBoxState()
    {
        int numChecked = 0;
        foreach (var check in skincareCheckboxes)
            if ((bool)check.IsChecked)
                numChecked++;

        if (numChecked == skincareCheckboxes.Count)
            SkinCareCheckBox.IsChecked = true;
        else if (numChecked != 0)
            SkinCareCheckBox.IsChecked = null;
        else
            SkinCareCheckBox.IsChecked = false;

        void StoreCheckBox(CheckBox check)
        {
            ViewModel.UpdateMorningRoutine(check.Name, (bool)check.IsChecked);
        }

        foreach (var check in skincareCheckboxes)
            StoreCheckBox(check);
        StoreCheckBox(ShowerCheckBox);
        StoreCheckBox(YogaCheckBox);
        StoreCheckBox(BreakfastCheckBox);
    }

    private void ShaveCheckBox_Checked(object sender, RoutedEventArgs e)
    {
        SetSkinCareCheckBoxState();
    }

    private void ShaveCheckBox_Unchecked(object sender, RoutedEventArgs e)
    {
        SetSkinCareCheckBoxState();
    }

    private void WakeupTime_SelectedTimeChanged(TimePicker sender, TimePickerSelectedValueChangedEventArgs args)
    {
        ViewModel.UpdateWakeupTime(sender.Time);
    }
}

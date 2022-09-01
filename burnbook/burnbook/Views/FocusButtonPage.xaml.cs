using burnbook.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace burnbook.Views;

public sealed partial class FocusButtonPage : Page
{
    static int counter = 0;
    
    public FocusButtonViewModel ViewModel
    {
        get;
    }

    public FocusButtonPage()
    {
        ViewModel = App.GetService<FocusButtonViewModel>();
        InitializeComponent();
        CounterButtonText.Text = $"Distracted {counter} Times Today.";
    }

    public void ClickCounter(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        if (button == null)
            return;
        counter++;
        button.Content = $"Distracted {counter} Times Today.";
    }

}

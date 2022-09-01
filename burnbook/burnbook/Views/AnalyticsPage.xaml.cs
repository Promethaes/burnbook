using burnbook.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace burnbook.Views;

public sealed partial class AnalyticsPage : Page
{
    public AnalyticsViewModel ViewModel
    {
        get;
    }

    public AnalyticsPage()
    {
        ViewModel = App.GetService<AnalyticsViewModel>();
        InitializeComponent();
    }
}

using burnbook.ViewModels;

using CommunityToolkit.WinUI.UI.Controls;

using Microsoft.UI.Xaml.Controls;

namespace burnbook.Views;

public sealed partial class TodoListPage : Page
{
    public TodoListViewModel ViewModel
    {
        get;
    }

    public TodoListPage()
    {
        ViewModel = App.GetService<TodoListViewModel>();
        InitializeComponent();
    }

    private void OnViewStateChanged(object sender, ListDetailsViewState e)
    {
        if (e == ListDetailsViewState.Both)
        {
            ViewModel.EnsureItemSelected();
        }
    }
}

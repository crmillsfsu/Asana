using Asana.Maui.ViewModels;

namespace Asana.Maui.Views;

public partial class ProjectsView : ContentPage
{
    public ProjectsView()
    {
        InitializeComponent();
        BindingContext = new ProjectsPageViewModel();
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void AddClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//ProjectsPageView");
    }

    private void EditClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//ProjectsPageView");
    }

    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as ProjectViewModel)?.DoDelete();
    }
    
}
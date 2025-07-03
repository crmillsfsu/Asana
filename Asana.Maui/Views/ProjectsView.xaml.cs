using Asana.Maui.ViewModels;

namespace Asana.Maui.Views
{
    public partial class ProjectsView : ContentPage
    {
        public ProjectsView()
        {
            InitializeComponent();
            try
            {
                var viewModel = new ProjectsPageViewModel();
                BindingContext = viewModel;
                System.Diagnostics.Debug.WriteLine("ProjectsView BindingContext set successfully");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ProjectsView Error: {ex.Message}");
                DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("ProjectsView NavigatedTo");
                (BindingContext as ProjectsPageViewModel)?.RefreshPage();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"NavigatedTo Error: {ex.Message}");
            }
        }

        private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
        {
    
        }

        private void SearchClicked(object sender, EventArgs e)
		{
    		(BindingContext as ProjectsPageViewModel)?.HandleSearchClick();
		}

	private void AddNewClicked(object sender, EventArgs e)
	{
    	// For now, show alert - later you can navigate to ProjectDetailView
    	DisplayAlert("Info", "Add New Project - Coming Soon!", "OK");
    	// Shell.Current.GoToAsync("//ProjectDetails");
	}

	private void EditClicked(object sender, EventArgs e)
	{
    	var selectedId = (BindingContext as ProjectsPageViewModel)?.SelectedProjectId ?? 0;
    	if (selectedId > 0)
    	{
        	DisplayAlert("Info", $"Edit Project {selectedId} - Coming Soon!", "OK");
        	// Shell.Current.GoToAsync($"//ProjectDetails?projectId={selectedId}");
    	}
    	else
    	{
        	DisplayAlert("Warning", "Please select a project to edit.", "OK");
    	}
		}

		private void DeleteClicked(object sender, EventArgs e)
		{
    		(BindingContext as ProjectsPageViewModel)?.DeleteProject();
		}

		private void InLineDeleteClicked(object sender, EventArgs e)
		{
    		(BindingContext as ProjectsPageViewModel)?.RefreshPage();
		}

        private void BackToToDosClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//MainPage");
        }

    }
}
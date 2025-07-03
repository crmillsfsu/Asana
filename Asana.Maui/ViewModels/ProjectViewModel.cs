using Asana.Library.Models;
using Asana.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Asana.Maui.ViewModels
{
    public class ProjectViewModel
    {
        public ProjectViewModel() {
            Model = Model ?? new Project();
            DeleteCommand = new Command(DoDelete);
        }

        public ProjectViewModel(int id)
        {
            Model = ProjectServiceProxy.Current.GetById(id) ?? new Project();
            DeleteCommand = new Command(DoDelete);
        }

        public ProjectViewModel(Project? model)
        {
            Model = model ?? new Project();
            DeleteCommand = new Command(DoDelete);
        }

        public void DoDelete()
        {
            DoDelete(Model);
        }

        public void DoDelete(Project? model)
        {
            if (Model != null)
            {
                ProjectServiceProxy.Current.DeleteProject(Model);
            }
        }

        public Project? Model { get ; set; }
        public ICommand? DeleteCommand { get; set; }

        public List<int> CompletePercentages
        {
            get
            {
                return new List<int> { 0, 25, 50, 75, 100 };
            }
        }

        public int SelectedCompletePercent { 
            get
            {
                return (int)(Model?.CompletePercent ?? 0);
            }
            set
            {
                if (Model != null && Model.CompletePercent != value)
                {
                    Model.CompletePercent = value;
                }
            }
        }

        public void AddOrUpdateProject()
        {
            if (Model != null)
            {
                ProjectServiceProxy.Current.AddOrUpdate(Model);
            }
        }

        public string CompletePercentDisplay
        {
            set
            {
                if(Model == null)
                {
                    return;
                }

                if (!int.TryParse(value, out int p))
                {
                    Model.CompletePercent = 0;
                }
                else
                {
                    Model.CompletePercent = p;
                }
            }

            get
            {
                return ((int)(Model?.CompletePercent ?? 0)).ToString();
            }
        }
    }
}
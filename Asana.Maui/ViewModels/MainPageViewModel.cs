using Asana.Library.Models;
using Asana.Library.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Maui.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private ToDoServiceProxy _toDoSvc;

        public MainPageViewModel()
        {
            _toDoSvc = ToDoServiceProxy.Current;
            Query = string.Empty;
        }

        public ToDoDetailViewModel SelectedToDo { get; set; }

        private string query;
        public string Query { 
            get { return query; }
            set {
                if (query != value)
                {
                    query = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public ObservableCollection<ToDoDetailViewModel> ToDos
        {
            get
            {
                var toDos = _toDoSvc.ToDos.Where(t => (t?.Name?.Contains(Query) ?? false) || (t?.Description?.Contains(Query) ?? false))
                        .Select(t => new ToDoDetailViewModel(t));
                if (!IsShowCompleted)
                {
                    toDos = toDos.Where(t => !t?.Model?.IsCompleted ?? false);
                }
                return new ObservableCollection<ToDoDetailViewModel>(toDos);
            }
        }

        public ObservableCollection<ProjectViewModel> Projects
        {
            get
            {
                var projectList 
                    = ProjectServiceProxy.Current
                    .Projects.Select(p => new ProjectViewModel(p));
                return new ObservableCollection<ProjectViewModel>(projectList);
            }
        }

        public int SelectedToDoId => SelectedToDo?.Model?.Id ?? 0;

        private bool isShowCompleted;
        public bool IsShowCompleted { 
            get
            {
                return isShowCompleted;
            }

            set
            {
                if (isShowCompleted != value)
                {
                    isShowCompleted = value;
                    NotifyPropertyChanged(nameof(ToDos));
                }
            }
        }

        public void DeleteToDo()
        {
            if (SelectedToDo == null)
            {
                return;
            }

            ToDoServiceProxy.Current.DeleteToDo(SelectedToDo?.Model?.Id ?? 0);
            NotifyPropertyChanged(nameof(ToDos));
        }

        public void RefreshPage()
        {
            NotifyPropertyChanged(nameof(ToDos));
        }

        public void HandleSearchClick()
        {
            RefreshPage();
            Query = string.Empty;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

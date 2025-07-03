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
            query = string.Empty; 
        }


        public ToDoDetailViewModel? SelectedToDo { get; set; }

    
        private string query = string.Empty;
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
                var todos = _toDoSvc.ToDos.Select(t => new ToDoDetailViewModel(t));
                if (!IsShowCompleted)
                {
                    todos = todos.Where(t => !t.Model?.IsCompleted ?? false);
                }
                return new ObservableCollection<ToDoDetailViewModel>(todos);
            }
        }

        public int SelectedToDoId => SelectedToDo?.Model?.Id ?? 0;

        private bool isShowCompleted;
        public bool IsShowCompleted { 
            get { return isShowCompleted; }
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
            if (SelectedToDo?.Model != null)
            {
                ToDoServiceProxy.Current.DeleteToDo(SelectedToDo.Model);
                NotifyPropertyChanged(nameof(ToDos));
            }
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
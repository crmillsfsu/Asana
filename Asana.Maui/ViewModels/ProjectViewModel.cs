using Asana.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Maui.ViewModels
{
    public class ProjectViewModel
    {
        public Project? Model { get; set; }

        public override string ToString()
        {
            return $"{Model?.Id ?? -1}. {Model?.Name}";
        }
    }
}

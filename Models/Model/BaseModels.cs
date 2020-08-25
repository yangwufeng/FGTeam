using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Model
{
    public class BaseModels : INotifyPropertyChanged
    {
        [Key]
        public int? Id { get; set; }

        public DateTime? Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? Updated { get; set; }

        public string Updatedby { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;



        protected void HandlerPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}

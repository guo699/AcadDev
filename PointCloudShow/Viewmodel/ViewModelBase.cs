using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace PointCloudShow.Viewmodel
{
    class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChange(string name)
        {
            if (PropertyChanged != null)
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

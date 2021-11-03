using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AcadPlugInCommon.MVVM
{
    public class ViewModelBase : INotifyPropertyChanged,INotifyPropertyChanging
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;

        protected void OnPropertyChanging(string propName)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propName));
        }
        protected void OnMultPropertyChanging(params string[] props)
        {
            foreach (var item in props)
            {
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(item));
            }
        }

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        protected void OnMultPropertyChanged(params string[] props)
        {
            foreach (var item in props)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(item));
            }
        }
    }
}

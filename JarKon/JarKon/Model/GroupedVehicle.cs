using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace JarKon.Model
{
    class GroupedVehicle : ObservableCollection<Vehicle>
    {
        public string LongName { get; set; }
        public string ShortName { get; set; }

    }
}

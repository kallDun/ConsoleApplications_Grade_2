using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager.Classes.Operations.Observers
{
    class SystemObserverSingleton
    {
        static SystemObserver observer;

        public SystemObserver GetInstance()
        {
            if (observer is null)
            {
                observer = new SystemObserver();
            }
            return observer;
        }
    }
}

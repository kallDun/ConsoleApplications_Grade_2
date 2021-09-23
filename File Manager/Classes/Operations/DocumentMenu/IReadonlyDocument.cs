using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager.Classes.Operations.DocumentMenu
{
    interface IReadonlyDocument
    {
        void OpenDocument(string path);

        bool CloseDocument();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager.Classes.Operations.DocumentMenu
{
    interface IDocument : IReadonlyDocument
    {
        void SaveDocument();

        void SaveDocumentAs(string path);

    }
}

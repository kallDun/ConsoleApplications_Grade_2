using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager.Classes.Operations.OpenFile
{
    abstract class OpeningFileFactory
    {
        public abstract void OpenFile(string path);
    }
}

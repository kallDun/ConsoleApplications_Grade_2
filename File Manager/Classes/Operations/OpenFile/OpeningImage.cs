using File_Manager.Classes.Operations.DocumentMenu;
using File_Manager.Classes.Views.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager.Classes.Operations.OpenFile
{
    class OpeningImage : OpeningFileFactory
    {
        public override void OpenFile(string path)
        {
            IReadonlyDocument window = new ImageReaderWindow();
            window.OpenDocument(path);
        }

    }
}

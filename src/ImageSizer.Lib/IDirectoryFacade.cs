using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSizer.Lib
{
    public interface IDirectoryFacade
    {
        IList<string> ReadPath(string path);        
    }
}

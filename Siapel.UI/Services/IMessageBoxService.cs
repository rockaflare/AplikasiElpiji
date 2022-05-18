using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Siapel.UI.Services
{
    public interface IMessageBoxService
    {
        bool ShowMessage(string message, string caption);
    }
}

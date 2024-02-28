using Microsoft.Win32;
using Newtonsoft.Json;
using Python.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ReceptFromHolodilnik.Services.Interfaces
{
    internal interface IPythonModel
    {
        public string SendMessageToAi(string str);
    }
}

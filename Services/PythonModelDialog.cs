using System;
using Python.Runtime;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceptFromHolodilnik.Services
{
    internal class PythonModelDialog
    {
        public string SendMessageToAi(string str)
        {
            Runtime.PythonDLL = "";
            PythonEngine.Initialize();
            using(Py.GIL())
            {
                var pythonScript = Py.Import("mypythonscript");
                var message = new PyString(str);

                var result = pythonScript.InvokeMethod("say_hello",
                    new PyObject[] { message });
                return result.ToString();
            }
        }
    }
}

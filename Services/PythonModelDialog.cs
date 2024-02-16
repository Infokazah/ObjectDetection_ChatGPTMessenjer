using System;
using Python.Runtime;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace ReceptFromHolodilnik.Services
{
    internal class PythonModelDialog
    {
        public PythonModelDialog() 
        {
            Runtime.PythonDLL = "C:\\Users\\Влад\\AppData\\Local\\Programs\\Python\\Python312\\python312.dll";
            PythonEngine.Initialize();
        }  
        public string SendMessageToAi(string str)
        {
            using(Py.GIL())
            {
                var pythonScript = Py.Import("ask_gpt");
                var message = new PyString(str);

                var result = pythonScript.InvokeMethod("ask_gpt",
                    new PyObject[] { message });
                return result.ToString();
            }
        }
    }
}

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
        public string SendMessageToAi(string str)
        {
            Runtime.PythonDLL = "C:\\Users\\Влад\\AppData\\Local\\Programs\\Python\\Python312\\python312.dll";
            PythonEngine.Initialize();
            using(Py.GIL())
            {
                var pythonScript = Py.Import("ask_gpt.py");
                var message = new PyString(str);

                var result = pythonScript.InvokeMethod("ask_gpt",
                    new PyObject[] { message });
                MessageBox.Show(result.ToString());
                return result.ToString();
            }
        }
    }
}

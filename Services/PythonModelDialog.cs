using System;
using Python.Runtime;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using Microsoft.Win32;
using Newtonsoft.Json;


namespace ReceptFromHolodilnik.Services
{
    internal class PythonModelDialog
    {
        public PythonModelDialog() 
        {
            string jsonString = "";
            string filePath = "found_file.json";

            if (File.Exists(filePath))
            {
                jsonString = File.ReadAllText(filePath);
                jsonString = jsonString.Replace("\"", "");
            }
            else
            {
                throw new Exception();
            }

            if(jsonString.Length == 0) 
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                if (openFileDialog.ShowDialog() == true)
                {
                    jsonString = openFileDialog.FileName;

                    File.WriteAllText(filePath, JsonConvert.SerializeObject(jsonString, Formatting.Indented));
                }
                else
                {
                    MessageBox.Show("Файлы не выбраны.");
                }
            }
            Runtime.PythonDLL = jsonString;
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

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
using ReceptFromHolodilnik.Services.Interfaces;
using System.Diagnostics;


namespace ReceptFromHolodilnik.Services
{
    internal class PythonModelDialog : IPythonModel
    {
        public PythonModelDialog() 
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.RedirectStandardInput = true;
           
            process.Start();

            
            process.StandardInput.WriteLine("pip install -U g4f[all]");

           
            process.WaitForExit();
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
            try 
            {

                using (Py.GIL())
                {
                    var pythonScript = Py.Import("ask_gpt");
                    var message = new PyString(str);

                    var result = pythonScript.InvokeMethod("ask_gpt", new PyObject[] { message });

                    return result.ToString();
                }
            }
            catch (Exception ex) 
            {
                return "Ебал всё это";
            }
        }
    }
}

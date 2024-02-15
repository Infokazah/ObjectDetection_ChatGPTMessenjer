using ReceptFromHolodilnik.Infrastructure.Commands.Base;
using ReceptFromHolodilnik.ViewModel.Base;
using ReceptFromHolodilnik.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using ReceptFromHolodilnik.Infrastructure.Commands;
using System.Collections.ObjectModel;
using ReceptFromHolodilnik.Models;
using Microsoft.Win32;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ReceptFromHolodilnik.Services;

namespace ReceptFromHolodilnik.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private YoloDialog _yoloModel;
        private PythonModelDialog _pythonModel;
        private ObservableCollection<Message> _messages;

        public ObservableCollection<Message> Messages
        {
            get => _messages;
            private set 
            {
                _messages = value;
                OnPropertyChanged(nameof(Messages));
            }
        }

        private string _currentMessage;
        public string CurrentMessage
        {
            set
            {
                _currentMessage = value;
                OnPropertyChanged(nameof(CurrentMessage));
            }
            get => _currentMessage;
        }
        private string _currentObjects;
        public string CurrentObjects
        {
            set
            {
                _currentObjects = value;
                OnPropertyChanged(nameof(CurrentObjects));
            }
            get => _currentObjects;
        }

        public RegularCommand SendMessage { get; }

        private bool CanSendMessageExecute(object p) => true;

        private void SendMessageExecute(object message)
        {
            if(CurrentMessage != "")
            {
                Messages.Add(new Message(CurrentMessage));
                _pythonModel.SendMessageToAi(CurrentMessage);
                CurrentMessage = "";
                OnPropertyChanged(nameof(Messages));
            }
        }


        private ImageSource _filePath;

        public ImageSource FilePath
        {
            get => _filePath;
            private set { _filePath = value; OnPropertyChanged(nameof(FilePath));}
        }
        public RegularCommand ChooseImage { get; }

        private bool CanChooseImageExecute(object p) => true;

        private void ChooseImageExecute(object message)
        {
            var dialog = new OpenFileDialog
            {
                Title = "Выбор изображения",
                CheckFileExists = true,
            };
            if (dialog.ShowDialog() != true) return;

            BitmapImage image = new BitmapImage(new Uri(dialog.FileName));
            FilePath = image;

            List <string> DetectedObjects = _yoloModel.DetectObjects(image);
            foreach (var detectedObject in DetectedObjects)
            {
                CurrentObjects += $"{detectedObject}|";
            }
            CurrentMessage = $"Что можно приготовить из данных продуктов:{CurrentObjects}";
        }

        public MainWindowViewModel()
        {
            Messages = new ObservableCollection<Message>();
            SendMessage = new RegularCommand(SendMessageExecute, CanSendMessageExecute);
            ChooseImage = new RegularCommand(ChooseImageExecute, CanChooseImageExecute);
            _yoloModel = new YoloDialog();
            _pythonModel = new PythonModelDialog();
        }

    }
}

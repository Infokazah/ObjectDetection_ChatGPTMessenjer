using System;
using System.Collections.Generic;
using System.Windows;
using System.Collections.ObjectModel;
using ReceptFromHolodilnik.Models;
using Microsoft.Win32;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ReceptFromHolodilnik.Services;
using BaseClassesLyb;
using ReceptFromHolodilnik.Services.Interfaces;
using System.Threading.Tasks;

namespace ReceptFromHolodilnik.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {

        //поля для моделей
        private YoloDialog _yoloModel;
        private PythonModelDialog _pythonModel;
        private YouTubeFindService _youtubeService;
        #region Аттрибуты
        //путь до изображения
        private string _videoLincs;

        public string VideoLincs
        {
            get => _videoLincs;
            private set { _videoLincs = value; OnPropertyChanged(nameof(VideoLincs)); }
        }

        private ImageSource _filePath;

        public ImageSource FilePath
        {
            get => _filePath;
            private set { _filePath = value; OnPropertyChanged(nameof(FilePath)); }
        }
        //список для переписки
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
        //текущее сообщение
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
        //список обнаруженных объектов
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
        #endregion
        #region Команды
        //отправка сообщений
        public RegularCommand SendMessage { get; }

        private bool CanSendMessageExecute(object p) => true;

        private async void SendMessageExecute(object message)
        {
            if (!string.IsNullOrEmpty(CurrentMessage))
            {
                Messages.Add(new Message(CurrentMessage));
                OnPropertyChanged(nameof(Messages));

                string result = _pythonModel.SendMessageToAi(CurrentMessage);
                Message messageModel = new Message(result, HorizontalAlignment.Left);
                Messages.Add(messageModel);
                VideoLincs = await _youtubeService.SearcnVideo(result);
                messageModel = new Message(result, HorizontalAlignment.Left);
                Messages.Add(messageModel);
                CurrentMessage = "";
            }
        }
        //выбор изображения
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
            CurrentObjects = "";
            foreach (var detectedObject in DetectedObjects)
            {
                CurrentObjects += $"{detectedObject}|";
            }
            CurrentMessage = $"Что можно приготовить из данных продуктов:{CurrentObjects}";
        }
        #endregion

        public MainWindowViewModel(IPythonModel py, IYoloDialog yolo, IYouTubeService youTube)
        {
            Messages = new ObservableCollection<Message>();
            SendMessage = new RegularCommand(SendMessageExecute, CanSendMessageExecute);
            ChooseImage = new RegularCommand(ChooseImageExecute, CanChooseImageExecute);
            _yoloModel = (YoloDialog?)yolo;
            _pythonModel = (PythonModelDialog?)py;
            _youtubeService = (YouTubeFindService)youTube;
        }

    }
}

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

namespace ReceptFromHolodilnik.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
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

        public RegularCommand SendMessage { get; }

        private bool CanSendMessageExecute(object p) => true;

        private void SendMessageExecute(object message)
        {
            if(CurrentMessage != "")
            {
                Messages.Add(new Message(CurrentMessage));
                CurrentMessage = "";
                OnPropertyChanged(nameof(Messages));
            }
        }

        public MainWindowViewModel()
        {
            Messages = new ObservableCollection<Message>();
            SendMessage = new RegularCommand(SendMessageExecute, CanSendMessageExecute);
        }

    }
}

using ReceptFromHolodilnik.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ReceptFromHolodilnik.ViewModels
{
    internal class ViewModelLocator
    {
        public MainWindowViewModel MainWindowViewModel => App.Host.Services.GetRequiredService<MainWindowViewModel>();
        public PythonModelDialog GetInRitm => App.Host.Services.GetRequiredService<PythonModelDialog>();
        public YoloDialog GetInMelody => App.Host.Services.GetRequiredService<YoloDialog>();
    }
}

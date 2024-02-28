using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace ReceptFromHolodilnik.Services.Interfaces
{
    internal interface IYoloDialog
    {
        public List<string> DetectObjects(BitmapImage bitmapImage);
    }
}

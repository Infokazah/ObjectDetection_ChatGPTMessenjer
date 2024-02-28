using FastYolo.Model;
using FastYolo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media.Imaging;
using ReceptFromHolodilnik.Services.Interfaces;

namespace ReceptFromHolodilnik.Services
{
    internal class YoloDialog : IYoloDialog
    {
        private YoloWrapper YoloModel;
        public YoloDialog()
        {
            YoloModel = new YoloWrapper(
                Path.Combine(Directory.GetCurrentDirectory(),"Yolo\\yolov3.cfg"), 
                Path.Combine(Directory.GetCurrentDirectory(), "Yolo\\yolov3.weights"), 
                Path.Combine(Directory.GetCurrentDirectory(), "Yolo\\coco.names"));
        }

        public List<string> DetectObjects(BitmapImage bitmapImage) 
        {
            byte[] byteArray;
            using (MemoryStream stream = new MemoryStream())
            {
                BitmapEncoder encoder = new PngBitmapEncoder(); 
                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                encoder.Save(stream);
                byteArray = stream.ToArray();
            }

            IEnumerable<YoloItem> yoloItems = YoloModel.Detect(byteArray);

            List<string> result = new List<string>();

            foreach (var item in yoloItems) 
            {
                if(!result.Contains(item.Type))
                {
                    result.Add(item.Type);
                }
            }
            if(result.Count == 0) 
            {
                result.Add("Продукты не обнаружены");
            }
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ReceptFromHolodilnik.Services.Interfaces
{
    internal interface IYouTubeService
    {
        public  async Task SearcnVideo(string stroke) => throw new NotImplementedException();
        private  List<string> ExtractKeywords(string input) => throw new NotImplementedException();
        private async Task<List<string>> SearchVideos(string keyword) => throw new NotImplementedException();
    }
}

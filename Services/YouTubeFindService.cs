using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using ReceptFromHolodilnik.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ReceptFromHolodilnik.Services
{
    internal class YouTubeFindService : IYouTubeService
    {
        public async Task<string> SearcnVideo(string input)
        {
            string videoList = "Подробнее о рецептах можно посмотреть в видеороликах ниже:\n";
            // Извлечение слов, обведенных в %
            List<string> keywords = ExtractKeywords(input);

            if (keywords.Count == 0)
            {
                return videoList;
            }

            // Поиск видео по каждому ключевому слову
            foreach (string keyword in keywords)
            {
                List<string> videoIds = await SearchVideos(keyword);
                if (videoIds.Count == 0)
                {
                    throw new ArgumentNullException("Отсутствуют рецепты");
                }
                else
                {
                    foreach (string videoId in videoIds)
                    {
                        videoList = $"{videoList};\nhttps://www.youtube.com/watch?v={videoId}";
                    }
                }
            }
            return videoList;

        }

        private List<string> ExtractKeywords(string input)
        {
            List<string> keywords = new List<string>();
            MatchCollection matches = Regex.Matches(input, @"%([^%]+)%");
            foreach (Match match in matches)
            {
                keywords.Add(match.Groups[1].Value);
            }
            return keywords;
        }

        private async Task<List<string>> SearchVideos(string keyword)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyDkJCWPRkC2ImPqNyaohHVzQ7N4oLNwElc", 
                ApplicationName = "YouTube Search"
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = keyword; 
            searchListRequest.MaxResults = 1; 
            searchListRequest.Type = "video"; 

            var searchListResponse = await searchListRequest.ExecuteAsync();

            List<string> videoIds = new List<string>();
            foreach (var searchResult in searchListResponse.Items)
            {
                videoIds.Add(searchResult.Id.VideoId);
            }

            return videoIds;
        }
    }
}

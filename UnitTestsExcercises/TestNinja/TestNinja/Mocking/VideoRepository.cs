using System.Collections.Generic;
using System.Linq;

namespace TestNinja.Mocking
{
    public class VideoRepository : IVideoRepository
    {
        public IEnumerable<Video> GetUnprocessedVideos()
        {
            List<Video> videos = new List<Video>();

            using (VideoContext context = new VideoContext())
            {
                videos =
               (from video in context.Videos
                where !video.IsProcessed
                select video).ToList();
            }

            return videos;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace TestNinja.Mocking
{
    public class VideoService
    {
        IFileReader fileReader;

        public VideoService(IFileReader fileReader)
        {
            this.fileReader = fileReader;
        }
        public string ReadVideoTitle()
        {
            string str = this.fileReader.Read("video.txt");
            var video = JsonConvert.DeserializeObject<Video>(str);
            if (video == null)
                return "Error parsing the video.";

            return video.Title;
        }

        public string GetUnprocessedVideosAsCsv(IVideoContext videoContext)
        {
            var videoIds = new List<int>();

            using (IVideoContext context = videoContext)
            {
                foreach (Video v in context.GetVideos())
                    videoIds.Add(v.Id);
            }

            return string.Join(",", videoIds);
        }
    }

    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsProcessed { get; set; }
    }

    public interface IVideoContext : IDisposable
    {
        IEnumerable<Video> GetVideos();
    }

    public class VideoContext : DbContext, IVideoContext
    {
        private DbSet<Video> videos;
        public VideoContext(DbSet<Video> videos)
        {
            this.videos = videos;
        }
        public IEnumerable<Video> GetVideos()
        {
            return videos.ToList();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GetFileAttributes.Data_Access;

namespace GetFileAttributes.Business_Logic
{
    class BusinessLogic
    {
        DataAccess DA = new DataAccess();

        public string AlbumArtist { get; set; }
        public string Genre { get; set; }
        public string AlbumTitle { get; set; }
        public int Year { get; set; }
        public string Type { get; set; }
        public int TrackNumber { get; set; }
        public string TrackTitle { get; set; }
        public TimeSpan Duration { get; set; }
        public decimal FileSize { get; set; }
        public int Bitrate { get; set; }
        public string FilePath { get; set; }

        public BusinessLogic(string albumArtist, string genre, string albumTitle, int year, string type, int trackNumber, string trackTitle, TimeSpan duration, decimal fileSize, int bitrate, string filePath)
        {
            AlbumArtist = albumArtist;
            Genre = genre;
            AlbumTitle = albumTitle;
            Year = year;
            Type = type;
            TrackNumber = trackNumber;
            TrackTitle = trackTitle;
            Duration = duration;
            FileSize = fileSize;
            Bitrate = bitrate;
            FilePath = filePath;
        }

        public BusinessLogic()
        {

        }

    }
}

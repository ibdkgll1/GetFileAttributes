using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagLib;
using GetFileAttributes.Business_Logic;

namespace GetFileAttributes
{
    class Program
    {
        static void Main(string[] args)
        {
            BusinessLogic BL = new BusinessLogic();

            string source = "N:\\Music\\Full CD\\AC DC\\1975 - High Voltage (Australian)\\";
            string file = "01. Baby Please Don't Go.mp3";

            TagLib.File tlf = TagLib.File.Create(source + file);

            BL.AlbumArtist = tlf.Tag.Performers[0].ToString();
            BL.Genre = tlf.Tag.Genres.First(s => !string.IsNullOrEmpty(s));
            BL.AlbumTitle = tlf.Tag.Album;
            BL.Year = Convert.ToInt32(tlf.Tag.Year);
            BL.Type = tlf.MimeType.Substring(tlf.MimeType.Length - 3);
            BL.TrackNumber = Convert.ToInt32(tlf.Tag.Track);
            BL.TrackTitle = tlf.Tag.Title;
            BL.Duration = tlf.Properties.Duration;
            BL.Bitrate = tlf.Properties.AudioBitrate;
            BL.FileSize = new System.IO.FileInfo(source + file).Length;
            BL.FilePath = source;

            Console.WriteLine("Artist: " + BL.AlbumArtist);
            Console.WriteLine("Genre: " + BL.Genre);
            Console.WriteLine("Album: " + BL.AlbumTitle);
            Console.WriteLine("Year: " + BL.Year);
            Console.WriteLine("Type: " + BL.Type);
            Console.WriteLine("Track: " + BL.TrackNumber);
            Console.WriteLine("Title: " + BL.TrackTitle);
            Console.WriteLine("Duration: " + BL.Duration);
            Console.WriteLine("BitRate: " + BL.Bitrate);
            Console.WriteLine("Size: " + BL.FileSize);
            Console.WriteLine("Path: " + BL.FilePath);

            Console.WriteLine(" ");
            Console.WriteLine("Press Any Key to Continue...");
            Console.ReadLine();

        }


    }
}

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
        public static int artistRowsInserted = 0;
        public static int artistFileRowsInserted = 0;
        public static int artistSongRowsInserted = 0;

        static void Main(string[] args)
        {
            try
            {
                //string source = "N:\\Music\\Full CD\\AC DC\\1984 - Jailbreak (Live)\\";                   // Test Line...
                string source = "N:\\Music\\Full CD\\AC DC\\";                                              // Prod Line...
                string[] filePaths = Directory.GetFiles(source, "*.mp3", SearchOption.AllDirectories);

                string path = string.Empty;
                string file = string.Empty;

                foreach (string item in filePaths)
                {
                    string[] itemSplit = item.Split('\\');

                    path = source + itemSplit[4] + "\\";                                                    // Prod Line...
                    //path = source;                                                                        // Test Line...
                    file = itemSplit[5];
                    processSong(path, file);
                }

                Console.WriteLine("Artist Rows Inserted......: " + artistRowsInserted.ToString());
                Console.WriteLine("Artist File Rows Inserted.: " + artistFileRowsInserted.ToString());
                Console.WriteLine("Artist Song Rows Inserted.: " + artistSongRowsInserted.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void processSong(string source, string file)
        {
            try
            {
                BusinessLogic BL = new BusinessLogic();
                string connectionString = Properties.Settings.Default.CONNECTION_STRING;

                TagLib.File tlf = TagLib.File.Create(source + file);

                if (tlf.Tag.Performers.Length > 0)
                {
                    BL.AlbumArtist = tlf.Tag.Performers[0].ToString();
                }
                else
                {
                    BL.AlbumArtist = " ";
                }

                if (tlf.Tag.Genres.Length > 0)
                {
                    BL.Genre = tlf.Tag.Genres.First(s => !string.IsNullOrEmpty(s));
                }
                else
                {
                    BL.Genre = " ";
                }

                if (tlf.Tag.Album != null)
                {
                    BL.AlbumTitle = tlf.Tag.Album;
                }
                else
                {
                    string[] pathSplit = source.Split('\\');
                    BL.AlbumTitle = pathSplit[4];
                }

                BL.Year = Convert.ToInt32(tlf.Tag.Year);
                BL.Type = tlf.MimeType.Substring(tlf.MimeType.Length - 3);
                BL.TrackNumber = Convert.ToInt32(tlf.Tag.Track);

                if (tlf.Tag.Title != null)
                {
                    BL.TrackTitle = tlf.Tag.Title;
                }
                else
                {
                    BL.TrackTitle = file.Substring(0, file.Length - 4);
                }
                
                BL.Duration = tlf.Properties.Duration;
                BL.Bitrate = tlf.Properties.AudioBitrate;
                BL.FileSize = new System.IO.FileInfo(source + file).Length;
                BL.FilePath = source;
                BL.FileCreated = System.IO.File.GetCreationTime(source + file);
                BL.FileModified = System.IO.File.GetLastWriteTime(source + file);

                int currentKeyFromArtist = BL.getCurrentKeyFromArtistBL(connectionString);

                artistRowsInserted = 0;
                Boolean existingArtist = BL.checkForExistingArtistBL(connectionString, BL.AlbumArtist);
                if (existingArtist == false)
                {
                    artistRowsInserted = BL.insertArtistBL(connectionString, 1, BL.AlbumArtist);
                }

                artistFileRowsInserted = 0;
                Boolean existingArtistFile = BL.checkForExistingArtistFileBL(connectionString, file);
                if (existingArtistFile == false)
                {
                    artistFileRowsInserted = BL.insertArtistFileBL(connectionString, currentKeyFromArtist, BL.FilePath, file, BL.FileCreated, BL.FileModified, BL.FileSize, BL.Type, BL.Bitrate);
                }

                artistSongRowsInserted = 0;
                Boolean existingArtistSong = BL.checkForExistingArtistSongBL(connectionString, BL.TrackTitle, BL.AlbumTitle);
                if (existingArtistSong == false)
                {
                    artistSongRowsInserted = BL.insertArtistSongBL(connectionString, currentKeyFromArtist, BL.TrackTitle, BL.AlbumTitle, BL.Genre, BL.Year, BL.TrackNumber, BL.Duration);
                }

                //Console.WriteLine("Path.....: " + BL.FilePath);
                //Console.WriteLine("File.....: " + file);
                //Console.WriteLine("Created..: " + BL.FileCreated);
                //Console.WriteLine("Modified.: " + BL.FileModified);
                //Console.WriteLine("Size.....: " + BL.FileSize);
                //Console.WriteLine("Type.....: " + BL.Type);
                //Console.WriteLine("BitRate..: " + BL.Bitrate);

                //Console.WriteLine(" ");

                //Console.WriteLine("Artist...: " + BL.AlbumArtist);
                //Console.WriteLine("Title....: " + BL.TrackTitle);
                //Console.WriteLine("Album....: " + BL.AlbumTitle);
                //Console.WriteLine("Genre....: " + BL.Genre);
                //Console.WriteLine("Year.....: " + BL.Year);
                //Console.WriteLine("Track....: " + BL.TrackNumber);
                //Console.WriteLine("Duration.: " + BL.Duration);

                //Console.WriteLine(" ");

                //Console.WriteLine("Current Key...............: " + currentKeyFromArtist.ToString());

                //Console.WriteLine(" ");

                //Console.WriteLine("Artist Rows Inserted......: " + artistRowsInserted.ToString());
                //Console.WriteLine("Artist File Rows Inserted.: " + artistFileRowsInserted.ToString());
                //Console.WriteLine("Artist Song Rows Inserted.: " + artistSongRowsInserted.ToString());

                //Console.WriteLine(" ");
                //Console.WriteLine("Press Any Key to Continue...");
                //Console.ReadLine();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}

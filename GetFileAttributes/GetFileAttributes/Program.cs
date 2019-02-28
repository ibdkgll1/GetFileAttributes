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
                MusicLogic ML = new MusicLogic();

                Guid id = Guid.NewGuid();
                string artistKey = id.ToString();

                id = Guid.NewGuid();
                string artistItemKey = id.ToString();
                
                string connectionString = Properties.Settings.Default.CONNECTION_STRING;

                TagLib.File tlf = TagLib.File.Create(source + file);

                if (tlf.Tag.Performers.Length > 0)
                {
                    ML.AlbumArtist = tlf.Tag.Performers[0].ToString();
                }
                else
                {
                    ML.AlbumArtist = " ";
                }

                if (tlf.Tag.Genres.Length > 0)
                {
                    ML.Genre = tlf.Tag.Genres.First(s => !string.IsNullOrEmpty(s));
                }
                else
                {
                    ML.Genre = " ";
                }

                if (tlf.Tag.Album != null)
                {
                    ML.AlbumTitle = tlf.Tag.Album;
                }
                else
                {
                    string[] pathSplit = source.Split('\\');
                    ML.AlbumTitle = pathSplit[4];
                }

                ML.Year = Convert.ToInt32(tlf.Tag.Year);
                ML.Type = tlf.MimeType.Substring(tlf.MimeType.Length - 3);
                ML.TrackNumber = Convert.ToInt32(tlf.Tag.Track);

                if (tlf.Tag.Title != null)
                {
                    ML.TrackTitle = tlf.Tag.Title;
                }
                else
                {
                    ML.TrackTitle = file.Substring(0, file.Length - 4);
                }

                ML.Duration = tlf.Properties.Duration;
                ML.Bitrate = tlf.Properties.AudioBitrate;
                ML.FileSize = new System.IO.FileInfo(source + file).Length;
                ML.FilePath = source;
                ML.FileCreated = System.IO.File.GetCreationTime(source + file);
                ML.FileModified = System.IO.File.GetLastWriteTime(source + file);

                string currentKeyFromArtist = BL.getCurrentKeyFromArtistBL(connectionString, ML.AlbumArtist);
                if (currentKeyFromArtist.Length > 0)
                {
                    artistKey = currentKeyFromArtist;                                               // Over Write The Set Artist Key...
                }

                if (ML.AlbumArtist.Trim().Length > 0)
                {
                    artistRowsInserted = 0;
                    Boolean existingArtist = BL.checkForExistingArtistBL(connectionString, ML.AlbumArtist);
                    if (existingArtist == false)
                    {
                        artistRowsInserted = BL.insertArtistBL(connectionString, artistKey, ML.AlbumArtist);
                    }
                }

                artistFileRowsInserted = 0;
                Boolean existingArtistFile = BL.checkForExistingArtistFileBL(connectionString, file);
                if (existingArtistFile == false)
                {
                    artistFileRowsInserted = BL.insertArtistFileBL(connectionString, artistKey, artistItemKey, ML.FilePath, file, ML.FileCreated, ML.FileModified, ML.FileSize, ML.Type, ML.Bitrate);
                }

                artistSongRowsInserted = 0;
                Boolean existingArtistSong = BL.checkForExistingArtistSongBL(connectionString, ML.TrackTitle, ML.AlbumTitle);
                if (existingArtistSong == false)
                {
                    artistSongRowsInserted = BL.insertArtistSongBL(connectionString, artistKey, artistItemKey, ML.TrackTitle, ML.AlbumTitle, ML.Genre, ML.Year, ML.TrackNumber, ML.Duration);
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

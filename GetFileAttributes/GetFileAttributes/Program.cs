using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using TagLib;
using GetFileAttributes.Business_Logic;

namespace GetFileAttributes
{
    class Program
    {
        public static int recordCount = 0;
        public static int totalArtistCount = 0;
        public static int totalArtistRowsInserted = 0;
        public static int totalArtistFileRowsInserted = 0;
        public static int totalArtistSongRowsInserted = 0;

        public static string prevArtistName = string.Empty;
        public static string processingItem = string.Empty;

        static void Main(string[] args)
        {
            try
            {
                //string source = "N:\\Music\\Full CD\\AC DC\\1984 - Jailbreak (Live)\\";                           // Test Line...
                string source = "N:\\Music\\Full CD\\";                                                             // Prod Line...
                string[] filePaths = Directory.GetFiles(source, "*.mp3", SearchOption.AllDirectories);
                Array.Sort(filePaths);

                string path = string.Empty;
                string file = string.Empty;

                string artistName = string.Empty;                
                string artistAlbumName = string.Empty;

                foreach (string item in filePaths)
                {
                    recordCount = recordCount + 1;
                    processingItem = item;

                    string[] itemSplit = item.Split('\\');

                    switch(itemSplit.Length)
                    {
                        case 5:
                            path = source + itemSplit[3] + "\\";                                                    // Prod Line...
                            //path = source;                                                                        // Test Line...
                            file = itemSplit[4];

                            artistName = itemSplit[3];
                            artistAlbumName = itemSplit[4];
                            break;
                        case 6:
                            path = source + itemSplit[3] + "\\" + itemSplit[4] + "\\";                              // Prod Line...
                            //path = source;                                                                        // Test Line...
                            file = itemSplit[5];

                            artistName = itemSplit[3];
                            artistAlbumName = itemSplit[4];
                            break;
                        case 7: 
                            path = source + itemSplit[3] + "\\" + itemSplit[4] + "\\" + itemSplit[5] + "\\";        // Prod Line...
                            //path = source;                                                                        // Test Line...
                            file = itemSplit[6];

                            artistName = itemSplit[3];
                            artistAlbumName = itemSplit[4];
                            break;
                        default:
                            break;
                    }

                    if (prevArtistName != artistName)
                    {
                        prevArtistName = artistName;
                        totalArtistCount = totalArtistCount + 1;
                    }

                    if (totalArtistCount < 10)
                    {
                        //Debug.WriteLine("Path: " + path + ", File: " + file + ", Artist: " + artistName + ", Album: " + artistAlbumName);
                        processSong(path, file, artistName, artistAlbumName);
                    }
                    else
                        break;
                }

                Console.WriteLine(" ");
                Console.WriteLine("Total Artist Count........: " + totalArtistCount.ToString());
                Console.WriteLine("Artist Rows Inserted......: " + totalArtistRowsInserted.ToString());
                Console.WriteLine("Artist File Rows Inserted.: " + totalArtistFileRowsInserted.ToString());
                Console.WriteLine("Artist Song Rows Inserted.: " + totalArtistSongRowsInserted.ToString());
                Console.WriteLine(" ");
                Console.WriteLine("Process Completed Successfully...");
                Console.WriteLine(" ");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Processing Item: " + processingItem);
                Console.WriteLine("Previous Artist: " + prevArtistName);
                Console.WriteLine(" ");
                Console.WriteLine("Exception: " + ex.Message);
                Console.ReadLine();
                throw new Exception(ex.Message);
            }
        }

        public static void processSong(string source, string file, string artistName, string artistAlbumName)
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

                if (tlf.Tag.Genres.Length > 0)
                {
                    ML.Genre = tlf.Tag.Genres.First(s => !string.IsNullOrEmpty(s));
                }
                else
                {
                    ML.Genre = " ";
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

                string currentKeyFromArtist = BL.getCurrentKeyFromArtistBL(connectionString, artistName);
                if (currentKeyFromArtist.Length > 0)
                {
                    artistKey = currentKeyFromArtist;                                               // Over Write The Set Artist Key...
                }

                int artistRowsInserted = 0;
                Boolean existingArtist = BL.checkForExistingArtistBL(connectionString, artistKey);
                if (existingArtist == false)
                {
                    artistRowsInserted = BL.insertArtistBL(connectionString, artistKey, artistName);
                    totalArtistRowsInserted = totalArtistRowsInserted + artistRowsInserted;
                }

                int artistFileRowsInserted = 0;
                Boolean existingArtistFile = BL.checkForExistingArtistFileBL(connectionString, source, file);
                if (existingArtistFile == false)
                {
                    artistFileRowsInserted = BL.insertArtistFileBL(connectionString, artistKey, artistItemKey, ML.FilePath, file, ML.FileCreated, ML.FileModified, ML.FileSize, ML.Type, ML.Bitrate);
                    totalArtistFileRowsInserted = totalArtistFileRowsInserted + artistFileRowsInserted;
                }

                int artistSongRowsInserted = 0;
                Boolean existingArtistSong = BL.checkForExistingArtistSongBL(connectionString, ML.TrackTitle, artistAlbumName);
                if (existingArtistSong == false)
                {
                    artistSongRowsInserted = BL.insertArtistSongBL(connectionString, artistKey, artistItemKey, ML.TrackTitle, artistAlbumName, ML.Genre, ML.Year, ML.TrackNumber, ML.Duration);
                    totalArtistSongRowsInserted = totalArtistSongRowsInserted + artistSongRowsInserted;
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
                Console.WriteLine("Record Count: " + recordCount.ToString());
                Console.WriteLine("Artist Rows Inserted......: " + totalArtistRowsInserted.ToString());
                Console.WriteLine("Artist File Rows Inserted.: " + totalArtistFileRowsInserted.ToString());
                Console.WriteLine("Artist Song Rows Inserted.: " + totalArtistSongRowsInserted.ToString());
                Console.ReadLine();
                throw new Exception(ex.Message);
            }
        }

    }
}

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
        public DateTime FileCreated { get; set; }
        public DateTime FileModified { get; set; }

        public BusinessLogic(string albumArtist, string genre, string albumTitle, int year, string type, int trackNumber, string trackTitle, TimeSpan duration, decimal fileSize, int bitrate, string filePath, DateTime fileCreated, DateTime fileModified)
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
            FileCreated = fileCreated;
            FileModified = fileModified;
        }

        public BusinessLogic()
        {

        }

        public Boolean checkForExistingArtistBL(string connection, string artistName)
        {
            try
            {
                Boolean existingArtist = DA.checkForExistingArtistDA(connection, artistName);
                return existingArtist;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int getCurrentKeyFromArtistBL(string connection)
        {
            try
            {
                int currentKeyFromArtist = DA.getCurrentKeyFromArtistDA(connection);
                return currentKeyFromArtist;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int insertArtistBL(string connection, int artistId, string artistName)
        {
            try
            {
                int recordsInserted = DA.insertArtistDA(connection, artistId, artistName);
                return recordsInserted;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Boolean checkForExistingArtistFileBL(string connection, string artistFile)
        {
            try
            {
                Boolean existingArtistFile = DA.checkForExistingArtistFileDA(connection, artistFile);
                return existingArtistFile;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int insertArtistFileBL(string connection, int artistId, string artistPath, string artistFile, DateTime artistFileCreated, DateTime artistFileModified, decimal artistFileSize, string artistFileType, int artistFileBitRate)
        {
            try
            {
                int recordsInserted = DA.insertArtistFileDA(connection, artistId, artistPath, artistFile, artistFileCreated, artistFileModified, artistFileSize, artistFileType, artistFileBitRate);
                return recordsInserted;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Boolean checkForExistingArtistSongBL(string connection, string artistTitle, string albumTitle)
        {
            try
            {
                Boolean existingArtistSong = DA.checkForExistingArtistSongDA(connection, artistTitle, albumTitle);
                return existingArtistSong;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int insertArtistSongBL(string connection, int artistId, string artistTitle, string artistAlbum, string artistGenre, int artistYear, int artistTrackNumber, TimeSpan artistDuration)
        {
            try
            {
                int recordsInserted = DA.insertArtistSongDA(connection, artistId, artistTitle, artistAlbum, artistGenre, artistYear, artistTrackNumber, artistDuration);
                return recordsInserted;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}

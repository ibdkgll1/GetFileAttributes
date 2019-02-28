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

        public Boolean checkForExistingArtistBL(string connection, string artistKey)
        {
            try
            {
                Boolean existingArtist = DA.checkForExistingArtistDA(connection, artistKey);
                return existingArtist;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string getCurrentKeyFromArtistBL(string connection, string artistName)
        {
            try
            {
                string currentKeyFromArtist = DA.getCurrentKeyFromArtistDA(connection, artistName);
                return currentKeyFromArtist;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int insertArtistBL(string connection, string artistKey, string artistName)
        {
            try
            {
                int recordsInserted = DA.insertArtistDA(connection, artistKey, artistName);
                return recordsInserted;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Boolean checkForExistingArtistFileBL(string connection, string artistPath, string artistFile)
        {
            try
            {
                Boolean existingArtistFile = DA.checkForExistingArtistFileDA(connection, artistPath, artistFile);
                return existingArtistFile;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int insertArtistFileBL(string connection, string artistKey, string artistItemKey, string artistPath, string artistFile, DateTime artistFileCreated, DateTime artistFileModified, decimal artistFileSize, string artistFileType, int artistFileBitRate)
        {
            try
            {
                int recordsInserted = DA.insertArtistFileDA(connection, artistKey, artistItemKey, artistPath, artistFile, artistFileCreated, artistFileModified, artistFileSize, artistFileType, artistFileBitRate);
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

        public int insertArtistSongBL(string connection, string artistKey, string artistItemKey, string artistTitle, string artistAlbum, string artistGenre, int artistYear, int artistTrackNumber, TimeSpan artistDuration)
        {
            try
            {
                int recordsInserted = DA.insertArtistSongDA(connection, artistKey, artistItemKey, artistTitle, artistAlbum, artistGenre, artistYear, artistTrackNumber, artistDuration);
                return recordsInserted;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}

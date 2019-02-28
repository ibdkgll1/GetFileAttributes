using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetFileAttributes.Data_Access
{
    class DataAccess
    {

        public Boolean checkForExistingArtistDA(string connection, string artistKey)
        {
            try
            {
                Boolean existingArtist = false;

                SqlConnection myConnection = new SqlConnection(connection);
                myConnection.Open();

                string query = "SELECT TOP 1 ArtistKey FROM Artist WHERE ArtistKey = @ArtistKey";
                SqlCommand myCommand = new SqlCommand(query, myConnection);

                myCommand.Parameters.AddWithValue("@ArtistKey", artistKey);

                SqlDataReader dr = myCommand.ExecuteReader();
                while (dr.Read())
                {
                    if (dr != null)
                    {
                        existingArtist = true;
                    }
                    else
                    {
                        existingArtist = false;
                    }
                }

                myConnection.Close();
                return existingArtist;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string getCurrentKeyFromArtistDA(string connection, string artistName)
        {
            try
            {
                string artistKey = string.Empty;

                SqlConnection myConnection = new SqlConnection(connection);
                myConnection.Open();

                string query = "SELECT TOP 1 ArtistKey FROM Artist WHERE ArtistName = @ArtistName";
                SqlCommand myCommand = new SqlCommand(query, myConnection);

                myCommand.Parameters.AddWithValue("@ArtistName", artistName);

                SqlDataReader dr = myCommand.ExecuteReader();
                while (dr.Read())
                {
                    artistKey = dr["ArtistKey"].ToString();
                }

                myConnection.Close();
                return artistKey;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int insertArtistDA(string connection, string artistKey, string artistName)
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(connection);
                myConnection.Open();

                string query = "INSERT INTO Artist (ArtistKey, ArtistName, ArtistInsertDateTime, ArtistUpdateDateTime)";
                query += " VALUES (@ArtistKey, @ArtistName, GETDATE(), GETDATE())";

                SqlCommand myCommand = new SqlCommand(query, myConnection);

                myCommand.Parameters.AddWithValue("@ArtistKey", artistKey);
                myCommand.Parameters.AddWithValue("@ArtistName", artistName);

                int recordInserted = myCommand.ExecuteNonQuery();

                myConnection.Close();
                return recordInserted;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Boolean checkForExistingArtistFileDA(string connection, string artistPath, string artistFile)
        {
            try
            {
                Boolean existingArtistFile = false;

                SqlConnection myConnection = new SqlConnection(connection);
                myConnection.Open();

                string query = "SELECT TOP 1 ArtistFile FROM ArtistFile WHERE ArtistPath = @ArtistPath AND ArtistFile = @ArtistFile";
                SqlCommand myCommand = new SqlCommand(query, myConnection);

                myCommand.Parameters.AddWithValue("@ArtistPath", artistPath);
                myCommand.Parameters.AddWithValue("@ArtistFile", artistFile);

                SqlDataReader dr = myCommand.ExecuteReader();
                while (dr.Read())
                {
                    if (dr != null)
                    {
                        existingArtistFile = true;
                    }
                    else
                    {
                        existingArtistFile = false;
                    }
                }

                myConnection.Close();
                return existingArtistFile;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int insertArtistFileDA(string connection, string artistKey, string artistItemKey, string artistPath, string artistFile, DateTime artistFileCreated, DateTime artistFileModified, decimal artistFileSize, string artistFileType, int artistFileBitRate)
        {
            try
            {
                string query = "INSERT INTO ArtistFile (ArtistKey, ArtistItemKey, ArtistPath, ArtistFile, ArtistFileCreated, ArtistFileModified, ArtistFileSize, ArtistFileType, ArtistFileBitRate)";
                query += " VALUES (@ArtistKey, @ArtistItemKey, @ArtistPath, @ArtistFile, @ArtistFileCreated, @ArtistFileModified, @ArtistFileSize, @ArtistFileType, @ArtistFileBitRate)";

                SqlConnection myConnection = new SqlConnection(connection);
                myConnection.Open();

                SqlCommand myCommand = new SqlCommand(query, myConnection);

                myCommand.Parameters.AddWithValue("@ArtistKey", artistKey);
                myCommand.Parameters.AddWithValue("@ArtistItemKey", artistItemKey);
                myCommand.Parameters.AddWithValue("@ArtistPath", artistPath);
                myCommand.Parameters.AddWithValue("@ArtistFile", artistFile);
                myCommand.Parameters.AddWithValue("@ArtistFileCreated", artistFileCreated);
                myCommand.Parameters.AddWithValue("@ArtistFileModified", artistFileModified);
                myCommand.Parameters.AddWithValue("@ArtistFileSize", artistFileSize);
                myCommand.Parameters.AddWithValue("@ArtistFileType", artistFileType);
                myCommand.Parameters.AddWithValue("@ArtistFileBitRate", artistFileBitRate);

                int recordInserted = myCommand.ExecuteNonQuery();

                myConnection.Close();
                return recordInserted;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Boolean checkForExistingArtistSongDA(string connection, string artistTitle, string artistAlbum)
        {
            try
            {
                Boolean existingArtistSong = false;

                SqlConnection myConnection = new SqlConnection(connection);
                myConnection.Open();

                string query = "SELECT TOP 1 ArtistTitle FROM ArtistSong WHERE ArtistTitle = @ArtistTitle AND ArtistAlbum = @ArtistAlbum";
                SqlCommand myCommand = new SqlCommand(query, myConnection);

                myCommand.Parameters.AddWithValue("@ArtistTitle", artistTitle);
                myCommand.Parameters.AddWithValue("@ArtistAlbum", artistAlbum);

                SqlDataReader dr = myCommand.ExecuteReader();
                while (dr.Read())
                {
                    if (dr != null)
                    {
                        existingArtistSong = true;
                    }
                    else
                    {
                        existingArtistSong = false;
                    }
                }

                myConnection.Close();
                return existingArtistSong;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int insertArtistSongDA(string connection, string artistKey, string artistItemKey, string artistTitle, string artistAlbum, string artistGenre, int artistYear, int artistTrackNumber, TimeSpan artistDuration)
        {
            try
            {
                string query = "INSERT INTO ArtistSong (ArtistKey, ArtistItemKey, ArtistTitle, ArtistAlbum, ArtistGenre, ArtistYear, ArtistTrackNumber, ArtistDuration, ArtistCreated, ArtistUpdated)";
                query += " VALUES (@ArtistKey, @ArtistItemKey, @ArtistTitle, @ArtistAlbum, @ArtistGenre, @ArtistYear, @ArtistTrackNumber, @ArtistDuration, GETDATE(), GETDATE())";

                SqlConnection myConnection = new SqlConnection(connection);
                myConnection.Open();

                SqlCommand myCommand = new SqlCommand(query, myConnection);

                myCommand.Parameters.AddWithValue("@ArtistKey", artistKey);
                myCommand.Parameters.AddWithValue("@ArtistItemKey", artistItemKey);
                myCommand.Parameters.AddWithValue("@ArtistTitle", artistTitle);
                myCommand.Parameters.AddWithValue("@ArtistAlbum", artistAlbum);
                myCommand.Parameters.AddWithValue("@ArtistGenre", artistGenre);
                myCommand.Parameters.AddWithValue("@ArtistYear", artistYear);
                myCommand.Parameters.AddWithValue("@ArtistTrackNumber", artistTrackNumber);
                myCommand.Parameters.AddWithValue("@ArtistDuration", artistDuration);

                int recordInserted = myCommand.ExecuteNonQuery();

                myConnection.Close();
                return recordInserted;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}

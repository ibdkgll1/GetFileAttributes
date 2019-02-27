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

        public Boolean checkForExistingArtistDA(string connection, string artistName)
        {
            try
            {
                Boolean existingArtist = false;

                SqlConnection myConnection = new SqlConnection(connection);
                myConnection.Open();

                string query = "SELECT TOP 1 ArtistName FROM Artist WHERE ArtistName = @ArtistName";
                SqlCommand myCommand = new SqlCommand(query, myConnection);

                myCommand.Parameters.AddWithValue("@ArtistName", artistName);

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

        public int getCurrentKeyFromArtistDA(string connection)
        {
            try
            {
                int nextCurrentFromArtist = 0;

                SqlConnection myConnection = new SqlConnection(connection);
                myConnection.Open();

                string query = "SELECT TOP 1 ArtistId FROM Artist ORDER BY ArtistId DESC";
                SqlCommand myCommand = new SqlCommand(query, myConnection);

                SqlDataReader dr = myCommand.ExecuteReader();
                while (dr.Read())
                {
                    nextCurrentFromArtist = Convert.ToInt32(dr["ArtistId"]);
                }

                myConnection.Close();
                return nextCurrentFromArtist;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int insertArtistDA(string connection, int artistId, string artistName)
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(connection);
                myConnection.Open();

                string query = "INSERT INTO Artist (ArtistId, ArtistName)";
                query += " VALUES (@ArtistId, @ArtistName)";

                SqlCommand myCommand = new SqlCommand(query, myConnection);

                myCommand.Parameters.AddWithValue("@ArtistId", artistId);
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

        public Boolean checkForExistingArtistFileDA(string connection, string artistFile)
        {
            try
            {
                Boolean existingArtistFile = false;

                SqlConnection myConnection = new SqlConnection(connection);
                myConnection.Open();

                string query = "SELECT TOP 1 ArtistFile FROM ArtistFile WHERE ArtistFile = @ArtistFile";
                SqlCommand myCommand = new SqlCommand(query, myConnection);

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

        public int insertArtistFileDA(string connection, int artistId, string artistPath, string artistFile, DateTime artistFileCreated, DateTime artistFileModified, decimal artistFileSize, string artistFileType, int artistFileBitRate)
        {
            try
            {
                string query = "INSERT INTO ArtistFile (ArtistId, ArtistPath, ArtistFile, ArtistFileCreated, ArtistFileModified, ArtistFileSize, ArtistFileType, ArtistFileBitRate)";
                query += " VALUES (@ArtistId, @ArtistPath, @ArtistFile, @ArtistFileCreated, @ArtistFileModified, @ArtistFileSize, @ArtistFileType, @ArtistFileBitRate)";

                SqlConnection myConnection = new SqlConnection(connection);
                myConnection.Open();

                SqlCommand myCommand = new SqlCommand(query, myConnection);

                myCommand.Parameters.AddWithValue("@ArtistId", artistId);
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

        public int insertArtistSongDA(string connection, int artistId, string artistTitle, string artistAlbum, string artistGenre, int artistYear, int artistTrackNumber, TimeSpan artistDuration)
        {
            try
            {
                string query = "INSERT INTO ArtistSong (ArtistId, ArtistTitle, ArtistAlbum, ArtistGenre, ArtistYear, ArtistTrackNumber, ArtistDuration)";
                query += " VALUES (@ArtistId, @ArtistTitle, @ArtistAlbum, @ArtistGenre, @ArtistYear, @ArtistTrackNumber, @ArtistDuration)";

                SqlConnection myConnection = new SqlConnection(connection);
                myConnection.Open();

                SqlCommand myCommand = new SqlCommand(query, myConnection);

                myCommand.Parameters.AddWithValue("@ArtistId", artistId);
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

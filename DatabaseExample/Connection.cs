using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.CodeDom;

/// <summary>
/// Summary description for Connection
/// </summary>
public class Connection
{
    
    static string path = 
    @"Data Source=(LocalDB)\MSSQLLocalDB;Integrated Security=True;"+
    @"AttachDbFilename=D:\Developing\ProjectYud\App_Data\database.mdf";
    
    public static void NonQueryCommand(string command)
    {
        SqlConnection connection = new SqlConnection(path);
        SqlCommand commnd = new SqlCommand(command, connection);
        commnd.ExecuteNonQuery();
    }
    
    /// <summary>
    /// working method, retrieves as needed
    /// </summary>
    /// <param name="passedId"></param>
    /// <returns> the user with the specified id</returns>
    public static UserInfo RetriveUserData(int passedId)
    {
        SqlConnection connection = new SqlConnection(path);
        int id = -1;
        string username = "", fullname = "", email = "";
        DateTime time = new DateTime();
        SqlCommand command = new SqlCommand(
            "SELECT TOP 1 * FROM users WHERE id=@userid;",
            connection);
        command.Parameters.AddWithValue(@"userid", passedId);
        using (connection)
        {
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    id = (int)reader["Id"];
                    username = (string)reader["username"];
                    fullname = reader["fullname"].GetType()==typeof(DBNull)? "":(string)reader["fullname"];
                    time = reader["birthdate"].GetType() == typeof(DBNull) ? new DateTime() : Convert.ToDateTime(reader["birthdate"].ToString());
                    email = (string)reader["email"];
                }
                connection.Close();
                if (fullname.Length == 0)
                {
                    if(time == new DateTime(0))
                    {
                        return new UserInfo(id, username, email);
                    }
                    else
                    {
                        return new UserInfo(id, username, time, email);
                    }
                }
                if (time == new DateTime(0))
                {
                    return new UserInfo(id, username, fullname, email); ;
                }
                else
                {
                    return new UserInfo(id, username, fullname, time, email);
                }
                
            }
            else
            {
                connection.Close();
                
                return new UserInfo();
            }

            
        }
    }

    /// <summary>
    /// unchecked
    /// </summary>
    /// <param name="command"></param>
    /// <returns>the comments via the specified sql query</returns>
    public static DataTable RetrieveComments(string command)
    {

        SqlConnection connection = new SqlConnection(path);
        SqlCommand cmd = connection.CreateCommand();
        cmd.CommandText = command;
        DataTable table = new DataTable();
        DataColumn column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "id";
        table.Columns.Add(column);
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "Text";
        table.Columns.Add (column);
        column.ColumnName = "AuthorID";
        table.Columns.Add(column);
        column.DataType = System.Type.GetType("System.DateTime");
        column.ColumnName = "Time";
        table.Columns.Add (column);
        using (connection)
        {
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while(reader.Read())
                {
                    DataRow row = table.NewRow();
                    row["id"]=reader["Id"];
                    row["Text"] = reader["CommentText"];
                    row["AuthorID"] = reader["AuthorID"];
                    row["PostID"] = reader["PostID"];
                    row["Time"] = reader["Time"];
                }
            }

            connection.Close();
        }
        return table;
    }
    
    
    /// <summary>
    /// unchecked
    /// </summary>
    /// <param name="RequesterId"></param>
    /// <param name="TrueIfPost"></param>
    /// <returns>all the commends of a user OR all the comments under a song</returns>
    public static DataTable RetrieveComments(int RequesterId, bool TrueIfPost)
    {
        
        SqlConnection connection = new SqlConnection(path);
        SqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Comments WHERE @type = @id";
        command.Parameters.AddWithValue(
            @"type",
            TrueIfPost ? "PostId":"AuthorID"
            );
        command.Parameters.AddWithValue(@"id", RequesterId);
        DataTable table = new DataTable();
        DataColumn column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "id";
        table.Columns.Add(column);
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "Text";
        table.Columns.Add(column);
        column.ColumnName = "AuthorID";
        table.Columns.Add(column);
        column.DataType = System.Type.GetType("System.DateTime");
        column.ColumnName = "Time";
        table.Columns.Add(column);
        using (connection)
        {
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    DataRow row = table.NewRow();
                    row["id"] = reader["Id"];
                    row["Text"] = reader["CommentText"];
                    row["AuthorID"] = reader["AuthorID"];
                    row["PostID"] = reader["PostID"];
                    row["Time"] = reader["Time"];
                }
            }

            connection.Close();
        }
        return table;
    }
    
    /// <summary>
    ///working method, works as needed 
    /// </summary>
    /// <param name="id"></param>
    /// <returns>the Song with the specified id</returns>
    public static SongData RetrieveSongData(int id)
    {
        string code = "";
        DateTime datee = new DateTime();
        SqlConnection connection = new SqlConnection(path);
        SqlCommand command = connection.CreateCommand();
        command.CommandText =
            @"SELECT TOP 1 Date,Code FROM SONGS
            WHERE
            Id=@SongID;
            ";
        command.Parameters.AddWithValue(@"SongID", id.ToString());
        connection.Open();
        SqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                code = reader["Code"].ToString();
                datee = DateTime.Parse(reader["Date"].ToString());
            }
        }
        connection.Close();
        return new SongData(id,code,datee);
    }

    /// <summary>
    /// working method, works as needed
    /// </summary>
    /// <returns>the last song in the DataBase</returns>
    public static SongData RetrieveLastSong()
    {
        int id = 0;
        string code = "";
        DateTime datee = new DateTime();
        SqlConnection connection = new SqlConnection(path);
        SqlCommand command = connection.CreateCommand();
        command.CommandText =
            @"SELECT TOP 1 * FROM SONGS
            ORDER BY
            Date desc;
            ";
        connection.Open();
        SqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                id = int.Parse(reader["id"].ToString());
                code = reader["Code"].ToString();
                datee = DateTime.Parse(reader["Date"].ToString());
            }
        }
        connection.Close();
        return new SongData(id, code, datee);
    }

    /// <summary>
    /// unchecked. Should upload Song Data to a server
    /// </summary>
    /// <param name="link"></param>
    /// <param name="date"></param>
    /// <returns>the number of Songs uploaded(should be 1 if worked, 0 if didn't)</returns>
    public static int InputSongData(string link, DateTime date)
    {

        SqlConnection connection = new SqlConnection(path);
        SqlCommand command = connection.CreateCommand();
        command.CommandText =
            @"INSERT INTO Songs
            (Date,Link)
            VALUES
            (@songDate,@SongLink)
            ";
        command.Parameters.AddWithValue(@"songDate", date.ToString());
        command.Parameters.AddWithValue(@"SongLink", link);
        connection.Open();
        int code = command.ExecuteNonQuery();
        connection.Close();
        return code;

    }
}
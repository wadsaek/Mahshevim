using RedEye.SqlCon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RedEye.main.Admin
{
    public partial class AdminAddGame : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Permission"] == null || int.Parse(Session["Permission"].ToString()) < 2)
            {
                Response.Redirect("~/main/index.aspx");
            }

        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            if (Request.Form["GameName"].Trim() == "" || Request.Form["GameDescription"].Trim() == "")
            {
                InformLabel.Text = "Empty Name or Description";
            }
            else
            {
                string GameName = Request.Form["GameName"];
                SqlConnection con = new SqlConnection(Sqlcon.connectionstring);
                SqlCommand cmd = con.CreateCommand();
                cmd.Parameters.Clear();
                cmd.CommandText = "SELECT * FROM Games WHERE GameName = @GameName";
                cmd.Parameters.AddWithValue("@GameName", GameName);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataTable dt = new DataTable();
                adapter.Fill(dt);


                if (dt.Rows.Count != 0)
                {
                    InformLabel.Text = "Game with the same name already exists";
                }
                else
                {
                    string GameDescription = Request.Form["GameDescription"];
                    int GamePrice = int.Parse(Request.Form["GamePrice"]);

                    cmd.CommandText = "INSERT INTO Games (GameName, GameDescription, GamePrice, FileSource, GameImage) VALUES(@GameName, @GameDescription, @GamePrice, '_', '_')";

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue(@"GameName", GameName);
                    cmd.Parameters.AddWithValue(@"GameDescription", GameDescription);
                    cmd.Parameters.AddWithValue(@"GamePrice", GamePrice);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    string RootDirectory = Server.MapPath("~/Games/");
                    string path = RootDirectory + GameName + "/Image/";
                    Directory.CreateDirectory(path);
                    string strFileName = "Image.jpg";
                    strFileName = Path.GetFileName(strFileName);
                    string strGameFilePath = path + strFileName;
                    GameFile.PostedFile.SaveAs(strGameFilePath);

                    path = RootDirectory + GameName + "/GameFile/";
                    Directory.CreateDirectory(path);
                    string strImageFileName = GameImage.PostedFile.FileName;
                    strImageFileName = Path.GetFileName(strImageFileName);
                    string strImageFilePath = path + strImageFileName;
                    GameImage.PostedFile.SaveAs(strImageFilePath);

                    //string ImagePathForWeb = "/Games/" + "/" + GameName + "/" + strImageFileName;
                    //string GameFileForWeb = "/Games/" + "/" + GameName + "/" + strFileName;

                    //cmd.CommandText = "UPDATE Games SET FileSource = @FileSource, GameImage = @GameImage WHERE GameName = @GameName";
                    //cmd.Parameters.AddWithValue(@"FileSource", GameFileForWeb);
                    //cmd.Parameters.AddWithValue(@"GameImage", ImagePathForWeb);
                    //con.Open();
                    //cmd.ExecuteNonQuery();
                    //con.Close();

                    InformLabel.Text = "Game Appended Successfully";
                };
            }
            LabelDiv.Visible = true;
        }
    }
}
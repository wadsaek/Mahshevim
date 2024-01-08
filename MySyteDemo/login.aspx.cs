using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login : System.Web.UI.Page
{

    //string path = 
    static SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\yoky\mySite\App_Data\myDb.mdf;");
    static SqlCommand cmd = new SqlCommand("", connection);
    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Count > 0)
        {

            cmd.CommandText = String.Format("select * from users where userName='{0}' and password='{1}';",
                Request.Form["userName"].ToString(), Request.Form["password"].ToString());
            
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                Response.Write("Hello" + dataTable.Rows[0]["firstName"] + " " + dataTable.Rows[0]["lastName"]);
            }
            else
            {
                Response.Write("Wrong Details");
            }
        }
    }
}
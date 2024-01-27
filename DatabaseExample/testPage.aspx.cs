using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class aspx_Default : System.Web.UI.Page
{
    public UserInfo myUser;
    public SongData mySong;
    protected void Page_Load(object sender, EventArgs e)
    {
        myUser = Connection.RetriveUserData(0);
        mySong = Connection.RetrieveSongData(0);
    }
}
string RootDirectory = Server.MapPath("~/Games/"); //your directory (have to use mappath)
string path = RootDirectory + GameName + '\\'; //in my situation string GameName = Request.Form["GameName"]
Directory.CreateDirectory(path);
string strFileName = GameFile.PostedFile.FileName; //in my situation GameFile is id of input with type=file
strFileName = Path.GetFileName(strFileName); 
string strGameFilePath = path + strFileName;
GameFile.PostedFile.SaveAs(strGameFilePath);

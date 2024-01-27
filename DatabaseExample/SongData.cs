using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SongData
/// </summary>
public class SongData
{
    public static string SpotifyIdCutOut(string link)
    {
        int pFrom = link.IndexOf("/track/") + "/track/".Length;
        int pTo = link.LastIndexOf("&si");

        return link.Substring(pFrom, pTo - pFrom);
    }
    public bool validity { get; }
    public int SongId { get;}
    public string SpotifyCode { get;}
    public DateTime SongDate { get;}
    public SongData(int songId, string spotifyId, DateTime songDate)
    {
        SongId = songId;
        SpotifyCode = spotifyId;
        SongDate = songDate;
        validity = spotifyId != "" ? true : false;
    }
}
<%@ Page Title="" Language="C#" MasterPageFile="~/masters/MasterPage.master" AutoEventWireup="true" CodeFile="testPage.aspx.cs" Inherits="aspx_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <details>
        <summary>
            <%=myUser.UserName%>
        </summary>
        <ul>
            <li>
                <%=myUser.Id %>
            </li>
            <li>
                <%=myUser.FullName %>
            </li>
            <li>
                <%=myUser.Birth %>
            </li>
            <li>
                <%=myUser.Email %>
            </li>
        </ul>
    </details>
    <div>
        validity: <%=mySong.validity %> <br />
        date: <%=mySong.SongDate%> <br />
        id: <%=mySong.SongId %> <br />
        SpotifyCode: <%=mySong.SpotifyCode %>
    </div>
</asp:Content>


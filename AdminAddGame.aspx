<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminAddGame.aspx.cs" Inherits="RedEye.main.Admin.AdminAddGame" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="flexcontainer">
            <div style="height: 1200px; width: 100%; background-image: url('/media/background_add_2.png'); background-repeat: no-repeat; background-color: black; background-position-x: center">
                <div style="display: flex; width: 100%; justify-content: center; flex-direction: column; align-items: center; margin-top: 50px">
                    <div style="display: flex; width: 690px; padding-left: 40px">
                        <div style="font-size: 32px; color: white">
                            ADD NEW GAME
                        </div>
                    </div>
                    <div style="display: flex; flex-direction: column; width: 650px; height: 700px; background-color: #282828; margin-top: 10px; border-radius: 20px; padding: 25px 20px; justify-content: space-between">
                        <div style="display: flex; justify-content: space-between; flex-direction: column;">
                            <div style="margin-bottom: 20px">
                                <div style="font-size: 24px; color: white">
                                    Game Name*
                                </div>
                                <input type="text" size="50" style="padding: 5px; outline: none; font-size: 20px" id="GameName", name="GameName", required="required"/>
                            </div>
                            <div style="margin-bottom: 20px">
                                <div style="font-size: 24px; color: white">
                                    Game Description*
                                </div>
                                <textarea cols="53" rows="10" maxlength="500" style="padding: 5px; outline: none; font-size: 20px; resize: none; font-family:Arial" id="GameDescription" name="GameDescription" required="required"></textarea>
                            </div>
                            <div style="margin-bottom: 20px">
                                <div style="font-size: 24px; color: white" >
                                    Game Image*
                                </div>
                                <input type="file" runat="server" accept="image/*" id="GameImage" name="GameImage" required="required" style="display: block; color: white; font-family: proxima-nova-condensed; font-size: 15px"/>                            </div>
                            <div style="margin-bottom: 20px">
                                <div style="font-size: 24px; color: white" >
                                    Game Price*
                                </div>
                                <input type="number" max="256" min="0" style="padding: 5px; outline: none; font-size: 20px" id="GamePrice", name="GamePrice", required="required"/>
                            </div>
                            <div>
                                <div style="font-size: 24px; color: white" >
                                    Game File*
                                </div>
                                <input type="file" runat="server" id="GameFile" name="GameFile" required="required" style="display: block; color: white; font-family: proxima-nova-condensed; font-size: 15px"/>
                            </div>
                        </div>
                        <div style="display: flex; justify-content: center">
                            <asp:Button runat="server" ID="Submit" OnClick="Submit_Click" class="LoginButton" Text="ADD"/>
                        </div>
                        <div runat="server" visible="false" id="LabelDiv" style="display: flex; color: white; justify-content: center">
                            <asp:Label runat="server" ID="InformLabel"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>

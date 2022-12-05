<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1_PictureCode_Check.aspx.cs" Inherits="WebApplication2019_Captcha.WebForm.WebForm1_PictureCode_Check" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

    <script type="text/javascript">   
        function reloadcode() {   
            document.getElementById("Image1").src = "ValidatePicture.ashx?mis2000lab=" + Math.random();
            return false;
        }
    </script>

    <script type="text/javascript" src="/Scripts/jquery-3.3.1.js"></script>
    <script type="text/javascript">   
        //// 用滑鼠點擊圖片，就會更新圖片。需搭配上面引用jQuery
        //$(document).ready(function () {
        //    $("#Image1").bind("click", function () {
        //        this.src = "ValidatePicture.ashx?mis2000lab=" + Math.random();
        //        // 點擊圖片就會自動更換。因為產生圖片的.ashx檔不需要傳入數值就能產生圖片。
        //        // 搭配這一段JavaScript卻需要輸入才能重新產生圖片，所以每次點擊 onclick都會傳入一個無意義又會變化的值。
        //    });
        //});
    </script>

</head>
<body>
    <h3>搭配 /WebForm/ValidatePicture.ashx</h3>
    <form id="form1" runat="server">
        <div>
            <br />
            帳號：<asp:TextBox ID="TextBox1Account" runat="server"></asp:TextBox><br />
            密碼：<asp:TextBox ID="TextBox2Passwd" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <br />
            請輸入圖片驗證碼：<asp:TextBox ID="TextBox3" runat="server" Width="50"></asp:TextBox>

                         <asp:Image ID="Image1" runat="server" ImageUrl="ValidatePicture.ashx" 
                                               onclick="javasrcipt: this.src='/WebForm/ValidatePicture.ashx?mis2000lab=' + Math.random()" /><br />
                         （點擊圖片就會自動更換。因為產生圖片的.ashx檔不需要傳入數值就能產生圖片。搭配這一段JavaScript卻需要輸入才能重新產生圖片，所以每次點擊 onclick都會傳入一個無意義又會變化的值。）
                         <br />
                         <asp:LinkButton ID="LinkButton1" runat="server" Text="重新整理(1)，更換圖片" 
                                                        OnClientClick="return reloadcode()"/>

                        <button type="button" id="LinkButton2" onclick="reloadcode()">重新整理(2)，更換圖片</button>
                        <!--  如果沒有加上 type="button"，會被當成 Submit導致畫面PostBack。 -->
                        <!--  如果沒有加上 type="button"，後面要寫成 onclick="return reloadcode()"。 -->

            <br /><br />
            <asp:Button ID="Button1" runat="server" Text="Button_Login" OnClick="Button1_Click" />

        </div>
        <asp:Label ID="Label1" runat="server" Font-Size="Large"></asp:Label>
    </form>
</body>
</html>

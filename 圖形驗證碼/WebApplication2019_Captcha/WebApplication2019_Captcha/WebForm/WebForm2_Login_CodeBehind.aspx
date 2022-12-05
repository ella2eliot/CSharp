<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2_Login_CodeBehind.aspx.cs" Inherits="WebApplication2019_Captcha.WebForm.WebForm2_Login_CodeBehind" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
  <!-- 成功版本，需搭配<button>  -->
 <script src="https://www.google.com/recaptcha/api.js"></script>
 <script>
     function onSubmit(token) {
         // https://developers.google.com/recaptcha/docs/v3   [Automatically bind the challenge to a button]
         document.getElementById("g-recaptcha-CodeBehind").innerText = token;  // 驗證成功後執行
         window.alert(token);
         document.getElementById("form1").submit();
     }
 </script> 

</head>
<body>
    <form id="form1" runat="server">
        Login Name&nbsp;   <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        <br />
        reCAPTCHA<br />
        <!-- ******************************************************************** -->
        <!--  自己設定隱藏欄位（Hidden）。 -->
       <input type="hidden" id="g-recaptcha-CodeBehind" name="g-recaptcha-CodeBehind" />
        <!-- ******************************************************************** -->


        <br /> <br />
        <asp:Button runat="server" ID="Button1" OnClick="Button1_Click" 
                        class="g-recaptcha" 
                        data-sitekey="6LfM_aYZAAAAABeZzIs-4T16ujpy9nKw9OIMcods"
                        data-callback='onSubmit' 
                        data-action='submit'
            Text="WebForm_Button_Login_登入"  />

        <!--    移除 class="g-recaptcha" ，可以執行後置程式碼 Button1_Click"。但無法取得token。
                   移除 data-callback='onSubmit'  ，可以執行後置程式碼 Button1_Click"。但無法取得token。

                   移除 data-callback='onSubmit'  ，無法執行後置程式碼 Button1_Click"。無法取得token。
                   改成 OnClientClick='onSubmit'   ，無法執行後置程式碼 Button1_Click"。無法取得token。
            -->
        <br /> <br />
        <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1_Login_HTML.aspx.cs" Inherits="WebApplication2019_Captcha.WebForm.WebForm1_Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

<%--    <script type="text/javascript">
        // 驗證成功後執行
        var onSubmit = function(token) {
            $('#g-recaptcha-CodeBehind').val() = token;
            //window.alert("Submit");
        };
        var onloadCallback = function() {
            grecaptcha.render('reCAPTCHA', {
                'sitekey': '6LcKxKYZAAAAABoEAZiG0TQm_aBI5iIKVzvHOGI2', // 填寫自己的site_key (Client端)
                'callback': onSubmit    // 執行成功後  onSubmit變數接收
            });
        };
    </script>--%>


<%--<script src="https://www.google.com/recaptcha/api.js?render=6LfM_aYZAAAAABeZzIs-4T16ujpy9nKw9OIMcods"></script>
   <script>
       function onSubmit(e) {
           e.preventDefault();
           grecaptcha.ready(function () {
               grecaptcha.execute('6LfM_aYZAAAAABeZzIs-4T16ujpy9nKw9OIMcods', { action: 'submit' }).then(function (token) {
                   // Add your logic to submit to your backend server here.
                   document.getElementById("form1").submit();
               });
           });
       }
  </script>--%>

  <!-- 成功版本，需搭配<button>  -->
 <script src="https://www.google.com/recaptcha/api.js"></script>
 <script>
     function onSubmit(token) {
         // https://developers.google.com/recaptcha/docs/v3   [Automatically bind the challenge to a button]
         //document.getElementById("g-recaptcha-CodeBehind").val() = token;  // 驗證成功後執行
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
        <!--  畫面上不要自己設定隱藏欄位（Hidden）。Google reCAPTCHA會自動建立一個隱藏欄位！ -->
        <!-- ******************************************************************** -->
        
        <br /> <br />

        <button class="g-recaptcha" 
                        data-sitekey="6LfM_aYZAAAAABeZzIs-4T16ujpy9nKw9OIMcods"
                        data-callback='onSubmit' 
                        data-action='submit'>[Button] Google-Submit，按下按鈕後，請稍等五秒鐘才會看見結果。</button>

        <br /> <br />
        <asp:Label ID="Label2" runat="server" ForeColor="Blue"></asp:Label>
    </form>
</body>
</html>

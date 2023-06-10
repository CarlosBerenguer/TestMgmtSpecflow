using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestJamaTestMgmt.Config;

namespace TestJamaTestMgmt.Test_Management.Login
{
    public class LoginScreen : PageObjectModel
    {
        public LoginScreen(SeleniumHelper helper) : base(helper) {}

        public void FillLoginForm(User usuario)
        {
            Helper.FillTextBoxById("j_username", usuario.Username);
            Helper.FillTextBoxById("j_password", usuario.Password);
        }

        public bool ValidateFillLoginForm(User usuario)
        {
            if (Helper.GetTextBoxValueById("j_username") != usuario.Username) return false;
            if (Helper.GetTextBoxValueById("j_password") != usuario.Password) return false;
            
            return true;
        }

        public bool ValidateSuccessLogin()
        {
            if (!Helper.ValidateUrlContent(Helper.Configuration.MainUrl)) return false;
            
            return true;
        }

        public bool ValidateLoginErrorMessage(string message)
        {
            return Helper.GetElementTextByClass("j-login-error").Contains(message);
        }

        public void ClickOnLoginButton()
        {
            Helper.ClickButtonById("loginButton");
        }

        public bool Login(User user)
        {
            AccessSystem();

            FillLoginForm(user);

            if(!ValidateFillLoginForm(user)) return false;

            ClickOnLoginButton();

            if (!ValidateSuccessLogin()) return false;

            return true;    
            

        }
    }
}

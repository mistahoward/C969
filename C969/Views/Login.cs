using C969.Controllers;
using C969.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C969
{
    public partial class LoginForm : Form
    {
        private readonly UserController _userController;
        readonly Dictionary<string, Dictionary<string, string>> translations = new Dictionary<string, Dictionary<string, string>>
        {
            { "en-US", new Dictionary<string, string> {
                {"login", "Login"},
                {"username", "Username"},
                {"password", "Password"},
                {"username_error", "Username cannot be empty."},
                {"password_error", "Password cannot be empty." },
                {"error", "The username and password did not match."}
            }},
            { "fr-FR", new Dictionary<string, string> {
                {"login", "S'identifier"},
                {"username", "Nom d'utilisateur"},
                {"password", "Mot de passe"},
                {"username_error", "Le nom d'utilisateur ne peut pas être vide." },
                {"password_error", "Le mot de passe ne peut pas être vide." },
                {"error", "Le nom d'utilisateur et le mot de passe ne correspondent pas."}
            }}
        };
        public LoginForm()
        {
            InitializeComponent();

            _userController = new UserController();
        }
        // Helper function for getting language (locale code)
        private string GetLanguage()
        {
            string localeCode = System.Globalization.CultureInfo.CurrentUICulture.IetfLanguageTag;

            if (translations.ContainsKey(localeCode))
            {
                return localeCode;
            }
            else
            {
                // Default to English if the system language isn't one of the hardcoded options.
                return "en-US";
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // grab selected language
            string selectedLanguage = this.GetLanguage();

            // choose selected language translations
            UsernameLabel.Text = translations[selectedLanguage]["username"];
            PasswordLabel.Text = translations[selectedLanguage]["password"];
        }
        private void LoginButton_Click(object sender, EventArgs e)
        {
            // clear any existing errors first and foremost
            errorProvider.Clear();

            string selectedLanguage = this.GetLanguage();
            // begin error checking, check if fields are empty or whitespace
            if(string.IsNullOrWhiteSpace(UsernameTextBox.Text))
            {
                errorProvider.SetError(UsernameTextBox, translations[selectedLanguage]["username_error"]);
                return;
            }
            if (string.IsNullOrWhiteSpace(PasswordTextBox.Text)) {
                errorProvider.SetError(PasswordTextBox, translations[selectedLanguage]["password_error"]);
                return;
            }
            var userName = UsernameTextBox.Text;
            var password = PasswordTextBox.Text;
            bool loginResult = _userController.Login(userName, password);
            if (!loginResult)
            {
                errorProvider.SetError(PasswordTextBox, translations[selectedLanguage]["error"]);
                return;
            }
            var calendarForm = new Calendar(DateTime.Now, ViewType.Week);
            this.Hide();
            calendarForm.ShowDialog();
            this.Close();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}

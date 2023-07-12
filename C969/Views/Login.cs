using C969.Controllers;
using C969.Models;
using C969.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C969
{
    public partial class LoginForm : Form
    {
        private readonly UserController _userController;
        private readonly AppointmentController _appointmentController;
        private List<Appointment> _usersAppointments;
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
            _appointmentController = new AppointmentController();
        }
        /// <summary>
        /// Utility function for getting local user's language
        /// </summary>
        /// <returns>Language string (locale code)</returns>
        private string GetLanguage()
        {
            string localeCode = CultureInfo.CurrentCulture.ToString();

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
        /// <summary>
        /// Loading method for login form. Grab user's langauge and adjust labels accordingly.
        /// </summary>
        private void LoginForm_Load(object sender, EventArgs e)
        {
            // grab selected language
            string selectedLanguage = this.GetLanguage();

            // choose selected language translations
            UsernameLabel.Text = translations[selectedLanguage]["username"];
            PasswordLabel.Text = translations[selectedLanguage]["password"];
        }

        /// <summary>
        /// Handles user login attempt and displays any relevant errors
        /// </summary>
        /// <param name="selectedLanguage">The language selected for the user. Expects language code in IETF format</param>
            private void HandleLogin(string selectedLanguage)
            {
                var userName = UsernameTextBox.Text;
                var password = PasswordTextBox.Text;
                User loginResult = _userController.Login(userName, password);
                if (loginResult == null)
                {
                    errorProvider.SetError(PasswordTextBox, translations[selectedLanguage]["error"]);
                    return;
                }
                ApplicationState.CurrentUser = loginResult;
                _usersAppointments = _appointmentController.GetUserAppointments(loginResult.userId);
                if (_usersAppointments.Any())
                {
                    // Find the closest appointment with start time at or after DateTime.Now
                    var approachingAppt = _usersAppointments
                        .Where(appt => appt.start >= DateTime.Now)
                        .OrderBy(appt => appt.start)
                        .FirstOrDefault();

                    if (approachingAppt != null && approachingAppt.start <= DateTime.Now.AddMinutes(15))
                    {
                        // Calculate the minutes left until the approaching appointment starts
                        int minutesLeft = (int)Math.Round((approachingAppt.start - DateTime.Now).TotalMinutes);
                        MessageBox.Show("You have an appointment in " + minutesLeft + " minute(s).", "Appointment Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                var calendarForm = new Calendar(DateTime.Now, CalendarViewType.Week, _appointmentController);
                this.Hide();
                calendarForm.ShowDialog();
                this.Close();
            }
        private void LoginButton_Click(object sender, EventArgs e)
        {
            // clear any existing errors first and foremost
            errorProvider.Clear();

            string selectedLanguage = GetLanguage();
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
            HandleLogin(selectedLanguage);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PasswordTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                HandleLogin(GetLanguage());
            }
        }
    }
}

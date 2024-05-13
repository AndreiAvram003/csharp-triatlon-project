using Model.Domain;
using Service.Service;

namespace GUI.GUI;

public partial class LoginForm : Form
{
    private IService service;
    public LoginForm(IService service)
    {
        InitializeComponent();
        this.service = service;
    }

    private void LoginButton_Click(object sender, EventArgs e)
    {
        string username = UsernameTextBox.Text;
        string password = PasswordTextBox.Text;

        MainForm mainForm = new MainForm();

        // Verificăm autentificarea utilizatorului
        Referee wantedRef = service.Login(username, password,mainForm);
        mainForm.setService(service);

        if (wantedRef != null)
        {
            // Autentificare reușită
            MessageBox.Show("Autentificare reușită!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // Deschide MainForm și trimite informațiile despre referee
            mainForm.setReferee(wantedRef);
            mainForm.InitializeComponent();
            mainForm.InitializeUI();
            mainForm.Show();

            // Ascunde formularul de login
            Hide();
        }
        else
        {
            // Autentificare eșuată
            MessageBox.Show("Nume de utilizator sau parolă incorecte!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void HandleCancelButtonClick(object sender, EventArgs e)
    {
        // Închide formularul de login
        Close();
    }
    
}

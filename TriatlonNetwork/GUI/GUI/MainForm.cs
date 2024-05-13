using Model.Domain;
using Service.Service;

namespace GUI.GUI;

public partial class MainForm : Form,IRefereeObserver
{
    private Referee referee;

    private IService service;

    public MainForm()
    {
    }

    public MainForm(Referee referee, IService service)
    {
        InitializeComponent();
        this.referee = referee;
        this.service = service;
        InitializeUI();
    }
    
    public void setService(IService service)
    {
        this.service = service;
    }
    
    public void InitializeUI()
    {
        // Aici poți actualiza interfața grafică pentru a reflecta informațiile despre referee
        // De exemplu, poți actualiza un label cu numele referee-ului sau alte informații relevante
        titleLabel.Text = "Welcome, " + referee.name + "!";

        // Configurare DataGridView
        dataGridViewParticipants.ColumnCount = 3;
        dataGridViewParticipants.Columns[0].Name = "ID";
        dataGridViewParticipants.Columns[1].Name = "Participant Name";
        dataGridViewParticipants.Columns[2].Name = "Points";

        // Încărcare date în DataGridView
        LoadParticipantsData();
    }

    private void LoadParticipantsData()
    {
        dataGridViewParticipants.Rows.Clear();
        // Obțineți lista de participanți și punctele lor din serviciu
        List<Participant> participants = service.GetParticipants(referee);

        // Adăugați fiecare participant și punctele sale în DataGridView
        foreach (Participant participant in participants)
        {
            dataGridViewParticipants.Rows.Add(participant.id,participant.name, participant.points);
        }
    }

    private void AddResultButton_Click(object sender, EventArgs e)
    {
        // Implementează funcționalitatea de adăugare de rezultate aici
        // Verificăm dacă există un rând selectat în DataGridView
            ResultForm resultForm = new ResultForm(service,referee.trial);
            resultForm.Show();
            
            
        
            // Deschidem ResultForm și trimitem informațiile despre participant și serviciu
            //ResultForm resultForm = new ResultForm(participantId, participantName, service);
            //resultForm.ShowDialog();
        }

    private void ViewReportButton_Click(object sender, EventArgs e)
    {
        // Implementează funcționalitatea de vizualizare a raportului aici
        ReportForm reportForm = new ReportForm(referee.trial,service);
        reportForm.Show();
    }

    private void LogoutButton_Click(object sender, EventArgs e)
    {
        // Implementează funcționalitatea de delogare aici
        this.Close();
    }

    public void Update()
    {
        // Implementează metoda Update pentru a actualiza interfața grafică
        // Aceasta va fi apelată de către serviciu atunci când se schimbă datele
        LoadParticipantsData();
    }

    public void setReferee(Referee wantedRef)
    {
        this.referee = wantedRef;
    }
}
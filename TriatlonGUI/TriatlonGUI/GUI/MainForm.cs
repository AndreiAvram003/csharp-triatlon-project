using TriatlonGUI.Domain;
using TriatlonGUI.Service;

namespace TriatlonGUI;

public partial class MainForm : Form
{
    private Referee referee;

    private IService service;
    public MainForm(Referee referee, IService service)
    {
        InitializeComponent();
        this.referee = referee;
        this.service = service;
        InitializeUI();
    }
    
    private void InitializeUI()
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
        // Obțineți lista de participanți și punctele lor din serviciu
        List<Participant> participants = service.GetParticipants();

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
        if (dataGridViewParticipants.SelectedRows.Count > 0)
        {
            // Obținem indicele rândului selectat
            int selectedRowIndex = dataGridViewParticipants.SelectedRows[0].Index;
        
            // Obținem informațiile despre participantul selectat din DataGridView
            string participantName = (string)dataGridViewParticipants.Rows[selectedRowIndex].Cells["Participant Name"].Value;
            int points = Convert.ToInt32(dataGridViewParticipants.Rows[selectedRowIndex].Cells["Points"].Value);
            long participantId = Convert.ToInt64(dataGridViewParticipants.Rows[selectedRowIndex].Cells["ID"].Value);
            Participant wantedParticipant = new Participant(participantId, participantName, points);
            ResultForm resultForm = new ResultForm(wantedParticipant,service,referee.trial);
            resultForm.Show();
            
            
        
            // Deschidem ResultForm și trimitem informațiile despre participant și serviciu
            //ResultForm resultForm = new ResultForm(participantId, participantName, service);
            //resultForm.ShowDialog();
        }
        else
        {
            MessageBox.Show("Select a participant first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
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
}
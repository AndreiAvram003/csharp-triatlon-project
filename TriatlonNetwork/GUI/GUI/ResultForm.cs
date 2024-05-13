using Model.Domain;
using Service.Service;

namespace GUI.GUI;

public partial class ResultForm : Form
{
    private readonly Participant participant;
    private readonly IService service;

    private readonly Trial trial;

    public ResultForm(IService service,Trial trial)
    {
        this.service = service;
        this.trial = trial;
        InitializeComponent();
        InitializeUI();
    }

    private void AddButton_Click(object sender, EventArgs e)
    {
        if (int.TryParse(pointsTextBox.Text, out int points))
        {
            if (participantsDataGridView.SelectedCells.Count > 0) {
                // Salvați rezultatul pentru participant
                // Aici trebuie să implementați logica de salvare a rezultatului în baza de date
                int selectedRowIndex = participantsDataGridView.SelectedRows[0].Index;
                string participantName =
                    (string)participantsDataGridView.Rows[selectedRowIndex].Cells["Participant Name"].Value;
                int partPoints = Convert.ToInt32(participantsDataGridView.Rows[selectedRowIndex].Cells["Points"].Value);
                long participantId = Convert.ToInt64(participantsDataGridView.Rows[selectedRowIndex].Cells["ID"].Value);
                Participant wantedParticipant = new Participant(participantId, participantName, points);
                Result result = service.AddResult(wantedParticipant, trial, points);
                if (result != null)
                {
                    MessageBox.Show("Result added successfully.", "Success", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("There is already a result for this participant at this trial.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {

                MessageBox.Show("Please enter a valid number of points.", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        else
        {
            MessageBox.Show("Please select a participant.", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
    
    private void InitializeUI()
    {
        participantsDataGridView.ColumnCount = 3;
        participantsDataGridView.Columns[0].Name = "ID";
        participantsDataGridView.Columns[1].Name = "Participant Name";
        participantsDataGridView.Columns[2].Name = "Points";


        // Incarcati lista de participanti in DataGridView folosind metoda service.GetParticipantsForTrial(trialId)
        List<Participant> participants = service.GetParticipantsAtTrial(trial);
        foreach (Participant participant in participants)
        {
            participantsDataGridView.Rows.Add(participant.id, participant.name, service.GetTotalPointsAtTrial(participant,trial));
        }
    }
}
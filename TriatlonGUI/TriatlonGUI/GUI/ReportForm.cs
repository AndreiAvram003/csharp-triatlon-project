using TriatlonGUI.Domain;
using TriatlonGUI.Service;

namespace TriatlonGUI;

    public partial class ReportForm : Form
    {
        private readonly Trial trial;
        private readonly IService service;

        public ReportForm(Trial trial, IService service)
        {
            this.trial = trial;
            this.service = service;
            InitializeComponent();
            InitializeUI();
        }

        private void InitializeUI()
        {
            // Inițializați DataGridView pentru afișarea participanților și punctelor lor
            this.participantsDataGridView.Location = new Point(20, 20);
            this.participantsDataGridView.Size = new Size(400, 200);
            this.Controls.Add(participantsDataGridView);

            // Adăugați coloane la DataGridView
            participantsDataGridView.ColumnCount = 2;
            participantsDataGridView.Columns[0].Name = "Participant Name";
            participantsDataGridView.Columns[1].Name = "Points";

            // Încărcați lista de participanți și punctele lor în DataGridView
            List<Participant> participants = service.GetParticipantsAtTrial(trial);
            foreach (Participant participant in participants)
            {
                participantsDataGridView.Rows.Add(participant.name, service.GetTotalPointsAtTrial(participant, trial));
            }
        }
    }
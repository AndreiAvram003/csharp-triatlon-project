namespace GUI.GUI
{
    partial class ReportForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // Creează un DataGridView pentru afișarea participanților și punctelor lor
           this.participantsDataGridView = new System.Windows.Forms.DataGridView();
           this.participantsDataGridView.Location = new System.Drawing.Point(20, 20);
           this.participantsDataGridView.Size = new System.Drawing.Size(400, 200);
           this.Controls.Add(participantsDataGridView);

            // Adaugă coloane la DataGridView
            participantsDataGridView.ColumnCount = 2;
            participantsDataGridView.Columns[0].Name = "Participant Name";
            participantsDataGridView.Columns[1].Name = "Points";
        }
        
        

        private System.Windows.Forms.DataGridView participantsDataGridView;
    }
}
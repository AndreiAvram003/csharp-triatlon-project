using System.ComponentModel;

namespace GUI.GUI;

partial class ResultForm
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
        this.pointsLabel = new System.Windows.Forms.Label();
        this.pointsLabel.Text = "No. of points:";
        this.pointsLabel.Location = new Point(20, 20);
        this.Controls.Add(pointsLabel);

        // Adăugați un TextBox pentru introducerea numărului de puncte
        this.pointsTextBox = new System.Windows.Forms.TextBox();
        this.pointsTextBox.Location = new Point(130, 20);
        this.Controls.Add(pointsTextBox);

        // Adăugați un DataGridView pentru afișarea listei de participanți
        this.participantsDataGridView = new System.Windows.Forms.DataGridView();
        this.participantsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.participantsDataGridView.Location = new System.Drawing.Point(50, 50);
        this.participantsDataGridView.Name = "participantsDataGridView";
        this.participantsDataGridView.Size = new System.Drawing.Size(400, 250);
        this.participantsDataGridView.TabIndex = 3;
        this.Controls.Add(participantsDataGridView);

        // Adăugați un buton "Add" pentru salvarea rezultatului
        this.addButton = new System.Windows.Forms.Button();
        this.addButton.Text = "Add";
        this.addButton.Location = new Point(20, 360);
        this.addButton.Click += new EventHandler(AddButton_Click);
        this.Controls.Add(addButton);
    }

    private System.Windows.Forms.DataGridView participantsDataGridView;
    private System.Windows.Forms.Button addButton;
    private System.Windows.Forms.TextBox pointsTextBox;
    private System.Windows.Forms.Label pointsLabel;
}




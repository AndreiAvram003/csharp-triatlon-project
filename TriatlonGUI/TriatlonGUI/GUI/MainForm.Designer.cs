using System.ComponentModel;

namespace TriatlonGUI;

partial class MainForm
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
    this.AddResultButton = new System.Windows.Forms.Button();
    this.ViewReportButton = new System.Windows.Forms.Button();
    this.LogoutButton = new System.Windows.Forms.Button();
    this.titleLabel = new System.Windows.Forms.Label();
    this.nameLabel = new System.Windows.Forms.Label();
    this.dataGridViewParticipants = new System.Windows.Forms.DataGridView();
    this.SuspendLayout();
    
    // 
    // AddResultButton
    // 
    this.AddResultButton.Location = new System.Drawing.Point(50, 120);
    this.AddResultButton.Name = "AddResultButton";
    this.AddResultButton.Size = new System.Drawing.Size(150, 50);
    this.AddResultButton.Text = "Add Result";
    this.AddResultButton.Click += new System.EventHandler(this.AddResultButton_Click);
    // 
    // ViewReportButton
    // 
    this.ViewReportButton.Location = new System.Drawing.Point(250, 120);
    this.ViewReportButton.Name = "ViewReportButton";
    this.ViewReportButton.Size = new System.Drawing.Size(150, 50);
    this.ViewReportButton.Text = "View Report";
    this.ViewReportButton.Click += new System.EventHandler(this.ViewReportButton_Click);
    // 
    // LogoutButton
    // 
    this.LogoutButton.Location = new System.Drawing.Point(450, 120);
    this.LogoutButton.Name = "LogoutButton";
    this.LogoutButton.Size = new System.Drawing.Size(150, 50);
    this.LogoutButton.Text = "Logout";
    this.LogoutButton.Click += new System.EventHandler(this.LogoutButton_Click);
    // 
    // titleLabel
    // 
    this.titleLabel.AutoSize = true;
    this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
    this.titleLabel.Location = new System.Drawing.Point(250, 25);
    this.titleLabel.Name = "titleLabel";
    this.titleLabel.Size = new System.Drawing.Size(218, 30);
    this.titleLabel.TabIndex = 0;
    this.titleLabel.Text = "Welcome, Referee!";
    
    // dataGridViewParticipants
    // 
    this.dataGridViewParticipants.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.dataGridViewParticipants.Location = new System.Drawing.Point(50, 220);
    this.dataGridViewParticipants.Name = "dataGridViewParticipants";
    this.dataGridViewParticipants.Size = new System.Drawing.Size(200, 250);
    this.dataGridViewParticipants.TabIndex = 3;
    
    // Adăugăm controalele la formular
    this.Controls.Add(this.AddResultButton);
    this.Controls.Add(this.ViewReportButton);
    this.Controls.Add(this.LogoutButton);
    this.Controls.Add(this.titleLabel);
    this.Controls.Add(this.dataGridViewParticipants);
    
    this.ResumeLayout(false);
}

    private System.Windows.Forms.Button AddResultButton;
    private System.Windows.Forms.Button ViewReportButton;
    private System.Windows.Forms.Button LogoutButton;
    private System.Windows.Forms.Label titleLabel;
    private System.Windows.Forms.Label nameLabel;
    private System.Windows.Forms.DataGridView dataGridViewParticipants;
}
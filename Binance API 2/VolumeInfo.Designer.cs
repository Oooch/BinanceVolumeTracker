namespace Binance_API_2
{
    partial class VolumeInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.coindatagridview = new System.Windows.Forms.DataGridView();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.alerticon = new System.Windows.Forms.NotifyIcon(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.coindatagridview)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // coindatagridview
            // 
            this.coindatagridview.AllowUserToAddRows = false;
            this.coindatagridview.AllowUserToDeleteRows = false;
            this.coindatagridview.AllowUserToOrderColumns = true;
            this.coindatagridview.AllowUserToResizeColumns = false;
            this.coindatagridview.AllowUserToResizeRows = false;
            this.coindatagridview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.coindatagridview.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.coindatagridview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.coindatagridview.DefaultCellStyle = dataGridViewCellStyle2;
            this.coindatagridview.Location = new System.Drawing.Point(-4, -2);
            this.coindatagridview.Name = "coindatagridview";
            this.coindatagridview.ReadOnly = true;
            this.coindatagridview.RowHeadersVisible = false;
            this.coindatagridview.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.coindatagridview.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.coindatagridview.Size = new System.Drawing.Size(744, 587);
            this.coindatagridview.TabIndex = 3;
            this.coindatagridview.SelectionChanged += new System.EventHandler(this.coindatagridview_SelectionChanged);
            this.coindatagridview.Sorted += new System.EventHandler(this.coindatagridview_Sorted);
            // 
            // RefreshButton
            // 
            this.RefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RefreshButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.RefreshButton.Location = new System.Drawing.Point(12, 534);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(117, 36);
            this.RefreshButton.TabIndex = 4;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Visible = false;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // alerticon
            // 
            this.alerticon.Text = "notifyIcon1";
            this.alerticon.Visible = true;
            // 
            // VolumeInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(654, 582);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.coindatagridview);
            this.Name = "VolumeInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Coin Volume";
            ((System.ComponentModel.ISupportInitialize)(this.coindatagridview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.DataGridView coindatagridview;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.NotifyIcon alerticon;
    }
}


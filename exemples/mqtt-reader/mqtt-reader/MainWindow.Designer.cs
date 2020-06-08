namespace mqtt_reader
{
    partial class MainWindow
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label2 = new System.Windows.Forms.Label();
            this.TopicBox = new System.Windows.Forms.TextBox();
            this.butSubscribeUnsubscribe = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.MessagesView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusConnect = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusMessages = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.treeDetail = new System.Windows.Forms.TreeView();
            this.MessagesViewToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Topic";
            // 
            // TopicBox
            // 
            this.TopicBox.Location = new System.Drawing.Point(53, 20);
            this.TopicBox.Name = "TopicBox";
            this.TopicBox.Size = new System.Drawing.Size(314, 20);
            this.TopicBox.TabIndex = 1;
            // 
            // butSubscribeUnsubscribe
            // 
            this.butSubscribeUnsubscribe.Location = new System.Drawing.Point(383, 18);
            this.butSubscribeUnsubscribe.Name = "butSubscribeUnsubscribe";
            this.butSubscribeUnsubscribe.Size = new System.Drawing.Size(162, 23);
            this.butSubscribeUnsubscribe.TabIndex = 2;
            this.butSubscribeUnsubscribe.Text = "Subscribe";
            this.butSubscribeUnsubscribe.UseVisualStyleBackColor = true;
            this.butSubscribeUnsubscribe.Click += new System.EventHandler(this.butSubscribeUnsubscribe_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.MessagesView);
            this.groupBox1.Location = new System.Drawing.Point(16, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(535, 512);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Messages";
            // 
            // MessagesView
            // 
            this.MessagesView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.MessagesView.FullRowSelect = true;
            this.MessagesView.GridLines = true;
            this.MessagesView.Location = new System.Drawing.Point(7, 20);
            this.MessagesView.Name = "MessagesView";
            this.MessagesView.ShowItemToolTips = true;
            this.MessagesView.Size = new System.Drawing.Size(522, 486);
            this.MessagesView.TabIndex = 0;
            this.MessagesView.UseCompatibleStateImageBehavior = false;
            this.MessagesView.View = System.Windows.Forms.View.Details;
            this.MessagesView.SelectedIndexChanged += new System.EventHandler(this.MessagesView_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Date";
            this.columnHeader1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader1.Width = 107;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Message";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 181;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Raw Json";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 230;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusConnect,
            this.toolStripStatusMessages});
            this.statusStrip1.Location = new System.Drawing.Point(0, 566);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(943, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusConnect
            // 
            this.toolStripStatusConnect.Name = "toolStripStatusConnect";
            this.toolStripStatusConnect.Size = new System.Drawing.Size(86, 17);
            this.toolStripStatusConnect.Text = "Not connected";
            // 
            // toolStripStatusMessages
            // 
            this.toolStripStatusMessages.Name = "toolStripStatusMessages";
            this.toolStripStatusMessages.Size = new System.Drawing.Size(0, 17);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.treeDetail);
            this.groupBox2.Location = new System.Drawing.Point(557, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(374, 553);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Details";
            // 
            // treeDetail
            // 
            this.treeDetail.Location = new System.Drawing.Point(6, 19);
            this.treeDetail.Name = "treeDetail";
            this.treeDetail.Size = new System.Drawing.Size(362, 528);
            this.treeDetail.TabIndex = 0;
            // 
            // MainWindow
            // 
            this.ClientSize = new System.Drawing.Size(943, 588);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.butSubscribeUnsubscribe);
            this.Controls.Add(this.TopicBox);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainWindow";
            this.Text = "MQTT-Reader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TopicBox;
        private System.Windows.Forms.Button butSubscribeUnsubscribe;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusConnect;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusMessages;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TreeView treeDetail;
        private System.Windows.Forms.ListView MessagesView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ToolTip MessagesViewToolTip;
    }
}


namespace QuickBox
{
    partial class FrmFiles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFiles));
            this.tabFileGroup = new System.Windows.Forms.TabControl();
            this.gbConfigGroupName = new System.Windows.Forms.GroupBox();
            this.gbDoChangeGroup = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOPCancelModifyName = new System.Windows.Forms.Button();
            this.btnDoAddGroupName = new System.Windows.Forms.Button();
            this.txtGroupName = new System.Windows.Forms.TextBox();
            this.btnDoModifyGroupName = new System.Windows.Forms.Button();
            this.gbOperateGroup = new System.Windows.Forms.GroupBox();
            this.lbGroupNames = new System.Windows.Forms.ListBox();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnDoDeleteGroupName = new System.Windows.Forms.Button();
            this.btnOPAddGroupName = new System.Windows.Forms.Button();
            this.btnOPModifyGroupName = new System.Windows.Forms.Button();
            this.listViewContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SetOptionContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.listViewLove = new System.Windows.Forms.ListView();
            this.loveListViewContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnExit = new QuickBox.MG.Controls.ImageButton();
            this.btnOption = new QuickBox.MG.Controls.ImageButton();
            this.gbConfigGroupName.SuspendLayout();
            this.gbDoChangeGroup.SuspendLayout();
            this.gbOperateGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabFileGroup
            // 
            this.tabFileGroup.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabFileGroup.ItemSize = new System.Drawing.Size(0, 24);
            this.tabFileGroup.Location = new System.Drawing.Point(1, 24);
            this.tabFileGroup.Name = "tabFileGroup";
            this.tabFileGroup.SelectedIndex = 0;
            this.tabFileGroup.Size = new System.Drawing.Size(490, 250);
            this.tabFileGroup.TabIndex = 0;
            // 
            // gbConfigGroupName
            // 
            this.gbConfigGroupName.BackColor = System.Drawing.SystemColors.Control;
            this.gbConfigGroupName.BackgroundImage = global::QuickBox.Properties.Resources.bg2;
            this.gbConfigGroupName.Controls.Add(this.gbDoChangeGroup);
            this.gbConfigGroupName.Controls.Add(this.gbOperateGroup);
            this.gbConfigGroupName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbConfigGroupName.Location = new System.Drawing.Point(1, 24);
            this.gbConfigGroupName.Name = "gbConfigGroupName";
            this.gbConfigGroupName.Size = new System.Drawing.Size(490, 320);
            this.gbConfigGroupName.TabIndex = 1;
            this.gbConfigGroupName.TabStop = false;
            this.gbConfigGroupName.Text = "配置分组";
            // 
            // gbOPGroup
            // 
            this.gbDoChangeGroup.Controls.Add(this.label1);
            this.gbDoChangeGroup.Controls.Add(this.btnOPCancelModifyName);
            this.gbDoChangeGroup.Controls.Add(this.btnDoAddGroupName);
            this.gbDoChangeGroup.Controls.Add(this.txtGroupName);
            this.gbDoChangeGroup.Controls.Add(this.btnDoModifyGroupName);
            this.gbDoChangeGroup.Location = new System.Drawing.Point(386, 20);
            this.gbDoChangeGroup.Name = "gbOPGroup";
            this.gbDoChangeGroup.Size = new System.Drawing.Size(280, 230);
            this.gbDoChangeGroup.TabIndex = 7;
            this.gbDoChangeGroup.TabStop = false;
            this.gbDoChangeGroup.Text = "配置分组名称";
            this.gbDoChangeGroup.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "分组名称：";
            // 
            // btnOPCancelModifyName
            // 
            this.btnOPCancelModifyName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOPCancelModifyName.Location = new System.Drawing.Point(173, 99);
            this.btnOPCancelModifyName.Name = "btnOPCancelModifyName";
            this.btnOPCancelModifyName.Size = new System.Drawing.Size(75, 23);
            this.btnOPCancelModifyName.TabIndex = 4;
            this.btnOPCancelModifyName.Text = "取消";
            this.btnOPCancelModifyName.UseVisualStyleBackColor = true;
            this.btnOPCancelModifyName.Click += new System.EventHandler(this.btnOPCancelModifyName_Click);
            // 
            // btnDoAddGroupName
            // 
            this.btnDoAddGroupName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDoAddGroupName.Location = new System.Drawing.Point(48, 99);
            this.btnDoAddGroupName.Name = "btnDoAddGroup";
            this.btnDoAddGroupName.Size = new System.Drawing.Size(75, 23);
            this.btnDoAddGroupName.TabIndex = 0;
            this.btnDoAddGroupName.Text = "添加";
            this.btnDoAddGroupName.UseVisualStyleBackColor = true;
            this.btnDoAddGroupName.Click += new System.EventHandler(this.btnDoAddGroup_Click);
            // 
            // txtGroupName
            // 
            this.txtGroupName.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtGroupName.Location = new System.Drawing.Point(48, 72);
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.Size = new System.Drawing.Size(200, 21);
            this.txtGroupName.TabIndex = 1;
            // 
            // btnDoModifyGroupName
            // 
            this.btnDoModifyGroupName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDoModifyGroupName.Location = new System.Drawing.Point(48, 99);
            this.btnDoModifyGroupName.Name = "btnDoModifyGroup";
            this.btnDoModifyGroupName.Size = new System.Drawing.Size(75, 23);
            this.btnDoModifyGroupName.TabIndex = 3;
            this.btnDoModifyGroupName.Text = "保存";
            this.btnDoModifyGroupName.UseVisualStyleBackColor = true;
            this.btnDoModifyGroupName.Click += new System.EventHandler(this.btnDoModifyGroup_Click);
            // 
            // gbOperateGroup
            // 
            this.gbOperateGroup.Controls.Add(this.lbGroupNames);
            this.gbOperateGroup.Controls.Add(this.btnReturn);
            this.gbOperateGroup.Controls.Add(this.btnDoDeleteGroupName);
            this.gbOperateGroup.Controls.Add(this.btnOPAddGroupName);
            this.gbOperateGroup.Controls.Add(this.btnOPModifyGroupName);
            this.gbOperateGroup.Location = new System.Drawing.Point(100, 20);
            this.gbOperateGroup.Name = "gbGroupList";
            this.gbOperateGroup.Size = new System.Drawing.Size(280, 230);
            this.gbOperateGroup.TabIndex = 6;
            this.gbOperateGroup.TabStop = false;
            this.gbOperateGroup.Text = "配置分组信息";
            // 
            // lbGroupNames
            // 
            this.lbGroupNames.FormattingEnabled = true;
            this.lbGroupNames.ItemHeight = 12;
            this.lbGroupNames.Location = new System.Drawing.Point(6, 20);
            this.lbGroupNames.Name = "lbGroupNames";
            this.lbGroupNames.Size = new System.Drawing.Size(176, 196);
            this.lbGroupNames.TabIndex = 2;
            // 
            // btnReturn
            // 
            this.btnReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturn.Location = new System.Drawing.Point(188, 193);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(75, 23);
            this.btnReturn.TabIndex = 5;
            this.btnReturn.Text = "返回";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnDoDeleteGroupName
            // 
            this.btnDoDeleteGroupName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDoDeleteGroupName.Location = new System.Drawing.Point(187, 95);
            this.btnDoDeleteGroupName.Name = "btnDoDeleteGroupName";
            this.btnDoDeleteGroupName.Size = new System.Drawing.Size(75, 23);
            this.btnDoDeleteGroupName.TabIndex = 3;
            this.btnDoDeleteGroupName.Text = "删除";
            this.btnDoDeleteGroupName.UseVisualStyleBackColor = true;
            this.btnDoDeleteGroupName.Click += new System.EventHandler(this.btnDoDeleteGroupName_Click);
            // 
            // btnOPAddGroupName
            // 
            this.btnOPAddGroupName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOPAddGroupName.Location = new System.Drawing.Point(188, 20);
            this.btnOPAddGroupName.Name = "btnOPAddGroupName";
            this.btnOPAddGroupName.Size = new System.Drawing.Size(75, 23);
            this.btnOPAddGroupName.TabIndex = 4;
            this.btnOPAddGroupName.Text = "添加";
            this.btnOPAddGroupName.UseVisualStyleBackColor = true;
            this.btnOPAddGroupName.Click += new System.EventHandler(this.btnOPAddGroupName_Click);
            // 
            // btnOPModifyGroupName
            // 
            this.btnOPModifyGroupName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOPModifyGroupName.Location = new System.Drawing.Point(187, 49);
            this.btnOPModifyGroupName.Name = "btnOPModifyGroupName";
            this.btnOPModifyGroupName.Size = new System.Drawing.Size(75, 23);
            this.btnOPModifyGroupName.TabIndex = 0;
            this.btnOPModifyGroupName.Text = "修改";
            this.btnOPModifyGroupName.UseVisualStyleBackColor = true;
            this.btnOPModifyGroupName.Click += new System.EventHandler(this.btnOPModifyGroupName_Click);
            // 
            // listViewContextMenuStrip
            // 
            this.listViewContextMenuStrip.Name = "listViewContextMenuStrip";
            this.listViewContextMenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // SetOptionContextMenuStrip
            // 
            this.SetOptionContextMenuStrip.Name = "SetOptionContextMenuStrip";
            this.SetOptionContextMenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // listViewLove
            // 
            this.listViewLove.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewLove.Location = new System.Drawing.Point(1, 275);
            this.listViewLove.Name = "listViewLove";
            this.listViewLove.Size = new System.Drawing.Size(490, 70);
            this.listViewLove.TabIndex = 5;
            this.listViewLove.UseCompatibleStateImageBehavior = false;
            // 
            // loveListViewContextMenuStrip
            // 
            this.loveListViewContextMenuStrip.Name = "loveListViewContextMenuStrip";
            this.loveListViewContextMenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImage = global::QuickBox.Properties.Resources.btnExit;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExit.BtnEnabled = true;
            this.btnExit.ButtonState = QuickBox.MG.Controls.ImageButton.State.Normal;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Location = new System.Drawing.Point(451, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(41, 22);
            this.btnExit.TabIndex = 4;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnOption
            // 
            this.btnOption.BackColor = System.Drawing.Color.Transparent;
            this.btnOption.BackgroundImage = global::QuickBox.Properties.Resources.btnOption;
            this.btnOption.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnOption.BtnEnabled = true;
            this.btnOption.ButtonState = QuickBox.MG.Controls.ImageButton.State.Normal;
            this.btnOption.FlatAppearance.BorderSize = 0;
            this.btnOption.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOption.Location = new System.Drawing.Point(419, 0);
            this.btnOption.Name = "btnOption";
            this.btnOption.Size = new System.Drawing.Size(32, 22);
            this.btnOption.TabIndex = 3;
            this.btnOption.UseVisualStyleBackColor = false;
            this.btnOption.Click += new System.EventHandler(this.btnOption_Click);
            // 
            // FrmFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BackgroundImage = global::QuickBox.Properties.Resources.bg;
            this.ClientSize = new System.Drawing.Size(492, 346);
            this.ControlBox = false;
            this.Controls.Add(this.tabFileGroup);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnOption);
            this.Controls.Add(this.listViewLove);
            this.Controls.Add(this.gbConfigGroupName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFiles";
            this.Text = "管理快捷方式";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmFiles_FormClosed);
            this.Load += new System.EventHandler(this.FrmFiles_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmFiles_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FrmFiles_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FrmFiles_MouseUp);
            this.gbConfigGroupName.ResumeLayout(false);
            this.gbDoChangeGroup.ResumeLayout(false);
            this.gbDoChangeGroup.PerformLayout();
            this.gbOperateGroup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabFileGroup;
        private System.Windows.Forms.Button btnDoAddGroupName;
        private System.Windows.Forms.ListBox lbGroupNames;
        private System.Windows.Forms.Button btnDoModifyGroupName;
        private System.Windows.Forms.Button btnDoDeleteGroupName;
        private System.Windows.Forms.TextBox txtGroupName;
        private System.Windows.Forms.Button btnOPModifyGroupName;
        private System.Windows.Forms.Button btnOPAddGroupName;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.GroupBox gbConfigGroupName;
        private System.Windows.Forms.Button btnOPCancelModifyName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbOperateGroup;
        private System.Windows.Forms.GroupBox gbDoChangeGroup;
        private System.Windows.Forms.ContextMenuStrip listViewContextMenuStrip;
        private QuickBox.MG.Controls.ImageButton btnOption;
        private QuickBox.MG.Controls.ImageButton btnExit;
        private System.Windows.Forms.ContextMenuStrip SetOptionContextMenuStrip;
        private System.Windows.Forms.ListView listViewLove;
        private System.Windows.Forms.ContextMenuStrip loveListViewContextMenuStrip;

    }
}
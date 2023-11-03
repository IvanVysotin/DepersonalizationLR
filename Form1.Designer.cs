namespace DepersonalizationLR
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            btnDepersonalize = new Button();
            btnPersonalize = new Button();
            label2 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(159, 15);
            label1.TabIndex = 0;
            label1.Text = "Выберите одно из действий";
            // 
            // btnDepersonalize
            // 
            btnDepersonalize.Location = new Point(12, 27);
            btnDepersonalize.Name = "btnDepersonalize";
            btnDepersonalize.Size = new Size(177, 58);
            btnDepersonalize.TabIndex = 1;
            btnDepersonalize.Text = "Обезличить";
            btnDepersonalize.UseVisualStyleBackColor = true;
            btnDepersonalize.Click += btnDepersonalize_Click;
            // 
            // btnPersonalize
            // 
            btnPersonalize.Location = new Point(12, 91);
            btnPersonalize.Name = "btnPersonalize";
            btnPersonalize.Size = new Size(177, 68);
            btnPersonalize.TabIndex = 2;
            btnPersonalize.Text = "Деобезличить";
            btnPersonalize.UseVisualStyleBackColor = true;
            btnPersonalize.Click += btnPersonalize_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(195, 9);
            label2.Name = "label2";
            label2.Size = new Size(573, 160);
            label2.TabIndex = 3;
            label2.Text = "Нажимая на одну из представленных кнопок, \r\nВы даете свое согласие на обработку \r\nВаших персональных данных, в соответствии \r\nс №152-ФЗ «О персональных данных» \r\nот 27.07.2006 года.";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 184);
            Controls.Add(label2);
            Controls.Add(btnPersonalize);
            Controls.Add(btnDepersonalize);
            Controls.Add(label1);
            Name = "Form1";
            Text = "DepersonalizationLR";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btnDepersonalize;
        private Button btnPersonalize;
        private Label label2;
    }
}
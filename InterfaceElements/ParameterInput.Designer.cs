
namespace RPK.InterfaceElements
{
    partial class ParameterInput
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.label = new System.Windows.Forms.Label();
            this.textBox = new System.Windows.Forms.TextBox();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.label);
            this.groupBox.Controls.Add(this.textBox);
            this.groupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox.Location = new System.Drawing.Point(0, 0);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(312, 71);
            this.groupBox.TabIndex = 0;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Наименование параметра";
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(232, 30);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(75, 25);
            this.label.TabIndex = 1;
            this.label.Text = "Ед. изм.";
            // 
            // textBox
            // 
            this.textBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.textBox.Location = new System.Drawing.Point(3, 27);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(223, 31);
            this.textBox.TabIndex = 0;
            this.textBox.Validating += new System.ComponentModel.CancelEventHandler(this.TextBox_Validating);
            // 
            // ParameterInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox);
            this.Name = "ParameterInput";
            this.Size = new System.Drawing.Size(312, 71);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        protected System.Windows.Forms.GroupBox groupBox;
        protected System.Windows.Forms.Label label;
        protected System.Windows.Forms.TextBox textBox;
    }
}

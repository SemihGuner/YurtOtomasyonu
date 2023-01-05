
namespace YurtOtomasyonu
{
    partial class VeriMenusu
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtKontrol = new System.Windows.Forms.Label();
            this.btngozat = new System.Windows.Forms.Button();
            this.dosyaAdi = new System.Windows.Forms.Label();
            this.buttonImp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(55, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Veri Export";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(55, 180);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(163, 32);
            this.label2.TabIndex = 1;
            this.label2.Text = "Veri Import";
            // 
            // txtKontrol
            // 
            this.txtKontrol.AutoSize = true;
            this.txtKontrol.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtKontrol.Location = new System.Drawing.Point(583, 229);
            this.txtKontrol.Name = "txtKontrol";
            this.txtKontrol.Size = new System.Drawing.Size(158, 32);
            this.txtKontrol.TabIndex = 2;
            this.txtKontrol.Text = "KONTROL";
            // 
            // btngozat
            // 
            this.btngozat.Location = new System.Drawing.Point(233, 179);
            this.btngozat.Name = "btngozat";
            this.btngozat.Size = new System.Drawing.Size(137, 44);
            this.btngozat.TabIndex = 3;
            this.btngozat.Text = "Gözat...";
            this.btngozat.UseVisualStyleBackColor = true;
            this.btngozat.Click += new System.EventHandler(this.btngozat_Click);
            // 
            // dosyaAdi
            // 
            this.dosyaAdi.AutoSize = true;
            this.dosyaAdi.Location = new System.Drawing.Point(586, 195);
            this.dosyaAdi.Name = "dosyaAdi";
            this.dosyaAdi.Size = new System.Drawing.Size(36, 17);
            this.dosyaAdi.TabIndex = 4;
            this.dosyaAdi.Text = "path";
            this.dosyaAdi.Click += new System.EventHandler(this.dosyaAdi_Click);
            // 
            // buttonImp
            // 
            this.buttonImp.Location = new System.Drawing.Point(233, 229);
            this.buttonImp.Name = "buttonImp";
            this.buttonImp.Size = new System.Drawing.Size(137, 44);
            this.buttonImp.TabIndex = 5;
            this.buttonImp.Text = "IMPORT";
            this.buttonImp.UseVisualStyleBackColor = true;
            this.buttonImp.Click += new System.EventHandler(this.buttonImp_Click);
            // 
            // VeriMenusu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1076, 506);
            this.Controls.Add(this.buttonImp);
            this.Controls.Add(this.dosyaAdi);
            this.Controls.Add(this.btngozat);
            this.Controls.Add(this.txtKontrol);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "VeriMenusu";
            this.Text = "VeriMenusu";
            this.Load += new System.EventHandler(this.VeriMenusu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label txtKontrol;
        private System.Windows.Forms.Button btngozat;
        private System.Windows.Forms.Label dosyaAdi;
        private System.Windows.Forms.Button buttonImp;
    }
}
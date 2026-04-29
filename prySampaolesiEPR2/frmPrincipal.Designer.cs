namespace prySampaolesiEPR2
{
    partial class frmPrincipal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnMigrar = new System.Windows.Forms.Button();
            this.btnConsultaBd = new System.Windows.Forms.Button();
            this.txtRegistro = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(12, 12);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(150, 30);
            this.btnBuscar.TabIndex = 0;
            this.btnBuscar.Text = "Buscar Archivos";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnMigrar
            // 
            this.btnMigrar.Location = new System.Drawing.Point(12, 48);
            this.btnMigrar.Name = "btnMigrar";
            this.btnMigrar.Size = new System.Drawing.Size(150, 30);
            this.btnMigrar.TabIndex = 1;
            this.btnMigrar.Text = "Iniciar Migración";
            this.btnMigrar.UseVisualStyleBackColor = true;
            this.btnMigrar.Click += new System.EventHandler(this.btnMigrar_Click);
            // 
            // btnConsultaBd
            // 
            this.btnConsultaBd.Location = new System.Drawing.Point(337, 444);
            this.btnConsultaBd.Name = "btnConsultaBd";
            this.btnConsultaBd.Size = new System.Drawing.Size(150, 30);
            this.btnConsultaBd.TabIndex = 2;
            this.btnConsultaBd.Text = "Consultar BD";
            this.btnConsultaBd.UseVisualStyleBackColor = true;
            this.btnConsultaBd.Click += new System.EventHandler(this.btnConsultaBd_Click);
            // 
            // txtRegistro
            // 
            this.txtRegistro.Location = new System.Drawing.Point(12, 84);
            this.txtRegistro.Multiline = true;
            this.txtRegistro.Name = "txtRegistro";
            this.txtRegistro.ReadOnly = true;
            this.txtRegistro.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRegistro.Size = new System.Drawing.Size(475, 354);
            this.txtRegistro.TabIndex = 3;
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 479);
            this.Controls.Add(this.btnConsultaBd);
            this.Controls.Add(this.txtRegistro);
            this.Controls.Add(this.btnMigrar);
            this.Controls.Add(this.btnBuscar);
            this.Name = "frmPrincipal";
            this.Text = "Migracion";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnMigrar;
        private System.Windows.Forms.Button btnConsultaBd;
        private System.Windows.Forms.TextBox txtRegistro;
    }
}


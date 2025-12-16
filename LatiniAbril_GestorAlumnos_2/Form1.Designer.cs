namespace LatiniAbril_GestorAlumnos_2
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
            btnCrear = new Button();
            btnLeer = new Button();
            btnModificar = new Button();
            btnEliminar = new Button();
            btnConvertir = new Button();
            btnReporte = new Button();
            txtSalida = new TextBox();
            btnSalir = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(311, 9);
            label1.Name = "label1";
            label1.Size = new Size(590, 38);
            label1.TabIndex = 0;
            label1.Text = "=== GESTOR DE ARCHIVOS DE TEXTO ===";
            // 
            // btnCrear
            // 
            btnCrear.Location = new Point(12, 92);
            btnCrear.Name = "btnCrear";
            btnCrear.Size = new Size(288, 34);
            btnCrear.TabIndex = 1;
            btnCrear.Text = "Crear nuevo archivo";
            btnCrear.UseVisualStyleBackColor = true;
            btnCrear.Click += btnCrear_Click;
            // 
            // btnLeer
            // 
            btnLeer.Location = new Point(12, 151);
            btnLeer.Name = "btnLeer";
            btnLeer.Size = new Size(289, 34);
            btnLeer.TabIndex = 2;
            btnLeer.Text = "Leer archivo existente";
            btnLeer.UseVisualStyleBackColor = true;
            btnLeer.Click += button2_Click;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(12, 213);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(288, 34);
            btnModificar.TabIndex = 3;
            btnModificar.Text = "Modificar archivo existente";
            btnModificar.UseVisualStyleBackColor = true;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(12, 274);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(288, 34);
            btnEliminar.TabIndex = 4;
            btnEliminar.Text = "Eliminar archivo";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnConvertir
            // 
            btnConvertir.Location = new Point(12, 334);
            btnConvertir.Name = "btnConvertir";
            btnConvertir.Size = new Size(288, 34);
            btnConvertir.TabIndex = 5;
            btnConvertir.Text = "Convertir entre formatos";
            btnConvertir.UseVisualStyleBackColor = true;
            btnConvertir.Click += btnConvertir_Click;
            // 
            // btnReporte
            // 
            btnReporte.Location = new Point(12, 397);
            btnReporte.Name = "btnReporte";
            btnReporte.Size = new Size(289, 34);
            btnReporte.TabIndex = 6;
            btnReporte.Text = "Crear reporte con corte de control";
            btnReporte.UseVisualStyleBackColor = true;
            btnReporte.Click += btnReporte_Click;
            // 
            // txtSalida
            // 
            txtSalida.BackColor = SystemColors.ButtonHighlight;
            txtSalida.Location = new Point(371, 80);
            txtSalida.Multiline = true;
            txtSalida.Name = "txtSalida";
            txtSalida.ReadOnly = true;
            txtSalida.ScrollBars = ScrollBars.Both;
            txtSalida.Size = new Size(813, 479);
            txtSalida.TabIndex = 7;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(1072, 574);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(112, 34);
            btnSalir.TabIndex = 8;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1196, 620);
            Controls.Add(btnSalir);
            Controls.Add(txtSalida);
            Controls.Add(btnReporte);
            Controls.Add(btnConvertir);
            Controls.Add(btnEliminar);
            Controls.Add(btnModificar);
            Controls.Add(btnLeer);
            Controls.Add(btnCrear);
            Controls.Add(label1);
            Font = new Font("Microsoft Sans Serif", 8.25F);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btnCrear;
        private Button btnLeer;
        private Button btnModificar;
        private Button btnEliminar;
        private Button btnConvertir;
        private Button btnReporte;
        private TextBox txtSalida;
        private Button btnSalir;
    }
}

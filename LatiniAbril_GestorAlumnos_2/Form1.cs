//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LatiniAbril_GestorAlumnos_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Archivos de texto|*.txt;*.csv;*.json;*.xml";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string resultado = menu.OpcionLeerArchivo(this, ofd.FileName);
                txtSalida.Text = resultado;
            }
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            menu.OpcionCrearNuevoArchivo(this);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Archivos de texto|*.txt;*.csv;*.json;*.xml";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                menu.OpcionModificarArchivo(this, ofd.FileName);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Archivos de texto|*.txt;*.csv;*.json;*.xml";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                menu.OpcionEliminarArchivo(this, ofd.FileName);
            }
        }

        private void btnConvertir_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Archivos de texto|*.txt;*.csv;*.json;*.xml";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                menu.OpcionConvertirEntreFormatos(this, ofd.FileName);
            }
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Archivos de texto|*.txt;*.csv;*.json;*.xml";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string reporte = menu.OpcionCrearReporte(this, ofd.FileName);
                txtSalida.Text = reporte;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

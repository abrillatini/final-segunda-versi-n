using System.Windows.Forms;

namespace LatiniAbril_GestorAlumnos_2
{
    public static class GeneradorReportes
    {
        public static string CrearReporte(Form owner, string ruta)
        {
            return menu.OpcionCrearReporte(owner, ruta);
        }
    }
}
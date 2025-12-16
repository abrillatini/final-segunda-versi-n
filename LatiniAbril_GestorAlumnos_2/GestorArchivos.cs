using System.Windows.Forms;

namespace LatiniAbril_GestorAlumnos_2
{
    public static class GestorArchivos
    {
        public static void CrearNuevoArchivo(Form owner)
        {
            menu.OpcionCrearNuevoArchivo(owner);
        }

        public static string LeerArchivo(Form owner, string ruta)
        {
            return menu.OpcionLeerArchivo(owner, ruta);
        }

        public static void ModificarArchivo(Form owner, string ruta)
        {
            menu.OpcionModificarArchivo(owner, ruta);
        }

        public static void EliminarArchivo(Form owner, string ruta)
        {
           menu.OpcionEliminarArchivo(owner, ruta);
        }
    }
}
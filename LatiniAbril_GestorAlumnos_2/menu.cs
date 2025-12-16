using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;      //ordena, agrupa y cuenta listas 
using System.Text;      // permite armar textos grandes
using System.Text.Json; //lee y escribe archivos JSON
using System.Xml.Linq;  //lee y escribe archivos XML
using System.Windows.Forms;
using Microsoft.VisualBasic;    //permite usar inputbox
using LatiniAbril_GestorAlumnos_2.Models; //permite usar la clase alumno

namespace LatiniAbril_GestorAlumnos_2
{
    public static class menu
    {
        //SUB-MENÚ 1: CREAR NUEVO ARCHIVO!!

        public static void OpcionCrearNuevoArchivo(Form owner)
        {
            // solicita el nombre del archivo

            string nombre = Prompt("Ingrese el nombre del archivo (sin extensión):");
            if (nombre == null) return; //si se presiona cancelar, se corta el ingreso y vuelve al menú

            if (string.IsNullOrWhiteSpace(nombre)) //validación, si esta vacío finaliza
            {
                MessageBox.Show("Nombre inválido.");
                return;

                /*ACLARACIONES:

                prompt = muestra ventana inputbox

                */

            }

            // solicita el formato

            string formato = Prompt("Ingrese formato (TXT, CSV, JSON, XML):");
            if (formato == null) return; //si se presiona cancelar, se corta el ingreso y vuelve al menú

            formato = formato.ToUpper(); //conversión mayusc.
            if (formato != "TXT" && formato != "CSV" && formato != "JSON" && formato != "XML") //validación
            {
                MessageBox.Show("Formato no válido.");
                return;
            }

            // solicita la cantidad de alumnos para registrar

            string cantStr = Prompt("Cantidad de alumnos a registrar:"); //string porque inputbox acepta solo string
            if (cantStr == null) return; //si se presiona cancelar, se corta el ingreso y vuelve al menú

            if (!int.TryParse(cantStr, out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Cantidad inválida.");
                return;

                /*ACLARACIONES:

                salta error si no se escribe un número o un valo <= a 0
                TryParse: convierte un string a int
               */
            }

            // por cada alumno se solicita

            List<Alumno> lista = new List<Alumno>();

            for (int i = 0; i < cantidad; i++)
            {
                MessageBox.Show($"Cargando alumno {i + 1} de {cantidad}");

                Alumno alu = new Alumno();

                // ✅ CAMBIO: llamada unificada de legajo (AGREGAR)
                alu.Legajo = PedirLegajoParaAgregar(lista, "Legajo");
                if (alu.Legajo == null) return; //si se presiona cancelar, se corta el ingreso y vuelve al menú

                alu.Apellido = PedirNoVacio("Apellido");
                if (alu.Apellido == null) return; //si se presiona cancelar, se corta el ingreso y vuelve al menú

                alu.Nombres = PedirNoVacio("Nombres");
                if (alu.Nombres == null) return; //si se presiona cancelar, se corta el ingreso y vuelve al menú

                alu.NumeroDocumento = PedirNoVacio("Número de Documento");
                if (alu.NumeroDocumento == null) return; //si se presiona cancelar, se corta el ingreso y vuelve al menú

                // ✅ AHORA: si el mail es inválido, SOLO se vuelve a pedir el mail (no reinicia todo)
                alu.Email = PedirEmail(); //método para evitar ingreso de datos nulo
                if (alu.Email == null) return; //si se presiona cancelar, se corta el ingreso y vuelve al menú

                alu.Telefono = PedirNoVacio("Teléfono");
                if (alu.Telefono == null) return; //si se presiona cancelar, se corta el ingreso y vuelve al menú

                lista.Add(alu);
            }

            string rutaBase = AppDomain.CurrentDomain.BaseDirectory;
            // me da la carpeta donde está guardado mi programa

            string rutaSinExt = Path.Combine(rutaBase, nombre);
            //me junta la carpeta más el nombre del archivo



            //guardar con extensión correspondiente
            switch (formato)
            {
                case "TXT":
                    GuardarTXT(rutaSinExt + ".txt", lista);
                    break;
                case "CSV":
                    GuardarCSV(rutaSinExt + ".csv", lista);
                    break;
                case "JSON":
                    GuardarJSON(rutaSinExt + ".json", lista);
                    break;
                case "XML":
                    GuardarXML(rutaSinExt + ".xml", lista);
                    break;
            }


            MessageBox.Show($"Archivo creado correctamente:\n{rutaSinExt}.{formato.ToLower()}");
        }


        //método para guardar en TXT
        private static void GuardarTXT(string ruta, List<Alumno> lista)
        {
            using (StreamWriter sw = new StreamWriter(ruta)) //abro archivo para escribirlo, termina el bloque y se cierra
            {
                foreach (var a in lista) //recorre los alumnos y genero una línea por cada uno
                {
                    // Legajo|Apellido|Nombres|NumeroDocumento|Email|Telefono
                    string linea = $"{a.Legajo}|{a.Apellido}|{a.Nombres}|{a.NumeroDocumento}|{a.Email}|{a.Telefono}";
                    sw.WriteLine(linea);
                }
            }
        }

        //método  para guardar en CSV
        private static void GuardarCSV(string ruta, List<Alumno> lista)
        {
            using (StreamWriter sw = new StreamWriter(ruta))
            {
                sw.WriteLine("Legajo,Apellido,Nombres,NumeroDocumento,Email,Telefono");

                foreach (var a in lista) //recorre alumnos y genera línea por cada uno
                {
                    string linea = $"{a.Legajo},{a.Apellido},{a.Nombres},{a.NumeroDocumento},{a.Email},{a.Telefono}";
                    sw.WriteLine(linea);
                }
            }
        }

        //método para guardar en JSON
        private static void GuardarJSON(string ruta, List<Alumno> lista)
        {
            string json = JsonSerializer.Serialize(lista, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ruta, json);

            //JsonSerialize: método que realiza la conversión

        }

        //guardar en XML
        private static void GuardarXML(string ruta, List<Alumno> lista)
        {
            XElement raiz = new XElement("Alumnos");    //nodo raíz

            foreach (var a in lista)    //creo un nodo por cada alumno
            {
                XElement xAlumno = new XElement("Alumno",
                    new XElement("Legajo", a.Legajo),
                    new XElement("Apellido", a.Apellido),
                    new XElement("Nombres", a.Nombres),
                    new XElement("NumeroDocumento", a.NumeroDocumento),
                    new XElement("Email", a.Email),
                    new XElement("Telefono", a.Telefono)
                );

                raiz.Add(xAlumno);
            }

            XDocument doc = new XDocument(raiz);    //
            doc.Save(ruta);
        }


        //métodos aux.
        private static string Prompt(string mensaje)
        {
            return Interaction.InputBox(mensaje, "Entrada de datos");
        }

        private static string PedirNoVacio(string campo)
        {
            string valor;
            do
            {
                valor = Prompt($"Ingrese {campo}:");
                if (valor == null) return null; //si se presiona cancelar, se corta el ingreso y vuelve al menú

                if (string.IsNullOrWhiteSpace(valor))
                    MessageBox.Show($"{campo} no puede estar vacío.");
            } while (string.IsNullOrWhiteSpace(valor));

            return valor;

            //va a repetir hasta que no esté vacío
        }


        private static string PedirLegajo(string mensaje, List<Alumno> alumnos = null, bool debeSerUnico = false, bool debeExistir = false)
        {
            while (true)
            {
                string legajo = PedirNoVacio(mensaje);
                if (legajo == null) return null; // cancelar

                legajo = legajo.Trim();

                if (alumnos == null)
                    return legajo;

                bool existe = BuscarPorLegajo(alumnos, legajo) != null;

                if (debeSerUnico && existe)
                {
                    MessageBox.Show("Ya existe un alumno con ese legajo. Ingrese otro.");
                    continue;
                }

                if (debeExistir && !existe)
                {
                    MessageBox.Show("No se encontró un alumno con ese legajo. Ingrese uno existente.");
                    continue;
                }

                return legajo;
            }
        }

        // ✅ Nuevo método: pide email y si no tiene @ solo repite el email
        private static string PedirEmail()
        {
            string email;

            do
            {
                email = Prompt("Ingrese Email:"); //método para evitar ingreso de datos nulo
                if (email == null) return null; //si se presiona cancelar, se corta el ingreso y vuelve al menú

                if (string.IsNullOrWhiteSpace(email))
                {
                    MessageBox.Show("Email no puede estar vacío.");
                    continue;
                }

                if (!email.Contains("@")) //validación de mail
                {
                    MessageBox.Show("Email inválido. Debe contener '@'. Se vuelve a pedir el Email.");
                    continue;
                }

                return email;

            } while (true);
        }


        //_______________________________________________________________________________________________


        // SUB-MENÚ 2: LEER ARCHIVO EXISTENTE.

        public static string OpcionLeerArchivo(Form owner, string rutaArchivo)
        {
            if (!File.Exists(rutaArchivo))      //si no existe se va a cortar
            {
                return "El archivo no existe.";
            }

            string extension = Path.GetExtension(rutaArchivo).ToLower(); //guardo típo de archivo
            List<Alumno> alumnos = new List<Alumno>(); // lista donde cargo todos los alumnos

            try
            {
                switch (extension)
                {
                    case ".txt":
                        alumnos = LeerTXT(rutaArchivo);
                        break;

                    case ".csv":
                        alumnos = LeerCSV(rutaArchivo);
                        break;

                    case ".json":
                        alumnos = LeerJSON(rutaArchivo);
                        break;

                    case ".xml":
                        alumnos = LeerXML(rutaArchivo);
                        break;

                    default:
                        return "Formato no soportado.";
                }
            }
            catch (Exception ex)
            {
                return "Error al leer archivo: " + ex.Message;
            }

            //va a leer según la extensión, si llega a fallar se ejecuta el catch



            // formato para mostrar los datos ingresados

            StringBuilder tabla = new StringBuilder();

            tabla.AppendLine("=======================================================================");
            tabla.AppendLine("| Legajo | Apellido | Nombres | Nro. Doc. | Email | Teléfono |");
            tabla.AppendLine("=======================================================================");

            foreach (var a in alumnos)
            {
                tabla.AppendLine($"| {a.Legajo} | {a.Apellido} | {a.Nombres} | {a.NumeroDocumento} | {a.Email} | {a.Telefono} |");
            }
            //el foreach va a agregar una fila por alumno

            tabla.AppendLine("=======================================================================");
            tabla.AppendLine($"Total de alumnos: {alumnos.Count}");

            return tabla.ToString();
        }




        //_______________________________________________Métodos para leer los distintos tipos de archivos 

        private static List<Alumno> LeerTXT(string ruta)
        {
            List<Alumno> lista = new List<Alumno>();

            foreach (string linea in File.ReadAllLines(ruta))      //me lee todas las líneas
            {
                string[] partes = linea.Split('|');     //separa las cosas por |
                if (partes.Length == 6)     //si se cumple la condición me crea el alumno
                {
                    lista.Add(new Alumno
                    {
                        Legajo = partes[0],
                        Apellido = partes[1],
                        Nombres = partes[2],
                        NumeroDocumento = partes[3],
                        Email = partes[4],
                        Telefono = partes[5]
                    });
                }
            }

            return lista;
        }

        private static List<Alumno> LeerCSV(string ruta)
        {
            List<Alumno> lista = new List<Alumno>();
            string[] lineas = File.ReadAllLines(ruta);

            // arranco de uno, la línea 0 es el encabezado
            for (int i = 1; i < lineas.Length; i++)
            {
                string[] partes = lineas[i].Split(',');

                if (partes.Length == 6)
                {
                    lista.Add(new Alumno
                    {
                        Legajo = partes[0],
                        Apellido = partes[1],
                        Nombres = partes[2],
                        NumeroDocumento = partes[3],
                        Email = partes[4],
                        Telefono = partes[5]
                    });
                }
            }

            return lista;
        }




        private static List<Alumno> LeerJSON(string ruta)
        {
            string contenido = File.ReadAllText(ruta);
            return JsonSerializer.Deserialize<List<Alumno>>(contenido); //devuelve una lista, si el JSON está vacío devuelve null
        }


        private static List<Alumno> LeerXML(string ruta)
        {
            List<Alumno> lista = new List<Alumno>();

            XElement raiz = XElement.Load(ruta);  //XElement abre el archivo XML, ruta dónde está el archivo

            foreach (var nodo in raiz.Elements("Alumno"))   //recorre los alumnos
            {
                lista.Add(new Alumno
                {
                    Legajo = nodo.Element("Legajo")?.Value,
                    Apellido = nodo.Element("Apellido")?.Value,
                    Nombres = nodo.Element("Nombres")?.Value,
                    NumeroDocumento = nodo.Element("NumeroDocumento")?.Value,
                    Email = nodo.Element("Email")?.Value,
                    Telefono = nodo.Element("Telefono")?.Value
                });
            }

            return lista;
        }


        //______________________________________________________________________________________________


        //SUB-MENÚ 3: MODIFICAR ARCHIVO EXISTENTE


        public static void OpcionModificarArchivo(Form owner, string rutaArchivo)
        {
            if (!File.Exists(rutaArchivo))
            {
                MessageBox.Show("El archivo no existe.");
                return;
            }


            // Detectar formato según la extensión del archivo

            string extension = Path.GetExtension(rutaArchivo).ToLower();
            List<Alumno> alumnos;

            try
            {
                switch (extension)
                {
                    case ".txt":
                        alumnos = LeerTXT(rutaArchivo);
                        break;
                    case ".csv":
                        alumnos = LeerCSV(rutaArchivo);
                        break;
                    case ".json":
                        alumnos = LeerJSON(rutaArchivo);
                        break;
                    case ".xml":
                        alumnos = LeerXML(rutaArchivo);
                        break;
                    default:
                        MessageBox.Show("Formato no soportado para modificar.");
                        return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al leer el archivo: " + ex.Message);
                return;
            }

            bool salir = false;
            bool guardado = false;

            while (!salir)
            {
                string menu = "=== OPCIONES DE MODIFICACIÓN ===\n" +
                              "1. Agregar nuevo alumno\n" +
                              "2. Modificar alumno existente (por legajo)\n" +
                              "3. Eliminar alumno (por legajo)\n" +
                              "4. Guardar y salir\n" +
                              "5. Cancelar sin guardar\n\n" +
                              "Ingrese una opción (1-5):";

                string opcion = Prompt(menu);

                switch (opcion)
                {
                    case "1":
                        AgregarNuevoAlumno(alumnos);
                        break;

                    case "2":
                        ModificarAlumnoPorLegajo(alumnos);
                        break;

                    case "3":
                        EliminarAlumnoPorLegajo(alumnos);
                        break;

                    case "4":
                        CrearBackupYGuardar(rutaArchivo, extension, alumnos);
                        guardado = true;
                        salir = true;
                        break;

                    case "5":
                        salir = true;
                        break;

                    default:
                        MessageBox.Show("Opción inválida.");
                        break;
                }
            }

            if (!guardado)
            {
                MessageBox.Show("Cambios descartados. No se guardó el archivo.");
            }
        }

        //_____________________________________________________funciones del menú dentro del sub-menú 3

        private static void AgregarNuevoAlumno(List<Alumno> alumnos)
        {
            MessageBox.Show("=== AGREGAR NUEVO ALUMNO ===");

            // hago una validación para que no haya un legajo igual
            // ✅ CAMBIO: llamada unificada de legajo (AGREGAR)
            string legajo = PedirLegajoParaAgregar(alumnos, "Legajo (nuevo)");
            if (legajo == null) return;

            if (BuscarPorLegajo(alumnos, legajo) != null)   //si existe el legajo, pide otro
            {
                MessageBox.Show("Ya existe un alumno con ese legajo. Ingrese otro.");
                return;
            }

            Alumno alu = new Alumno();
            alu.Legajo = legajo;
            alu.Apellido = PedirNoVacio("Apellido");
            if (alu.Apellido == null) return;

            alu.Nombres = PedirNoVacio("Nombres");
            if (alu.Nombres == null) return;

            alu.NumeroDocumento = PedirNoVacio("Número de Documento");
            if (alu.NumeroDocumento == null) return;

            alu.Email = PedirNoVacio("Email");
            if (alu.Email == null) return;

            if (!alu.Email.Contains("@"))       //valido que el mail lleve @
            {
                MessageBox.Show("Email inválido. No se agrega el alumno.");
                return;
            }

            alu.Telefono = PedirNoVacio("Teléfono");
            if (alu.Telefono == null) return;

            alumnos.Add(alu);
            MessageBox.Show("Alumno agregado correctamente.");
        }



        private static void ModificarAlumnoPorLegajo(List<Alumno> alumnos)
        {
            MessageBox.Show("=== MODIFICAR ALUMNO ===");

            // ✅ CAMBIO: llamada unificada de legajo (MODIFICAR)
            Alumno alu = PedirAlumnoParaModificar(alumnos, "Legajo del alumno a modificar");
            if (alu == null) return;

            // Mostrar los datos que se tienen antes de editarlos
            MostrarAlumno(alu);

            // Para cada campo: Enter deja el valor actual
            string nuevo;

            nuevo = Interaction.InputBox(
                $"Apellido actual: {alu.Apellido}\nIngrese nuevo Apellido (Enter para dejar igual):",
                "Modificar alumno");
            if (!string.IsNullOrWhiteSpace(nuevo))
                alu.Apellido = nuevo;

            nuevo = Interaction.InputBox(
                $"Nombres actuales: {alu.Nombres}\nIngrese nuevos Nombres (Enter para dejar igual):",
                "Modificar alumno");
            if (!string.IsNullOrWhiteSpace(nuevo))
                alu.Nombres = nuevo;

            nuevo = Interaction.InputBox(
                $"Documento actual: {alu.NumeroDocumento}\nIngrese nuevo Documento (Enter para dejar igual):",
                "Modificar alumno");
            if (!string.IsNullOrWhiteSpace(nuevo))
                alu.NumeroDocumento = nuevo;

            nuevo = Interaction.InputBox(
                $"Email actual: {alu.Email}\nIngrese nuevo Email (Enter para dejar igual):",
                "Modificar alumno");
            if (!string.IsNullOrWhiteSpace(nuevo))
            {
                if (!nuevo.Contains("@"))
                {
                    MessageBox.Show("Email inválido. Se mantiene el anterior.");
                }
                else
                {
                    alu.Email = nuevo;
                }
            }

            nuevo = Interaction.InputBox(
                $"Teléfono actual: {alu.Telefono}\nIngrese nuevo Teléfono (Enter para dejar igual):",
                "Modificar alumno");
            if (!string.IsNullOrWhiteSpace(nuevo))
                alu.Telefono = nuevo;

            MessageBox.Show("Alumno modificado en memoria.");
        }

        private static void EliminarAlumnoPorLegajo(List<Alumno> alumnos)
        {
            MessageBox.Show("=== ELIMINAR ALUMNO ===");

            // ✅ CAMBIO: llamada unificada de legajo (ELIMINAR usa la misma que MODIFICAR)
            Alumno alu = PedirAlumnoParaModificar(alumnos, "Legajo del alumno a eliminar");
            if (alu == null) return;

            // Mostrar datos y pedir confirmación
            MostrarAlumno(alu);
            DialogResult r = MessageBox.Show(
                "¿Confirma eliminar este alumno?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (r == DialogResult.Yes)
            {
                alumnos.Remove(alu);
                MessageBox.Show("Alumno eliminado de la lista en memoria.");
            }
            else
            {
                MessageBox.Show("Operación cancelada. No se eliminó el alumno.");
            }
        }

        private static void CrearBackupYGuardar(string rutaArchivo, string extension, List<Alumno> alumnos)
        {
            try
            {
                // Backup: renombrar con sufijo .bak
                string rutaBackup = rutaArchivo + ".bak";
                if (File.Exists(rutaBackup))
                {
                    File.Delete(rutaBackup);    //evito error si existe un backup
                }
                File.Move(rutaArchivo, rutaBackup);

                // Guardo manteniendo el formato original
                switch (extension)
                {
                    case ".txt":
                        GuardarTXT(rutaArchivo, alumnos);
                        break;
                    case ".csv":
                        GuardarCSV(rutaArchivo, alumnos);
                        break;
                    case ".json":
                        GuardarJSON(rutaArchivo, alumnos);
                        break;
                    case ".xml":
                        GuardarXML(rutaArchivo, alumnos);
                        break;
                }

                MessageBox.Show("Cambios guardados correctamente.\nSe creó un backup: " + rutaBackup);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar los cambios: " + ex.Message);
            }
        }

        //FUNCIONES EXTRAS

        private static Alumno BuscarPorLegajo(List<Alumno> alumnos, string legajo)
        {
            foreach (var a in alumnos)
            {
                if (a.Legajo == legajo)
                    return a;
            }
            return null;
        }


        //___________________________________ modificado
        private static string PedirLegajoParaAgregar(List<Alumno> alumnos, string mensaje)
        {
            while (true)
            {
                string legajo = PedirNoVacio(mensaje);
                if (legajo == null) return null;

                legajo = legajo.Trim();

                if (BuscarPorLegajo(alumnos, legajo) != null)
                {
                    MessageBox.Show("Ya existe un alumno con ese legajo. Ingrese otro.");
                    continue;
                }

                return legajo;
            }
        }

        // 2) FUNCIÓN ÚNICA para MODIFICAR (y también sirve para ELIMINAR): obliga a que exista
        private static Alumno PedirAlumnoParaModificar(List<Alumno> alumnos, string mensaje)
        {
            while (true)
            {
                string legajo = PedirNoVacio(mensaje);
                if (legajo == null) return null;

                legajo = legajo.Trim();

                Alumno alu = BuscarPorLegajo(alumnos, legajo);
                if (alu == null)
                {
                    MessageBox.Show("No se encontró un alumno con ese legajo. Ingrese uno existente.");
                    continue;
                }

                return alu;
            }
        }

        //______________________________________________________














        private static void MostrarAlumno(Alumno a)
        {
            string datos = $"Legajo: {a.Legajo}\n" +
                           $"Apellido: {a.Apellido}\n" +
                           $"Nombres: {a.Nombres}\n" +
                           $"Documento: {a.NumeroDocumento}\n" +
                           $"Email: {a.Email}\n" +
                           $"Teléfono: {a.Telefono}";

            MessageBox.Show(datos, "Datos del alumno");
        }


        //_________________________________________________________________________________________________


        //SUB-MENÚ 4: ELIMINAR ARCHIVO

        public static void OpcionEliminarArchivo(Form owner, string rutaArchivo)
        {
            // 1. Verificar existencia
            if (!File.Exists(rutaArchivo))  //si no existe el archivo me va a mostrar el MessageBox
            {
                MessageBox.Show("El archivo no existe.");
                return;
            }

            // 2. Obtener información del archivo
            FileInfo info = new FileInfo(rutaArchivo);

            string detalles =
                "=== INFORMACIÓN DEL ARCHIVO ===\n\n" +
                $"Nombre completo: {info.FullName}\n" +     //$: sirve para poder meter variables o expresiones dentro de un texto
                $"Tamaño: {info.Length / 1024.0:F2} KB\n" +
                $"Fecha de creación: {info.CreationTime}\n" +
                $"Última modificación: {info.LastWriteTime}\n\n" +
                "Para eliminar este archivo, escriba: CONFIRMAR";

            // 3. Pedir confirmación
            string confirmacion = Prompt(detalles); //crea la variable confirmación que guarda todo el texto almacenado en detalles

            if (confirmacion == null || confirmacion.Trim().ToUpper() != "CONFIRMAR") //si esta vacío o es distinto a la palabra "confirmar" se cancela
            {
                MessageBox.Show("Operación cancelada. No se eliminó el archivo."); //se ejecuta si la condición no se cumple
                return;
            }

            // 4. Eliminar el archivo
            try
            {
                File.Delete(rutaArchivo);   //delete es un método de la clase file que elimina el archivo del sistema
                MessageBox.Show("Archivo eliminado correctamente.");
            }
            catch (Exception ex)    //exception es una clase, representa un error en tiempo de ejecución y lo almacena en la variable ex
            {
                MessageBox.Show("Error al intentar eliminar el archivo: " + ex.Message);    //message propiedad de exception, muestra el error en texto
            }
        }


        //__________________________________________________________________________________________________


        //SUB-MENÚ 5: CONVERTIR ENTRE FORMATOS

        public static void OpcionConvertirEntreFormatos(Form owner, string rutaArchivo)
        {
            // verifico existencia
            if (!File.Exists(rutaArchivo))
            {
                MessageBox.Show("El archivo de origen no existe.");
                return;
            }

            // 2) Detectar formato de origen por extensión
            string extOrigen = Path.GetExtension(rutaArchivo).ToLower(); //obtengo extensión del archivo y lo guardo en la var. extOrigen
            string formatoOrigen;


            //este switch se encarga de asignarle la extensión correspondiente a la var. formatoOrigen, si no es válido sale del método
            switch (extOrigen)
            {
                case ".txt":
                    formatoOrigen = "TXT";
                    break;
                case ".csv":
                    formatoOrigen = "CSV";
                    break;
                case ".json":
                    formatoOrigen = "JSON";
                    break;
                case ".xml":
                    formatoOrigen = "XML";
                    break;
                default:
                    MessageBox.Show("Formato de origen no soportado.");
                    return;
            }


            string menu = $"Formato actual: {formatoOrigen}\n" +
                          "Seleccione formato de destino:\n";


            //estos if van a mostrar todos los formatos disponibles menos el formato actual
            if (formatoOrigen != "TXT") menu += "1. TXT\n";
            if (formatoOrigen != "CSV") menu += "2. CSV\n";
            if (formatoOrigen != "JSON") menu += "3. JSON\n";
            if (formatoOrigen != "XML") menu += "4. XML\n";


            menu += "\nIngrese la opción de destino:";      //le agregue una linea de texto más al texto que contiene el menú

            string opcionDestino = Prompt(menu);    //me muestra el menú en un inputbox y lo guarda en la var. opcionDestino 

            string formatoDestino = ""; //var. vacía 
            string extDestino = ""; //var. vacía. Guarda la extensión real (como .txt)


            //este switch sirve para validar que la extensión ingresada no sea igual a la de origen
            switch (opcionDestino)
            {
                case "1":
                    if (formatoOrigen == "TXT")
                    {
                        MessageBox.Show("Ya está en TXT. Elija otra opción.");
                        return;
                    }
                    formatoDestino = "TXT";
                    extDestino = ".txt";
                    break;

                case "2":
                    if (formatoOrigen == "CSV")
                    {
                        MessageBox.Show("Ya está en CSV. Elija otra opción.");
                        return;
                    }
                    formatoDestino = "CSV";
                    extDestino = ".csv";
                    break;

                case "3":
                    if (formatoOrigen == "JSON")
                    {
                        MessageBox.Show("Ya está en JSON. Elija otra opción.");
                        return;
                    }
                    formatoDestino = "JSON";
                    extDestino = ".json";
                    break;

                case "4":
                    if (formatoOrigen == "XML")
                    {
                        MessageBox.Show("Ya está en XML. Elija otra opción.");
                        return;
                    }
                    formatoDestino = "XML";
                    extDestino = ".xml";
                    break;

                default:
                    MessageBox.Show("Opción de formato destino inválida.");
                    return;
            }


            string nombreDestino = Prompt("Ingrese el nombre del archivo de destino (sin extensión):");
            if (string.IsNullOrWhiteSpace(nombreDestino))   //valido que el nombre ingresado sea válido
            {
                MessageBox.Show("Nombre de archivo destino inválido.");
                return;
            }


            string carpetaOrigen = Path.GetDirectoryName(rutaArchivo); //busca la carpeta directorio donde esta el archivo original y lo guarda en carpetaOrigen
            string rutaDestino = Path.Combine(carpetaOrigen, nombreDestino + extDestino); //construye la ruta completa del archivo


            List<Alumno> alumnos; //lista que guarda elementos del tipo alumno. Alumnos es la variable

            try
            {
                switch (formatoOrigen)
                {
                    case "TXT":
                        alumnos = LeerTXT(rutaArchivo);
                        break;
                    case "CSV":
                        alumnos = LeerCSV(rutaArchivo);
                        break;
                    case "JSON":
                        alumnos = LeerJSON(rutaArchivo);
                        break;
                    case "XML":
                        alumnos = LeerXML(rutaArchivo);
                        break;
                    default:
                        MessageBox.Show("Formato de origen no soportado.");
                        return;

                        //cada case llama al método, lee contenido, crea objeto Alumno, devuelve una lista y esa lista se guarda en alumnos 
                }
            }
            catch (Exception ex)    //si algo falla se va a ejecutar el catch
            {
                MessageBox.Show("Error al leer el archivo de origen: " + ex.Message);
                return;
            }


            //este try va a intentar guardar el archivo ya convertido
            try
            {
                switch (formatoDestino)     //va a elegir como guardarlo según el formatoDestino
                {
                    case "TXT":
                        GuardarTXT(rutaDestino, alumnos);
                        break;
                    case "CSV":
                        GuardarCSV(rutaDestino, alumnos);
                        break;
                    case "JSON":
                        GuardarJSON(rutaDestino, alumnos);
                        break;
                    case "XML":
                        GuardarXML(rutaDestino, alumnos);
                        break;
                }
            }
            catch (Exception ex)    //este catch se ejecuta en caso de error cancelando la conversión
            {
                MessageBox.Show("Error al guardar el archivo de destino: " + ex.Message);
                return;
            }

            // 8) Mostrar resumen de la conversión
            string resumen =
                "Conversión exitosa.\n\n" +
                $"Archivo origen: {Path.GetFileName(rutaArchivo)} ({alumnos.Count} registros)\n" +
                $"Archivo destino: {Path.GetFileName(rutaDestino)} ({alumnos.Count} registros)";

            MessageBox.Show(resumen);

            //alumnos.count: cantidad de registros convertidos
        }


        //__________________________________________________________________________________________________


        //SUB-MENÚ 6: CREAR REPORTE CON CORTE DE CONTROL DE UN NIVEL

        public static string OpcionCrearReporte(Form owner, string rutaArchivo)
        {

            if (!File.Exists(rutaArchivo))      //verifico que el archivo exista
            {
                return "El archivo no existe.";
            }

            // 2) Detectar formato por extensión
            string extension = Path.GetExtension(rutaArchivo).ToLower();    //obtiene la extensión del archivo (rutaArchivo) y la guarda en extension
            List<Alumno> alumnos;   //variable donde se va a guardar la lista alumnos

            try
            {
                switch (extension)  //elige que método usar para leer el archivo según extensión 
                {
                    case ".txt":
                        alumnos = LeerTXT(rutaArchivo);
                        break;
                    case ".csv":
                        alumnos = LeerCSV(rutaArchivo);
                        break;
                    case ".json":
                        alumnos = LeerJSON(rutaArchivo);
                        break;
                    case ".xml":
                        alumnos = LeerXML(rutaArchivo);
                        break;
                    default:
                        return "Formato de archivo no soportado para reporte.";
                }
            }
            catch (Exception ex)    //se ejecuta si hay algún error en el try
            {
                return "Error al leer el archivo: " + ex.Message;
            }

            if (alumnos == null || alumnos.Count == 0) //verifica que alumnos no sea nulo o 0
            {
                return "El archivo no contiene alumnos para generar el reporte.";
            }


            var grupos = alumnos
                .OrderBy(a => a.Apellido)   //OrderBy método de linq, ordena colecciones
                .ThenBy(a => a.Nombres) //ThenBy método de linq, es usado cuando el OrderBy es igual
                .GroupBy(a => a.Apellido); //GroupBy método de linq, lo usa para agrupar alfabéticamente

            int totalAlumnos = alumnos.Count;   //cuenta la cantidad total de alumnos
            int totalApellidos = grupos.Count();    //cuenta cuantos apellidos distintos hay 


            StringBuilder sb = new StringBuilder(); //stringbuilder: clase de system.text. Usado para construir textos largos

            string lineaGrande = new string('=', 70);   //el primer parámetro es lo que se escribe, el segundo la cantidad de veces
            string lineaCorta = new string('-', 70);    //lo mismo que arriba

            sb.AppendLine(lineaGrande);
            sb.AppendLine("REPORTE DE ALUMNOS POR APELLIDO");
            sb.AppendLine("Fecha: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            sb.AppendLine(lineaGrande);
            sb.AppendLine();

            foreach (var grupo in grupos) //recorre cada grupo creado por groupby
            {
                string apellido = grupo.Key;    //grupo.Key valor por el cual se agrupa (apellido)

                sb.AppendLine($"APELLIDO: {apellido.ToUpper()}");
                sb.AppendLine(lineaCorta);
                sb.AppendLine();

                foreach (var a in grupo)    //recorre todos los alumnos que tengan el apellido, y agrega los datos completos de cada alumno
                {
                    sb.AppendLine($"Legajo: {a.Legajo}");
                    sb.AppendLine($"Nombres: {a.Nombres}");
                    sb.AppendLine($"Documento: {a.NumeroDocumento}");
                    sb.AppendLine($"Email: {a.Email}");
                    sb.AppendLine($"Teléfono: {a.Telefono}");
                    sb.AppendLine();
                }

                sb.AppendLine($"Subtotal {apellido.ToUpper()}: {grupo.Count()} alumno(s)"); //cuenta la cantidad de alumnos con ese apellido
                sb.AppendLine();    //salto de línea
            }

            sb.AppendLine(lineaGrande);
            sb.AppendLine("RESUMEN GENERAL");
            sb.AppendLine(lineaGrande);
            sb.AppendLine($"Total de Apellidos diferentes: {totalApellidos}");
            sb.AppendLine($"Total de Alumnos registrados: {totalAlumnos}");
            sb.AppendLine(lineaGrande);

            string reporte = sb.ToString(); //convierte lo almacenado en stringbuilder (sb) en un un string que se almacena en la var. reporte



            string resp = Prompt("¿Desea guardar el reporte en un archivo TXT? (S/N)"); //guarda la respuesta en la var. resp

            if (!string.IsNullOrWhiteSpace(resp) && resp.Trim().ToUpper() == "S")   //entra al if solo si el usuario escribio "s" o si no es nulo
            {
                string carpeta = Path.GetDirectoryName(rutaArchivo);    //obtiene la carpeta del archivo original

                if (string.IsNullOrEmpty(carpeta))  //si no hay carpeta usa la del programa
                {
                    carpeta = AppDomain.CurrentDomain.BaseDirectory;
                }

                string nombreReporte = Prompt("Ingrese el nombre del archivo de reporte (sin extensión):");

                if (string.IsNullOrWhiteSpace(nombreReporte))   //si no escribe el nombre usa uno cualquiera
                {
                    nombreReporte = "ReporteAlumnosPorApellido";
                }

                string rutaReporte = Path.Combine(carpeta, nombreReporte + ".txt"); //arma la ruta

                try
                {
                    File.WriteAllText(rutaReporte, reporte, Encoding.UTF8); //crea o sobreescribe un archivo de texto y escribe dentro el cont. del reporte
                    MessageBox.Show("Reporte guardado correctamente en:\n" + rutaReporte);
                }
                catch (Exception ex)    //muestra el error y el motivo del error
                {
                    MessageBox.Show("No se pudo guardar el reporte: " + ex.Message);
                }
            }

            return reporte; //devuelve el texto del reporte para mostrarlo en txtSalida
        }
    }
}


















































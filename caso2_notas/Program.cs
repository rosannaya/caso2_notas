using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace caso2_notas
{
    internal class Program
    {
        // ==========================================
        // 2. PROGRAMA PRINCIPAL
        // ==========================================
          // 4.3 Arreglos paralelos y contador de control
            static int MAX_ALUMNOS = 30;
            static string[] nombresAlumnos = new string[MAX_ALUMNOS];
            static double[] notasAlumnos = new double[MAX_ALUMNOS];
            static int totalAlumnos = 0;

            static string rutaArchivo = "registro_notas.txt";

            static void RegistrarEstudiante()
            {
                Console.WriteLine("\n--- REGISTRAR ESTUDIANTE ---");
                if (totalAlumnos >= MAX_ALUMNOS)
                {
                    Console.WriteLine("❌ Capacidad máxima alcanzada.");
                    return;
                }

                // Uso de la biblioteca modular de validación
                string nombre = Validador.LeerTextoNoVacio("Ingrese Nombre y Apellido: ").ToUpper();
                double nota = Validador.LeerDecimal("Ingrese la Nota final (0.0 a 20.0): ", 0.0, 20.0);

                // Almacenamiento en arreglos paralelos
                nombresAlumnos[totalAlumnos] = nombre;
                notasAlumnos[totalAlumnos] = nota;
                totalAlumnos++;

                Console.WriteLine("✅ ¡Estudiante registrado correctamente en memoria!");
            }

            static void GuardarEnArchivo()
            {
                // 5.2 y 5.3 Escritura estructurada con StreamWriter
                Console.WriteLine("\n--- GUARDAR NOTAS EN ARCHIVO (.TXT) ---");
                if (totalAlumnos == 0)
                {
                    Console.WriteLine("⚠️ No hay datos para guardar.");
                    return;
                }

                using (StreamWriter escritor = new StreamWriter(rutaArchivo))
                {
                    for (int i = 0; i < totalAlumnos; i++)
                    {
                        // Formato CSV: Nombre,Nota
                        escritor.WriteLine($"{nombresAlumnos[i]},{notasAlumnos[i]}");
                    }
                }
                Console.WriteLine($"✅ ¡{totalAlumnos} registros guardados en '{rutaArchivo}'!");
            }

            static void CargarDesdeArchivo()
            {
                // 5.2 y 5.4 Lectura con StreamReader y sincronización de memoria
                Console.WriteLine("\n--- CARGAR NOTAS DESDE ARCHIVO A MEMORIA ---");
                if (!File.Exists(rutaArchivo))
                {
                    Console.WriteLine("⚠️ No existe un archivo guardado previamente.");
                    return;
                }

                totalAlumnos = 0; // Reiniciamos contador

                using (StreamReader lector = new StreamReader(rutaArchivo))
                {
                    string linea;
                    while ((linea = lector.ReadLine()) != null && totalAlumnos < MAX_ALUMNOS)
                    {
                        string[] partes = linea.Split(',');
                        if (partes.Length == 2)
                        {
                            nombresAlumnos[totalAlumnos] = partes[0];
                            notasAlumnos[totalAlumnos] = Convert.ToDouble(partes[1]);
                            totalAlumnos++;
                        }
                    }
                }
                Console.WriteLine($"✅ ¡Se cargaron {totalAlumnos} estudiantes del archivo a la memoria!");
            }

            static void BuscarEstudiante()
            {
                // 4.4 Búsqueda textual utilizando Contains
                Console.WriteLine("\n--- BÚSQUEDA DE ESTUDIANTE ---");
                if (totalAlumnos == 0)
                {
                    Console.WriteLine("⚠️ No hay alumnos registrados.");
                    return;
                }

                string filtro = Validador.LeerTextoNoVacio("Ingrese parte del nombre a buscar: ").ToUpper();
                bool encontrado = false;

                for (int i = 0; i < totalAlumnos; i++)
                {
                    if (nombresAlumnos[i].Contains(filtro))
                    {
                        string estado = notasAlumnos[i] >= 11.0 ? "APROBADO 🎉" : "DESAPROBADO ❌";
                        Console.WriteLine($"🔍 Encontrado -> Alumno: {nombresAlumnos[i]} | Nota: {notasAlumnos[i]:F1} | Estado: {estado}");
                        encontrado = true;
                    }
                }

                if (!encontrado)
                {
                    Console.WriteLine("❌ No se encontraron coincidencias.");
                }
            }

            static void MostrarListadoGeneral()
            {
                Console.WriteLine("\n" + new string('=', 60));
                Console.WriteLine("                 LISTADO GENERAL DE NOTAS");
                Console.WriteLine(new string('=', 60));

                if (totalAlumnos == 0)
                {
                    Console.WriteLine("⚠️ No hay registros.");
                    Console.WriteLine(new string('=', 60));
                    return;
                }

                double suma = 0;
                for (int i = 0; i < totalAlumnos; i++)
                {
                    string estado = notasAlumnos[i] >= 11.0 ? "APROBADO" : "DESAPROBADO";
                    Console.WriteLine($"{i + 1}. {nombresAlumnos[i]} - Nota: {notasAlumnos[i]:F1} ({estado})");
                    suma += notasAlumnos[i];
                }

                double promedio = suma / totalAlumnos;
                Console.WriteLine(new string('-', 60));
                Console.WriteLine($"📊 Promedio General del Grupo: {promedio:F2}");
                Console.WriteLine(new string('=', 60));
            }

            static void Main(string[] args)
            {
                int opcion = 0;
                while (opcion != 6)
                {
                    Console.WriteLine("\n--- SISTEMA DE GESTIÓN DE NOTAS ---");
                    Console.WriteLine("1. Registrar estudiante (en memoria)");
                    Console.WriteLine("2. Guardar notas en archivo (.txt)");
                    Console.WriteLine("3. Cargar notas desde archivo a memoria");
                    Console.WriteLine("4. Buscar estudiante por texto");
                    Console.WriteLine("5. Mostrar listado general y promedio");
                    Console.WriteLine("6. Salir");

                    opcion = Validador.LeerEntero("Seleccione una opción (1-6): ", 1, 6);

                    switch (opcion)
                    {
                        case 1: RegistrarEstudiante(); break;
                        case 2: GuardarEnArchivo(); break;
                        case 3: CargarDesdeArchivo(); break;
                        case 4: BuscarEstudiante(); break;
                        case 5: MostrarListadoGeneral(); break;
                        case 6: Console.WriteLine("\n¡Muchos éxitos en tu examen!"); break;
                    }
                }
            }

        }
    }



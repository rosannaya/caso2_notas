using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace caso2_notas
{

    // ==========================================
    // 1. BIBLIOTECA MODULAR DE VALIDACIONES
    // ==========================================
    public static class Validador
    {
        public static int LeerEntero(string mensaje, int? minVal = null, int? maxVal = null)
        {
            int valor;
            while (true)
            {
                Console.Write(mensaje);
                if (int.TryParse(Console.ReadLine(), out valor))
                {
                    if (minVal.HasValue && valor < minVal.Value)
                    {
                        Console.WriteLine($"❌ Error: Debe ser mayor o igual a {minVal.Value}.");
                        continue;
                    }
                    if (maxVal.HasValue && valor > maxVal.Value)
                    {
                        Console.WriteLine($"❌ Error: Debe ser menor o igual a {maxVal.Value}.");
                        continue;
                    }
                    return valor;
                }
                Console.WriteLine("❌ Error: Ingrese un número entero válido.");
            }
        }

        public static double LeerDecimal(string mensaje, double? minVal = null, double? maxVal = null)
        {
            double valor;
            while (true)
            {
                Console.Write(mensaje);
                if (double.TryParse(Console.ReadLine(), out valor))
                {
                    if (minVal.HasValue && valor < minVal.Value)
                    {
                        Console.WriteLine($"❌ Error: La nota mínima es {minVal.Value}.");
                        continue;
                    }
                    if (maxVal.HasValue && valor > maxVal.Value)
                    {
                        Console.WriteLine($"❌ Error: La nota máxima es {maxVal.Value}.");
                        continue;
                    }
                    return valor;
                }
                Console.WriteLine("❌ Error: Ingrese un valor numérico válido.");
            }
        }

        public static string LeerTextoNoVacio(string mensaje)
        {
            while (true)
            {
                Console.Write(mensaje);
                string texto = Console.ReadLine()?.Trim();
                if (!string.IsNullOrEmpty(texto))
                {
                    return texto;
                }
                Console.WriteLine("❌ Error: Este campo no puede estar vacío.");
            }
        }
    }


}
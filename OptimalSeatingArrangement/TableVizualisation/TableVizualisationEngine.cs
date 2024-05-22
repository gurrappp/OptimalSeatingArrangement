using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTableExt;

namespace OptimalSeatingArrangement.TableVizualisation
{
    internal class TableVisualizationEngine
    {
        public static void ShowTable<T>(List<T> tableData, [AllowNull] string tableName, [AllowNull] string seatingPoints) where T : class
        {
            Console.Clear();
            //if (tableName == null)
            //    tableName = "";

            //if (seatingPoints == null)
            //    seatingPoints = "";

            Console.WriteLine("\n\n");

            ConsoleTableBuilder
                .From(tableData)
                .WithColumn(new List<string>{tableName ?? "",seatingPoints ?? "" })
                .ExportAndWriteLine();
            Console.WriteLine("\n\n");

        }

        public static void ShowBestTable<T>(List<T> tableData, [AllowNull] List<string> columns) where T : class
        {
            Console.Clear();
            
            Console.WriteLine("\n\n");

            ConsoleTableBuilder
                .From(tableData)
                .WithColumn(columns)
                .ExportAndWriteLine();
            Console.WriteLine("\n\n");

        }
    }
}

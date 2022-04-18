using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Code_Translator
{
    internal class Program
    {
        // TODO: write unit test for all operation parser and operation handler and invocation parser
        // TODO: add doc to InfoType and everything
        
        // TODO: improve condition operation in handling &&, ||
        // TODO: make condition break, continue, goto and function calls
        // TODO: add support for custom enum
        // TODO: add set color control
        // TODO: add support for foreach loop
        // TODO: add cellArray and cellStack
        // TODO: add support for switch

        [STAThread]
        static void Main(string[] args)
        {
            //for copying icon
            /*Task.Run(async () => {
                StringBuilder builder1 = new StringBuilder();
                var items = await Windows.ApplicationModel.DataTransfer.Clipboard.GetHistoryItemsAsync();
                foreach (var item in items.Items)
                {
                    string data = await item.Content.GetTextAsync();
                    if (data.Contains("end"))
                        break;
                    var bs = Encoding.BigEndianUnicode.GetBytes(data);
                    for (int i = 0; i < bs.Length; i += 2)
                    {
                        builder1.Append("0x");
                        builder1.Append(((bs[i] << 8) + bs[i + 1]).ToString("x"));
                        builder1.AppendLine();
                    }
                    Console.Write(builder1);
                    builder1.Clear();
                }
            });
            Console.ReadLine();
            return;*/

            try
            {
#if DEBUG
                string source = File.ReadAllText(Path.GetFullPath(@"..\..\..\..\MindustryLogics\Test.cs"));
#else
                Console.WriteLine("Select a file you want to compile...");
                string source = GetFile(Path.GetFullPath(@"..\..\..\"));
#endif
                Console.WriteLine("Original Code:");
                Console.WriteLine(source);
                Console.WriteLine();
                Console.Write("Analysing syntax...");
                Translator compiler = new Translator(source);
                Console.WriteLine("finished.");
                Console.Write("Compiling...");
                string compiled = compiler.Compile();
                Console.WriteLine("finished.");
                Console.WriteLine("Compiled Code:");
                StringBuilder builder = new StringBuilder();
                int i = 0;
                foreach (var line in compiled.AsSpan().EnumerateLines())
                {
                    if (line.IsWhiteSpace()) continue;
                    builder.Append($"{i, 4}: ");
                    builder.Append(line);
                    builder.AppendLine();
                    i++;
                }
                Console.Write(builder);
                Clipboard.SetText(compiled);
                Console.WriteLine("Code copied to clipboard.");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("File selection canceled.");
            }
            catch (CompilationException e)
            {
                Console.WriteLine();
#if DEBUG
                Console.WriteLine(e);
#else
                Console.WriteLine(e.Message);
#endif
            }

            Console.Write("Press any key to exit...");
            Console.ReadKey();
        }

        private static string GetFile(string initPath)
        {
            using OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = initPath;
            openFileDialog.Filter = "C# Source File (*.cs)|*.cs";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var fileStream = openFileDialog.OpenFile();
                using StreamReader reader = new StreamReader(fileStream);
                return reader.ReadToEnd();
            }

            throw new OperationCanceledException();
        }
    }
}

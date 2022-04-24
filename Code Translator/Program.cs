using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Code_Translator
{
    internal class Program
    {
        // TODO: write unit test for all operation parser and operation handler and invocation parser
        // TODO: make tutorial and add doc to readme.md
        
        // TODO: make condition break, continue, goto and function calls
        // TODO: add support for custom enum
        // TODO: add set color control
        // TODO: add support for foreach loop
        // TODO: add cellArray and cellStack
        // TODO: add support for switch statement
        // TODO: Change the compiler variable name
        // TODO: add LinkRef

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

#if DEBUG
            string filePath = Path.GetFullPath(@"..\..\..\..\MindustryLogics\Test.cs");
#else
            Console.WriteLine("Drop the .cs file that you want to compile to this window and then press enter.");
            string filePath = Console.ReadLine().Trim('"');
#endif

            Console.Write("Initializing...");
            Translator translator = new Translator();
            Console.WriteLine("finished.");
            while (true)
            {
                try
                {
                    string source = File.ReadAllText(filePath);
                    Console.WriteLine("Original Code:");
                    Console.WriteLine(source);
                    Console.WriteLine();
                    Console.Write("Analysing syntax...");
                    translator.SetSource(source);
                    Console.WriteLine("finished.");
                    Console.Write("Checking code validity...");
                    if (!translator.CheckCodeValidity())
                        throw new OperationCanceledException();
                    Console.WriteLine("finished.");
                    Console.Write("Translating...");
                    string translated = translator.Translate();
                    Console.WriteLine("finished.");
                    Console.WriteLine("Translated Code:");
                    StringBuilder builder = new StringBuilder();
                    int i = 0;
                    foreach (var line in translated.AsSpan().EnumerateLines())
                    {
                        if (line.IsWhiteSpace()) continue;
                        builder.Append($"{i,4}: ");
                        builder.Append(line);
                        builder.AppendLine();
                        i++;
                    }
                    Console.Write(builder);

                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        SetClipboard(translated);
                        Console.WriteLine("Code copied to clipboard.");
                    }
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Operation canceled.");
                }
                catch (Exception e)
                {
                    Console.WriteLine();
#if DEBUG
                    Console.WriteLine(e);
#else
                    Console.WriteLine(e.Message);
#endif
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();
                Console.Write($"\nPress enter to translate the file {filePath}");
                Console.ReadLine();
            }
        }

        /*private static string GetFile(string initPath)
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
        }*/

        public static void SetClipboard(string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value), "Attempt to set clipboard with null");

            Process clipboardExecutable = new Process();
            clipboardExecutable.StartInfo = new ProcessStartInfo
            {
                RedirectStandardInput = true,
                FileName = @"clip",
            };

            clipboardExecutable.Start();
            clipboardExecutable.StandardInput.Write(value);
            clipboardExecutable.StandardInput.Close();
            return;
        }
    }
}

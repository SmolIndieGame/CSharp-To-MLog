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
                Console.WriteLine("Drop the .cs file that you want to compile to this window and then press enter.");
                string s = Console.ReadLine().Trim('"');
                string source = File.ReadAllText(s);
#endif
                Console.WriteLine("Original Code:");
                Console.WriteLine(source);
                Console.WriteLine();
                Console.Write("Analysing syntax...");
                Translator translator = new Translator(source);
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
            catch (CompilationException e)
            {
                Console.WriteLine();
#if DEBUG
                Console.WriteLine(e);
#else
                Console.WriteLine(e.Message);
#endif
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.Write("Press any key to exit...");
            Console.ReadKey();
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
            clipboardExecutable.StartInfo = new ProcessStartInfo // Creates the process
            {
                RedirectStandardInput = true,
                FileName = @"clip",
            };
            clipboardExecutable.Start();

            clipboardExecutable.StandardInput.Write(value); // CLIP uses STDIN as input.
            // When we are done writing all the string, close it so clip doesn't wait and get stuck
            clipboardExecutable.StandardInput.Close();

            return;
        }

        public static void GetFile(string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value), "Attempt to set clipboard with null");

            Process pickerExe = new Process();
            pickerExe.StartInfo = new ProcessStartInfo // Creates the process
            {
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                FileName = @"pickerHost",
            };
            pickerExe.Start();

            pickerExe.StandardInput.Write(value); // CLIP uses STDIN as input.
            // When we are done writing all the string, close it so clip doesn't wait and get stuck
            pickerExe.StandardInput.Close();
            return;
        }
    }
}

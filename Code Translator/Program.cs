using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Code_Transpiler
{
    internal class Program
    {
        // TODO: write unit test for all operation parser and operation handler and invocation parser
        // TODO: make tutorial and add doc to readme.md
        // TODO: make transpiler reserved variable name start with $$
        // TODO: make 'jump to ptr' unique to each function to reduce custom function call's costs.
        // TODO: add LinkRef

        // TODO: make condition break, continue, goto and function calls
        // TODO: add support for custom enum
        // TODO: add set color control
        // TODO: add support for foreach loop
        // TODO: add cellArray and cellStack
        // TODO: add support for switch statement

        [STAThread]
        static void Main(string[] args)
        {
            Console.Write("Initializing...");
            Transpiler transpiler = new Transpiler();
            Console.WriteLine("finished.");

#if DEBUG
            string filePath = Path.GetFullPath(@"..\..\..\..\MindustryLogics\Test.cs");
#else
        translate:
            Console.WriteLine("Drop the .cs file that you want to translate to this window and then press enter.");
            Console.WriteLine($"Or type \"s\" or \"select\" to choose a file in {Directory.GetCurrentDirectory()}.");
            string filePath;
            string input = Console.ReadLine();
            if (input.ToLower() == "s" || input.ToLower() == "select")
            {
                if (!SelectFile(Directory.GetCurrentDirectory(), out string path))
                    goto translate;
                filePath = path;
            }
            else
                filePath = input.Trim('"');
#endif
            while (true)
            {
                try
                {
                    string source = File.ReadAllText(filePath);
                    Console.WriteLine("Original code:");
                    Console.WriteLine(source);
                    Console.WriteLine();
                    Console.Write("Analysing syntax...");
                    transpiler.SetSource(source);
                    Console.WriteLine("finished.");
                    Console.Write("Checking code validity...");
                    if (!transpiler.CheckCodeValidity())
                        throw new OperationCanceledException("Translation cancelled due to compile errors.");
                    Console.WriteLine("finished.");
                    Console.Write("Translating...");
                    string translated = transpiler.Translate();
                    Console.WriteLine("finished.");
                    Console.WriteLine("Translated code:");
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

            translateAgain:
                Console.WriteLine($"\nPress enter to translate the file at {filePath}.");
                Console.WriteLine("Or drop another file here and press enter to translate another file.");
                bool dirExist = Directory.Exists(Path.GetDirectoryName(filePath));
                if (dirExist)
                    Console.WriteLine($"Or type \"s\" or \"select\" to choose a file in {Path.GetDirectoryName(filePath)}.");
                string respond = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(respond))
                    continue;

                if (dirExist && (respond.ToLower() == "s" || respond.ToLower() == "select"))
                {
                    if (!SelectFile(Path.GetDirectoryName(filePath), out string path))
                        goto translateAgain;
                    filePath = path;
                    continue;
                }

                filePath = respond.Trim('"');
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

        public static bool SelectFile(string directoryPath, out string file)
        {
            file = null;
            string[] files;
            try
            {
                files = Directory.GetFiles(directoryPath, "*.cs");
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(e.Message);
                return false;
            }

            for (int i = 0; i < files.Length; i++)
                Console.WriteLine($"{i,4}: {Path.GetFileName(files[i])}");

            while (true)
            {
                Console.WriteLine("Type the index of the file you want to translate then press enter.");
                Console.WriteLine("Or press enter to go back.");
                string respond2 = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(respond2))
                    return false;
                if (!int.TryParse(respond2, out int index))
                {
                    Console.WriteLine("That is not a valid number.");
                    continue;
                }

                if (index < 0 || index >= files.Length)
                {
                    Console.WriteLine("Index out of bound.");
                    continue;
                }

                file = files[index];
                return true;
            }
        }

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

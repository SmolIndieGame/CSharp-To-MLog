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

        // TODO: make condition break, continue, goto and function calls
        // TODO: add support for custom enum
        // TODO: add set color control
        // TODO: add support for foreach loop
        // TODO: add cellArray and cellStack
        // TODO: add support for switch statement
        // TODO: change content types from enum to class

        [STAThread]
        static void Main(string[] args)
        {
            Console.Write("Initializing...");
            Transpiler transpiler = new Transpiler();
            Console.WriteLine("finished.");

#if DEBUG
            string filePath = Path.GetFullPath(@"..\..\..\..\MindustryLogics\Test.cs");
#else
        transpile:
            Console.WriteLine("Drop the .cs file that you want to transpile to this window and then press enter.");
            Console.WriteLine($"Or type \"s\" or \"select\" to choose a file in {Directory.GetCurrentDirectory()}.");
            string filePath;
            string input = Console.ReadLine();
            if (input.ToLower() == "s" || input.ToLower() == "select")
            {
                if (!SelectFile(Directory.GetCurrentDirectory(), out string path))
                    goto transpile;
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
                    WriteToConsoleWithLineNumber(source, true);
                    Console.WriteLine();
                    Console.Write("Analysing syntax...");
                    transpiler.SetSource(source);
                    Console.WriteLine("finished.");
                    Console.Write("Checking code validity...");
                    if (!transpiler.CheckCodeValidity())
                        throw new OperationCanceledException("Translation cancelled due to compile errors.");
                    Console.WriteLine("finished.");
                    Console.Write("Translating...");
                    string transpiled = transpiler.Transpile();
                    Console.WriteLine("finished.");
                    Console.WriteLine("Transpiled code:");
                    WriteToConsoleWithLineNumber(transpiled);

                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        SetClipboard(transpiled);
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

            transpileAgain:
                Console.WriteLine($"\nPress enter to transpile the file at {filePath}.");
                Console.WriteLine("Or drop another file here and press enter to transpile another file.");
                bool dirExist = Directory.Exists(Path.GetDirectoryName(filePath));
                if (dirExist)
                    Console.WriteLine($"Or type \"s\" or \"select\" to choose a file in {Path.GetDirectoryName(filePath)}.");
                string respond = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(respond))
                    continue;

                if (dirExist && (respond.ToLower() == "s" || respond.ToLower() == "select"))
                {
                    if (!SelectFile(Path.GetDirectoryName(filePath), out string path))
                        goto transpileAgain;
                    filePath = path;
                    continue;
                }

                filePath = respond.Trim('"');
            }
        }

        private static void WriteToConsoleWithLineNumber(string text, bool offsetOne = false)
        {
            StringBuilder builder = new();
            int i = offsetOne ? 1 : 0;
            foreach (var line in text.AsSpan().Trim().EnumerateLines())
            {
                builder.AppendFormat("{0,4}", i);
                builder.Append(": ");
                builder.Append(line);
                builder.AppendLine();
                i++;
            }
            Console.Write(builder);
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
                Console.WriteLine("Type the index of the file you want to transpile then press enter.");
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

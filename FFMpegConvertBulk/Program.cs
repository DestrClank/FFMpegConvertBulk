using System;
using System.Diagnostics;
using System.IO;

namespace ConvertisseurMusique
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: FFMpegConvertBulk <input folder> <input file extension> <output file extension> [--overwrite]\n\nOptions :\n      --overwrite : Overwrites destination files if they exist without asking.\nExample :\n      FFMpegConvertBulk \"C:\\SourceFolder\" .at3 .mp3 --overwrite");
                return;
            }

            string dossierSource = args[0];
            string extensionSource = args[1];
            string extensionDestination = args[2];
            bool overwriteAll = args.Length > 3 && args[3] == "--overwrite";
            string dossierDestination = Path.Combine(dossierSource, "Converted");

            if (File.Exists(dossierSource))
            {
                Console.WriteLine($"The source {dossierSource} is a file. Cannot continue.");
                Process.GetCurrentProcess().Kill();
            }

            if (!Directory.Exists(dossierSource))
            {
                Console.WriteLine($"The source folder {dossierSource} does not exist. Cannot continue.");
                Process.GetCurrentProcess().Kill();
            }

            if (!Directory.Exists(dossierDestination))
            {
                Directory.CreateDirectory(dossierDestination);
            }

            string[] fichiers = Directory.GetFiles(dossierSource, $"*{extensionSource}");
            int totalFichiers = fichiers.Length;
            int conversionsReussies = 0;

            foreach (string fichier in fichiers)
            {
                string nomFichier = Path.GetFileNameWithoutExtension(fichier);
                string fichierConverti = Path.Combine(dossierDestination, nomFichier + extensionDestination);
                bool overwrite = overwriteAll;

                if (File.Exists(fichierConverti) && !overwriteAll)
                {
                    Console.WriteLine($"The file {fichierConverti} already exists. Overwrite ? (y/n)");
                    string reponse = Console.ReadLine();
                    if (reponse.ToLower() != "y")
                    {
                        Console.WriteLine($"Conversion ignored for : {fichier}");
                        continue;
                    }
                    overwrite = true;
                }

                ConvertirFichier(fichier, fichierConverti, overwrite);
                conversionsReussies++;
                double pourcentage = (double)conversionsReussies / totalFichiers * 100;
                Console.WriteLine($"\u001b[0m({conversionsReussies}/{totalFichiers} [{pourcentage:F2}%]) \u001b[0mConverted : \u001b[33m{fichier}\u001b[0m -> \u001b[32m{fichierConverti}\u001b[0m ");
            }

            Console.WriteLine($"\u001b[34mConversion finished !\u001b[0m");
        }

        static void ConvertirFichier(string input, string output, bool overwrite)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = $"-i \"{input}\" \"{output}\"" + (overwrite ? " -y" : ""),
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(startInfo))
            {
                process.WaitForExit();
            }
        }
    }
}

using System.IO.Compression;

namespace Lab12
{
    public class KVVLog
    {
        private string filePath;

        public KVVLog(string filePath = "KVVlogfile.txt")
        {
            this.filePath = filePath;
            
            if (!File.Exists(this.filePath))
            {
                File.WriteAllText(this.filePath, "--- KVV Log File ---\n");
            }
        }

        public void WriteLog(string action, string details)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string logEntry = $"[{timestamp}] ACTION: {action}, DETAILS: {details}\n";
            File.AppendAllText(this.filePath, logEntry);
        }

        public string[] ReadLogs()
        {
            return File.ReadAllLines(this.filePath);
        }

        public string[] SearchLogs(string keyword)
        {
            return File.ReadLines(this.filePath)
                   .Where(line => line.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                   .ToArray();
        }
    }



    public static class KVVDiskInfo
    {
        public static void ShowFreeSpace(string driveLetter)
        {
            DriveInfo drive = new DriveInfo(driveLetter);
            if (drive.IsReady)
            {
                Console.WriteLine($"Drive {driveLetter}: Free Space: {drive.AvailableFreeSpace / (1024 * 1024 * 1024)} GB");
            }
            else
            {
                Console.WriteLine($"Drive {driveLetter} is not ready.");
            }
        }

        public static void ShowFileSystem(string driveLetter)
        {
            DriveInfo drive = new DriveInfo(driveLetter);
            if (drive.IsReady)
            {
                Console.WriteLine($"Drive {driveLetter}: File System: {drive.DriveFormat}");
            }
            else
            {
                Console.WriteLine($"Drive {driveLetter} is not ready.");
            }
        }

        public static void ShowAllDrivesInfo()
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    Console.WriteLine($"Drive {drive.Name}");
                    Console.WriteLine($"  Volume Label: {drive.VolumeLabel}");
                    Console.WriteLine($"  Total Size: {drive.TotalSize / (1024 * 1024 * 1024)} GB");
                    Console.WriteLine($"  Free Space: {drive.AvailableFreeSpace / (1024 * 1024 * 1024)} GB");
                    Console.WriteLine($"  File System: {drive.DriveFormat}");
                }
                else
                {
                    Console.WriteLine($"Drive {drive.Name} is not ready.");
                }
            }
        }

    }



    public class KVVFileInfo
    {
        public static void ShowFileInfo(string filePath)
        {
            if (File.Exists(filePath))
            {
                FileInfo fileInfo = new FileInfo(filePath);

                Console.WriteLine($"Full Path: {fileInfo.FullName}");
                Console.WriteLine($"Size: {fileInfo.Length} bytes");
                Console.WriteLine($"Extension: {fileInfo.Extension}");
                Console.WriteLine($"Name: {fileInfo.Name}");
                Console.WriteLine($"Created: {fileInfo.CreationTime}");
                Console.WriteLine($"Last Modified: {fileInfo.LastWriteTime}");
            }
            else
            {
                Console.WriteLine($"File not found: {filePath}");
            }
        }
    }



    public class KVVDirInfo
    {
        public static void ShowDirectoryInfo(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(directoryPath);

                Console.WriteLine($"Directory Path: {dirInfo.FullName}");
                Console.WriteLine($"Creation Time: {dirInfo.CreationTime}");

                int fileCount = dirInfo.GetFiles().Length;
                int subDirCount = dirInfo.GetDirectories().Length;

                Console.WriteLine($"Number of Files: {fileCount}");
                Console.WriteLine($"Number of Subdirectories: {subDirCount}");

                Console.WriteLine("Parent Directories:");
                DirectoryInfo parent = dirInfo.Parent;
                while (parent != null)
                {
                    Console.WriteLine($"  {parent.FullName}");
                    parent = parent.Parent;
                }
            }
            else
            {
                Console.WriteLine($"Directory not found: {directoryPath}");
            }
        }
    }


    public class KVVFileManager
    {
        public static void InspectDrive(string drivePath)
        {
            string inspectDirectory = "XXXInspect";
            string dirInfoFile = Path.Combine(inspectDirectory, "xxxdirinfo.txt");


            Directory.CreateDirectory(inspectDirectory);


            var files = Directory.GetFiles(drivePath);
            var directories = Directory.GetDirectories(drivePath);


            using (StreamWriter writer = new StreamWriter(dirInfoFile))
            {
                writer.WriteLine("--- Files ---");
                foreach (var file in files)
                {
                    writer.WriteLine(file);
                }

                writer.WriteLine("\n--- Directories ---");
                foreach (var dir in directories)
                {
                    writer.WriteLine(dir);
                }
            }


            string copiedFile = Path.Combine(inspectDirectory, "xxxdirinfo_copy.txt");
            File.Copy(dirInfoFile, copiedFile);
            File.Delete(dirInfoFile);
        }

        public static void CopyFilesByExtension(string sourceDir, string extension, string targetDir)
        {
            Directory.CreateDirectory(targetDir);

            var files = Directory.GetFiles(sourceDir, "*" + extension);
            foreach (var file in files)
            {
                string fileName = Path.GetFileName(file);
                string destPath = Path.Combine(targetDir, fileName);
                File.Copy(file, destPath);
            }
        }

        public static void MoveDirectory(string sourceDir, string targetDir)
        {
            string destPath = Path.Combine(targetDir, Path.GetFileName(sourceDir));
            Directory.Move(sourceDir, destPath);
        }

        public static void ArchiveDirectory(string sourceDir, string archivePath)
        {
            ZipFile.CreateFromDirectory(sourceDir, archivePath);
        }

        public static void ExtractArchive(string archivePath, string extractDir)
        {
            ZipFile.ExtractToDirectory(archivePath, extractDir);
        }

        
    }
        class Program
    {
        static void Main()
        {
            KVVLog log = new KVVLog();




            Console.WriteLine("\n--- Disk Information ---");

            KVVDiskInfo.ShowFreeSpace("C:\\");

            KVVDiskInfo.ShowFileSystem("C:\\");

            Console.WriteLine("\nAll Drives Info:");
            KVVDiskInfo.ShowAllDrivesInfo();


            Console.WriteLine("\n--- File Information ---");

            string filePath = "example.txt";

            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "This is an example file.");
            }

            KVVFileInfo.ShowFileInfo(filePath);

            Console.WriteLine("\n--- Directory Information ---");


            string directoryPath = "\\test";


            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
                File.WriteAllText(Path.Combine(directoryPath, "example.txt"), "This is an example file.");
            }

  
            KVVDirInfo.ShowDirectoryInfo(directoryPath);




            Console.WriteLine("--- File Manager ---");

            // Example usage
            string drivePath = "C:\\";
            string sourceDir = "TestDirectory";
            string targetDir = "XXXFiles";
            string inspectDir = "XXXInspect";
            string archivePath = Path.Combine(inspectDir, "XXXFiles.zip");
            string extractDir = "ExtractedFiles";

            KVVFileManager.InspectDrive(drivePath);

            KVVFileManager.CopyFilesByExtension(sourceDir, ".txt", targetDir);
            KVVFileManager.MoveDirectory(targetDir, inspectDir);

            KVVFileManager.ArchiveDirectory(Path.Combine(inspectDir, "XXXFiles"), archivePath);
            KVVFileManager.ExtractArchive(archivePath, extractDir);



            Console.WriteLine("\nAll Logs:");
            foreach (string logEntry in log.ReadLogs())
            {
                Console.WriteLine(logEntry);
            }


            Console.WriteLine("\nSearch Results:");
            foreach (string searchResult in log.SearchLogs("create"))
            {
                Console.WriteLine(searchResult);
            }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileProcessingTasks {
    class Program {

        static readonly string directoryPath = "C:\\Users\\ddabovski\\Downloads\\advanced.day.02.threading\\FileTargetFolder";
        static readonly int maxRunningTasks = 4;
        static readonly int targetProcessedFileNumber = 10;
        static int currentProcessedFileNumber = 0;

        static SemaphoreSlim taskLimiterSemaphore = new SemaphoreSlim(0, maxRunningTasks);
        static Dictionary<string, string> fileNameFileDataMap = new Dictionary<string, string>();
        static Queue<string> filesToProcess = new Queue<string>();
        private static object _lock = new object();

        static void Main(string[] args) {
            using (FileSystemWatcher watcher = new FileSystemWatcher()) {
                watcher.Path = directoryPath;
                watcher.NotifyFilter = NotifyFilters.FileName;
                watcher.Filter = "*.*";
                watcher.Created += OnFileAdded;
                watcher.EnableRaisingEvents = true;

                taskLimiterSemaphore.Release(maxRunningTasks);

                Console.WriteLine($"Watcher is watching the directory: {directoryPath}. Insert files into it to continue...");

                StartFileProcessing();

                while (currentProcessedFileNumber < targetProcessedFileNumber) {
                    //NOOP - wait for the processing to be done before writing out the files
                }

                foreach (var file in fileNameFileDataMap) {
                    Console.WriteLine($"File {file.Key}");
                }
            }
        }

        static void OnFileAdded(object source, FileSystemEventArgs args) {
            filesToProcess.Enqueue(args.FullPath);
        }

        static void StartFileProcessing() {
            for (int i = 0; i < targetProcessedFileNumber; i++) {
                Task.Run(() => {
                    ProcessFiles();
                });
            }
        }

        static void ProcessFiles() {
            while (true) {
                Monitor.Enter(_lock);
                if (currentProcessedFileNumber == targetProcessedFileNumber) {
                    Monitor.Exit(_lock);
                    break;
                }
                if (filesToProcess.Count > 0) {
                    taskLimiterSemaphore.Wait();

                    var filePathToProcess = filesToProcess.Dequeue();
                    var fileName = filePathToProcess.Split('\\').Last();

                    Console.WriteLine($"File {fileName} is being processed...");

                    var fileContent = File.ReadAllText(filePathToProcess);
                    fileNameFileDataMap.Add(fileName, fileContent);
                    currentProcessedFileNumber++;

                    Console.WriteLine($"File {fileName} finished processing.");
                    taskLimiterSemaphore.Release();
                }
                Monitor.Exit(_lock);
            }
        }
    }
}

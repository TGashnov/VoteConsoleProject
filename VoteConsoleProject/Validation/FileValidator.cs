﻿using System;
using System.Collections.Generic;
using System.Text;
using VoteConsoleProject.Files;
using System.IO;

namespace VoteConsoleProject.Validation
{
    static class FileValidator
    {
        public static FileTypes ReadFileType()
        {
            Console.Write("Введите тип файла (xml/json): ");
            while (true)
            {
                var input = Console.ReadLine().ToLower();
                FileTypes? fileType = FileManager.GetFileType(input);
                if (fileType == null)
                {
                    Console.Write("Пожалуйста, введите \"xml\" или \"json\" (без кавычек): ");
                    continue;
                }
                return (FileTypes)fileType;
            }
        }

        public static string ReadPathToSave()
        {
            Console.WriteLine("Текущая директория {0}, используется разделитель {1}",
                Environment.CurrentDirectory, Path.DirectorySeparatorChar);
            while (true)
            {
                Console.Write("Введите путь к файлу: ", Path.DirectorySeparatorChar);
                var fileName = Console.ReadLine();
                if (File.Exists(fileName))
                {
                    Console.Write("Указанный файл существует. Перезаписать?");
                    if (!InputControl.ReadYesNo())
                    {
                        continue;
                    }
                }
                return fileName;
            }
        }

        public static string ReadPathToLoad()
        {
            Console.WriteLine("Текущая директория {0}, используется разделитель {1}",
                Environment.CurrentDirectory, Path.DirectorySeparatorChar);
            while (true)
            {
                Console.Write("Введите путь к файлу: ", Path.DirectorySeparatorChar);
                var fileName = Console.ReadLine();
                if (!File.Exists(fileName))
                {
                    Console.WriteLine("Указанный файл не существует.");
                    continue;
                }
                return fileName;
            }
        }
    }
}
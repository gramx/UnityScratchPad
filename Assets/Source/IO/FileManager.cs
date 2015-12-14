using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class FileManager {

    public static readonly string SaveDirectory = "Saves/";
    public static readonly string BlockTextures = "Resources/Textures/Block/";
    private static string LoggerPath;

    public static void SetUpFileManager(string WorldName)
    {
        CheckCreateDirectory(SaveDirectory + WorldName + "/Logs/");
        LoggerPath = SaveDirectory + WorldName + "/Logs/log_y" + DateTime.Now.Year.ToString() + "m" + DateTime.Now.Month.ToString() + "d" + DateTime.Now.Day.ToString() + ".txt";
        File.WriteAllText(LoggerPath, "");
    }
    public static void CheckCreateDirectory(string directory)
    {
        if(!File.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }
    public static string GetLogDirectory()
    {
        return LoggerPath;
    }
}

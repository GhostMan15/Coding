using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

namespace NrpaNup;

public class Loginanje
{
    public ObservableCollection<string> Logginanje { get; } = new ObservableCollection<string>();
    public async Task Login(string ime, int userId)
    {
       
        LogToFile($"{ime} prijava: {DateTime.Now}" , userId);
    }

    public async Task Logout(string ime, int userId)
    {
       
        LogToFile($"{ime} odjava: {DateTime.Now}", userId);
    }

    private void LogToFile(string log, int userId)
    {
       
        string fileName = $"log_user_{userId}.txt"; 
        string filePath = Path.Combine("logs", fileName); 

      
        if (!File.Exists(filePath))
        {
           
            Directory.CreateDirectory("logs");
            using (StreamWriter writer = File.CreateText(filePath))
            {
              
                writer.WriteLine(log);
            }
        }
        else
        {
            // If the file already exists, append the log entry to it
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(log);
            }
        }
    }

    public List<string> ReadLogFile(int userId)
    {
       
        string fileName = $"log_user_{userId}.txt"; 
        string filePath = Path.Combine("logs", fileName); 
        List<string> logs = new List<string>();

        if (File.Exists(filePath))
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    logs.Add(line);
                }
            }
        }

        return logs;
      
    }
}
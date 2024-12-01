using System;
using System.Linq;

class Program
{
    static void Main()
    {
        string[] calibrationDocument =
        {
            "1abc2",
            "pqr3stu8vwx",
            "a1b2c3d4e5f",
            "treb7uchet"
        };

        int result = SumCalibrationValues(calibrationDocument);


        Console.WriteLine(result);
    }

    static int SumCalibrationValues(string[] calibrationDocument)
    {
        int totalSum = 0;

        for (int i = 0; i < calibrationDocument.Length; i++)
        {
            string line = calibrationDocument[i];


            var digits = line.Where(char.IsDigit).Select(c => int.Parse(c.ToString()));


            int firstDigit = digits.FirstOrDefault();
            int lastDigit = digits.LastOrDefault();


            int calibrationValue = firstDigit * 10 + lastDigit;


            totalSum += calibrationValue;
        }

        return totalSum;
    }
}


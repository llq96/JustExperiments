using System.Globalization;

namespace ExamplesForInterview.TgSolution;

public class TG_TaskAboutBytes
{
    public void Run() //Original
    {
        byte b1 = 0xAB;
        byte b2 = 0x99;
        byte temp;
        temp = (byte)-b2;
        Console.Write(temp + " "); //103
        temp = (byte)(b1 << b2);
        Console.Write(temp + " "); //0
        temp = (byte)(b2 >> 2);
        Console.WriteLine(temp); //38
    }

    public void Run2()
    {
        byte b1 = 0xAB;
        LogAsBinary(b1); // 171 is 0b_10101011

        byte b2 = 0x99;
        LogAsBinary(b2); // 153 is 0b_10011001

        var tempInt = -b2;
        LogAsBinary(tempInt); //-153 is 0b_11111111_11111111_11111111_01100111

        byte temp = (byte)tempInt; // Result = (tempInt % 256) + 256
        LogAsBinary(temp); // 103 is 0b_01100111

        temp = (byte)(b1 << b2);
        LogAsBinary(temp); // 0 is 0b_00000000

        temp = (byte)(b2 >> 2);
        LogAsBinary(temp); // 38 is 0b_00100110
    }

    private static void LogAsBinary<T>(T value) where T : IFormattable
    {
        var padLeft = value is Byte ? 8 : 32;
        var log = $"{value} is {GetBinaryLog(value, padLeft)}";
        Console.WriteLine(log);
    }

    private static string GetBinaryLog<T>(T value, int padLeft, int splitChunkSize = 8) where T : IFormattable
    {
        var convertedString = value.ToString("b", CultureInfo.InvariantCulture).PadLeft(padLeft, '0');
        var convertedStringWithUnderscore = string.Join('_', Split(convertedString, splitChunkSize));
        return $"0b_{convertedStringWithUnderscore}";
    }

    private static IEnumerable<string> Split(string str, int chunkSize)
    {
        return Enumerable.Range(0, str.Length / chunkSize)
            .Select(i => str.Substring(i * chunkSize, chunkSize));
    }
}
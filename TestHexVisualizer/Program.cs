using System;
using HexVisualizer;

namespace TestHexVisualizer
{
    class Program
    {
        [STAThread]

        static void Main(string[] args)
        {
            byte[] b = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            Console.WriteLine("Testing basic byte array");
            ByteArrayVisualizer.TestShowVisualizer(b);

            string s1 = "Some string";
            Console.WriteLine("Testing basic string");
            StringVisualizer.TestShowVisualizer(s1);

            string s2 = "000102030405060708090A0B0C0D0E0F101A1B1C1D1E1F";
            Console.WriteLine("Testing hex string");
            StringVisualizer.TestShowVisualizer(s2);

            string s3 = "0x000102030405060708090A0B0C0D0E0F101A1B1C1D1E1F";
            Console.WriteLine("Testing hex string with 0x prefix");
            StringVisualizer.TestShowVisualizer(s3);

            string s4 = Convert.ToBase64String(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 });
            Console.WriteLine("Testing base64");
            StringVisualizer.TestShowVisualizer(s4);

            string s5 = "ЁЂЃЄЅІЇЈЉЊЋЌЎЏАБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдежзи";

            Console.WriteLine("Testing encoding");
            StringVisualizer.TestShowVisualizer(s5);

        }
    }
}

using System;
using System.Diagnostics;
using Fernandezja.ColorHashSharp;

namespace ConsoleAppNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ColorHash >  Hello World");

            var colorHash = new ColorHash();

            //HSL (return HSL object)
            var hsl = colorHash.Hsl("Hello World");

            Console.WriteLine($"  H = {hsl.H} | S = {hsl.S} | L = {hsl.L}");
            Debug.WriteLine($"  H = {hsl.H} | S = {hsl.S} | L = {hsl.L}");


            //RGB (return System.Drawing.Color object)
            var color = colorHash.Rgb("Hello World");

            Console.WriteLine($"  R= {color.R} | G = {color.G} | B = {color.B}");
            Debug.WriteLine($"  R= {color.R} | G = {color.G} | B = {color.B}");


            //Hex (return string)
            var hex = colorHash.Hex("Hello World");

            Console.WriteLine($"  hex = {hex}");
            Debug.WriteLine($"  hex = {hex}");

            Console.WriteLine("Press any key to close...");
            Console.ReadKey();
        }
    }
}

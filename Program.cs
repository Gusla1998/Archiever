using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiever
{
    class Program
    {
        static void Main(string[] args)
        {
            Encode encoder = new Encode();
            Console.WriteLine("Введите название исходной картинки:");
            string image_input = Console.ReadLine();
            Console.WriteLine("Введите название файла для сохранения:");
            string file = Console.ReadLine();
            encoder.Image_Encoding(image_input, file);

            Decode decoder = new Decode();

            Console.WriteLine("Введите название картинки на выходе:");
            string image_output = Console.ReadLine();
            decoder.Image_Decoding(file, image_output);

            Console.Read();
        }
    }
}

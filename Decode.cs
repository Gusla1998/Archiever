using System.Drawing;
using System.IO;
using System.Collections.Generic;


namespace Archiever
{
	class Decode
	{
		public void Image_Decoding(string fileName, string imageName)
		{
			RLE_Algorithm cypher = new RLE_Algorithm();
			byte[] arr_image = ReadFile(fileName);//получаем массив байтов из файла fileName
			byte[] decomp_arr = cypher.decompress(arr_image);
			Image new_image = ByteArrayToImage(decomp_arr);//создаем объект Image из данного массива
			new_image.Save(imageName, System.Drawing.Imaging.ImageFormat.Bmp);//создаем картинку по пути imageName
		}
		//Из массива байтов получаем картинку и возвращаем ее
		public Image ByteArrayToImage(byte[] byteArrayIn)
		{
			MemoryStream ms = new MemoryStream(byteArrayIn);
			Image returnImage = Image.FromStream(ms);
			return returnImage;
		}
		//Считываем байты из файла и сохраняем в массив байтов
		public byte[] ReadFile(string fileName)
		{
			List<byte> arr_byte = new List<byte>();
			using (BinaryReader br = new BinaryReader(File.Open(fileName, FileMode.Open)))
			{
				for (int i = 0; i < br.BaseStream.Length; i++)
				{
					arr_byte.Add(br.ReadByte());
				}
			}

			byte[] arr = new byte[arr_byte.Count];

			for (int i = 0; i < arr_byte.Count; i++)
			{
				arr[i] = arr_byte[i];
			}
			return arr;
		}
	}
}

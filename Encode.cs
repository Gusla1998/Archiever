using System.Drawing;
using System.IO;


namespace Archiever
{
	class Encode
	{
		public void Image_Encoding(string ImageName, string FileName)
		{
			byte[] arr = ImageToByteArray(ImageName);//преобразуем картинку ImageName в массива байтов
			RLE_Algorithm cypher = new RLE_Algorithm();
			byte[] comp_arr = cypher.compress(arr);

			WriteFile(comp_arr, FileName);//записываем массив байтов в FileName
		}

		//Преобразовываем картинку в массив байтов
		private byte[] ImageToByteArray(string image)
		{
			Image imageIn = Image.FromFile(image);

			MemoryStream ms = new MemoryStream();
			imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
			return ms.ToArray();
		}

		//Записываем массив байтов в dat файл
		private void WriteFile(byte[] imageByte, string fileName)
		{
			using (BinaryWriter bw = new BinaryWriter(File.Open(fileName, FileMode.Create)))
			{
				for (int i = 0; i < imageByte.Length; i++)
				{
					bw.Write(imageByte[i]);
				}
			}
		}
	}
}

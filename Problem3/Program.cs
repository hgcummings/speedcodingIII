using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Problem3
{
  class Program
  {
    private const int Stride = 3;

    private static int width;
    private static int height;

    static void Main(string[] args)
    {
      var bitmap = (Bitmap)Image.FromFile("hiddenimage3.bmp");

      var hiddenBitmap = new Bitmap(width = bitmap.Width / Stride, height = bitmap.Height / Stride);

      for (int i = 0; i < width; i++)
      {
        for (int j = 0; j < height; j++)
        {
          var red = 0;
          var green = 0;
          var blue = 0;

          int mask = 1;

          for (var bit = 0; bit < 8; bit++)
          {
            checked
            {
              var currentPixel = bitmap.GetPixel((i * Stride) + (bit % Stride), (j * Stride) + (bit / Stride));

              red += (currentPixel.R & mask) << (bit );
              green += (currentPixel.G & mask) << (bit );
              blue += (currentPixel.B & mask) << (bit );
            }

          }

          hiddenBitmap.SetPixel(i, j, Color.FromArgb(255, red, green, blue));
        }
      }


      hiddenBitmap.Save("hiddenimage.bmp");
    }
  }
}

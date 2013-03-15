using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
      GenerateImages("filled-with-goodness.png", "");
    }

    private static void GenerateImages(String sourcePath, String prefix)
    {
      var source = (Bitmap)Image.FromFile(sourcePath);

      for (int pow = 0; pow < 4; pow++)
      {
        var newPrefix = String.Format("{0}{1}_", prefix, pow);

        var hiddenBitmap = new Bitmap(width = source.Width/Stride, height = source.Height/Stride);

        var blank = true;

        for (int i = 0; i < width; i++)
        {
          for (int j = 0; j < height; j++)
          {
            var red = 0;
            var green = 0;
            var blue = 0;
            
            for (var bit = 0; bit < 8; bit++)
            {
              checked
              {
                var currentPixel = source.GetPixel((i*Stride) + (bit%Stride), (j*Stride) + (bit/Stride));

                var mask = Convert.ToInt32(Math.Pow(2, pow));

                red += (currentPixel.R & mask) << (bit - pow);
                green += (currentPixel.G & mask) << (bit - pow);
                blue += (currentPixel.B & mask) << (bit - pow);
              }

            }

            if (blank && (red > 0 || green > 0 || blue > 0))
            {
              blank = false;
            }

            hiddenBitmap.SetPixel(i, j, Color.FromArgb(255, red, green, blue));
          }
        }

        if (!blank)
        {
          var filename = newPrefix.TrimEnd('_') + ".png";
          hiddenBitmap.Save(filename, ImageFormat.Png);

          if (newPrefix.Length < 6)
          {
            GenerateImages(filename, newPrefix);
          }
        }


      }

    }
  }
}

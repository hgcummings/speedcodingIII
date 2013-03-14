using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Problem2
{
  class Program
  {
    private const int Stride = 5;

    private static bool[,] state;
    private static int width;
    private static int height;

    static void Main(string[] args)
    {
        LoadInitialState();
        Run();
    }

    private static void LoadInitialState()
    {
      var bitmap = (Bitmap)Image.FromFile("Example.bmp");

      state = new bool[width = bitmap.Width / Stride, height = bitmap.Height / Stride];

      for (int i = 0; i < width; i++)
      {
        for (int j = 0; j < height; j++)
        {
          if (bitmap.GetPixel(i*Stride, j*Stride).R == 0)
          {
            state[i, j] = true;
          }
        }
      }
    }
    
    private static void Run()
    {
      for (int i = 0; i <= 1000; i++)
      {
        if (i < 3 || i == 1000)
        {
          SaveState(i);
        }
        Step();
      }
    }

    private static void Step()
    {
      var nextState = new bool[width,height];

      for (int i = 0; i < width; i++)
      {
        for (int j = 0; j < height; j++)
        {
          nextState[i, j] = GetNextState(i, j);
        }
      }

      state = nextState;
    }

    private static bool GetNextState(int i, int j)
    {
      var count = 0;

      var minX = i == 0 ? 0 : -1;
      var maxX = i == width - 1 ? 0 : 1;
      var minY = j == 0 ? 0 : -1;
      var maxY = j == height - 1 ? 0 : 1;

      for (int relX = minX; relX <= maxX; relX++)
      {
        for (int relY = minY; relY <= maxY; relY++)
        {
          if (relX == 0 && relY == 0)
          {
            continue;
          }

          if (state[i + relX, j + relY])
          {
            ++count;
          }

          if (count > 3)
          {
            return false;
          }
        }
      }

      return state[i, j] ? (count == 2 || count == 3) : (count == 3);
    }

    private static void SaveState(int step)
    {
      var bitmap = new Bitmap(width * Stride, height * Stride);

      for (int i = 0; i < width; i++)
      {
        for (int j = 0; j < height; j++)
        {
          var color = state[i,j] ? Color.Black : Color.White;

          for (int x = i*Stride; x < i*Stride + 5; x++)
          {
            for (int y = j * Stride; y < j * Stride + 5; y++)
            {
              bitmap.SetPixel(x, y, color);
            }
          }
        }
      }

      bitmap.Save(String.Format("Step{0}.bmp", step));
    }
  }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Color = System.Drawing.Color;

namespace Problem2Bonus
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private const int Stride = 5;

    private bool[,] state;
    private int width;
    private int height;

    private Bitmap prevBitmap;
    
    private void LoadInitialState()
    {
      var bitmap = (Bitmap)Image.FromFile("Example.bmp");

      state = new bool[width = bitmap.Width / Stride, height = bitmap.Height / Stride];

      for (int i = 0; i < width; i++)
      {
        for (int j = 0; j < height; j++)
        {
          if (bitmap.GetPixel(i * Stride, j * Stride).R == 0)
          {
            state[i, j] = true;
          }
        }
      }
    }

    private void Run()
    {
      for (int i = 0; i <= 1000; i++)
      {
        ShowState();
        Step();
      }
    }

    private void Step()
    {
      var nextState = new bool[width, height];

      for (int i = 0; i < width; i++)
      {
        for (int j = 0; j < height; j++)
        {
          nextState[i, j] = GetNextState(i, j);
        }
      }

      state = nextState;
    }

    private bool GetNextState(int i, int j)
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

    private void ShowState()
    {
      var bitmap = new Bitmap(width * Stride, height * Stride);

      for (int i = 0; i < width; i++)
      {
        for (int j = 0; j < height; j++)
        {
          var color = state[i, j] ? Color.Black : Color.White;

          if (prevBitmap != null)
          {
            var prevColor = prevBitmap.GetPixel(i * Stride, j * Stride);
            color = Color.FromArgb(255, (prevColor.R + color.R) / 2, (prevColor.G + color.G) / 2, (prevColor.B + color.B) / 2);
          }

          for (int x = i * Stride; x < i * Stride + 5; x++)
          {
            for (int y = j * Stride; y < j * Stride + 5; y++)
            {
              bitmap.SetPixel(x, y, color);
            }
          }
        }
      }

      prevBitmap = bitmap;

      var bitmapImage = new BitmapImage();
      var stream = new MemoryStream();
      bitmap.Save(stream, ImageFormat.Png);
      stream.Position = 0;
      bitmapImage.BeginInit();
      bitmapImage.CacheOption = BitmapCacheOption.None;
      bitmapImage.StreamSource = stream;
      bitmapImage.EndInit();

      MainImage.Source = bitmapImage;
    }

    public MainWindow()
    {
      InitializeComponent();
      LoadInitialState();
      ShowState();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
      var timer = new DispatcherTimer();
      timer.Interval = TimeSpan.FromMilliseconds(10);
      timer.Tick += TimerOnTick;
      timer.Start();
    }

    private void TimerOnTick(object sender, EventArgs eventArgs)
    {
      Step();
      ShowState();
    }
  }
}

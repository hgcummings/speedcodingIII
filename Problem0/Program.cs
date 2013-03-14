using System;
using System.Drawing;
using System.IO;

namespace Problem0
{
  public class Program
  {
    static void Main()
    {
      var number = Convert.ToInt32("0xFF44", 16);
      Console.WriteLine((number >> 8).ToString("X"));


      /*// Read a load of lines from a file
      foreach (string inputLine in File.ReadAllLines(@"FileList.csv"))
      {
        // Do some naive CSV parsing
        string[] sourceAndDestination = inputLine.Split(',');
        string sourceFilename = sourceAndDestination[0];
        string destinationFilename = sourceAndDestination[1];

        // Read a bitmap from disk
        // Note that this code requires you to add a reference to System.Drawing to your project
        var bitmap = (Bitmap)Image.FromFile(sourceFilename);

        // Do some pixel-level manipulation of the bitmap, fiddling with the colours using RGB values
        int centreX = bitmap.Width/2;
        int centreY = bitmap.Height/2;

        var oldColour = bitmap.GetPixel(centreX, centreY);

        var newColour = Color.FromArgb(oldColour.R/2, oldColour.G, oldColour.B*2);

        bitmap.SetPixel(centreX, centreY, newColour);

        // Save the resultant bitmap to disk
        bitmap.Save(destinationFilename);

        // Display some sort of output to some sort of console so you have something to send to MWR as your "answer"
        Console.WriteLine("The pixel was this red: " + newColour.R);
    }
       */
    }
  }
}

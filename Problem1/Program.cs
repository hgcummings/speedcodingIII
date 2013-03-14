using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem1
{
  class Program
  {
    static void Main(string[] args)
    {
      var answer = Enumerable
        .Range(0, 1000000)
        .Aggregate(0, (cumulative, current) => IsKaprekarNumber(current) ? cumulative + 1 : cumulative);

      Console.WriteLine("ANSWER:");
      Console.WriteLine(answer);
      Console.ReadLine();
    }

    private static bool IsKaprekarNumber(long current)
    {
      long square = current * current;

      int digits = square.ToString("X").Length;

      for (int splitPoint = 0; splitPoint < digits; splitPoint++)
      {
        int shift = 4*splitPoint;
        var upperPart = square >> (shift);
        var lowerPart = square - (upperPart << (shift));
        if (lowerPart > 0 & upperPart > 0 && lowerPart + upperPart == current)
        {
          Console.WriteLine("{0} squared = {1}, {0} = {2} + {3}", current.ToString("X"), square.ToString("X"), upperPart.ToString("X"), lowerPart.ToString("X"));
          return true;
        }
      }

      return false;
    }
  }
}

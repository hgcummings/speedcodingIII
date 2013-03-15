using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem4
{
  class Program
  {
    private static Dictionary<String, List<String>> connections = new Dictionary<string, List<string>>();

    static void Main(string[] args)
    {
      foreach (string inputLine in File.ReadAllLines(@"AllRoutes.csv"))
      {
        // Do some naive CSV parsing
        string[] pair = inputLine.Split(',');

        AddConnection(pair[0], pair[1]);
        AddConnection(pair[1], pair[0]);
      }

      foreach (var station in connections.Where(x => x.Value.Count > 6))
      {
        Console.WriteLine(station.Key);
      }
    }

    private static void AddConnection(string p0, string p1)
    {
      if (!connections.ContainsKey(p0))
      {
        connections.Add(p0, new List<string>());
      }
      connections[p0].Add(p1);
    }
  }
}

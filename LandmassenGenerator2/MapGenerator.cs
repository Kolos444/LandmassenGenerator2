using System;
using System.Collections.Generic;

namespace LandmassenGenerator2;

public static class MapGenerator{
	private static Random _rand = new();

	public static Tiles?[,] Generate(Dictionary<string, Tiles> tileLibrary, int width = 25, int height = 25,
									 int                       seed = 3531) {
		_rand = new(seed);
		Tiles?[,] map = new Tiles?[width, height];

		GenerateTile(_rand.Next(width), _rand.Next(height), map, tileLibrary);
		return map;
	}

	private static void GenerateTile(int x, int y, Tiles?[,] map, Dictionary<string, Tiles> tileLibrary) {
		Dictionary<string, int> possibilities = new();

		if (x - 1 >= 0 && map[x - 1, y] != null)
			for (int i = 0; i < map[x - 1, y]!.Value.compatible!.Length; i++)
				possibilities.Add(map[x - 1, y]!.Value.compatible![i], map[x - 1, y]!.Value.compatibleWeight![i]);

		if (y + 1 < map.GetLength(1) && map[x, y + 1] != null)
			for (int i = 0; i < map[x, y + 1]!.Value.compatible!.Length; i++)
				if (possibilities.ContainsKey(map[x, y + 1]!.Value.compatible![i]))
					possibilities[map[x, y + 1]!.Value.compatible![i]] += map[x, y + 1]!.Value.compatibleWeight![i];
				else
					possibilities.Add(map[x, y + 1]!.Value.compatible![i], map[x - 1, y]!.Value.compatibleWeight![i]);

		if (x + 1 < map.GetLength(0) && map[x + 1, y] != null)
			for (int i = 0; i < map[x + 1, y]!.Value.compatible!.Length; i++)
				if (possibilities.ContainsKey(map[x + 1, y]!.Value.compatible![i]))
					possibilities[map[x + 1, y]!.Value.compatible![i]] += map[x, y + 1]!.Value.compatibleWeight![i];
				else
					possibilities.Add(map[x + 1, y]!.Value.compatible![i], map[x - 1, y]!.Value.compatibleWeight![i]);

		if (y - 1 >= 0 && map[x, y - 1] != null)
			for (int i = 0; i < map[x, y - 1]!.Value.compatible!.Length; i++)
				if (possibilities.ContainsKey(map[x, y - 1]!.Value.compatible![i]))
					possibilities[map[x, y - 1]!.Value.compatible![i]] += map[x, y + 1]!.Value.compatibleWeight![i];
				else
					possibilities.Add(map[x, y - 1]!.Value.compatible![i], map[x - 1, y]!.Value.compatibleWeight![i]);

		int maxPossibility = 0;

		foreach ((string? key, int value) in possibilities){
			possibilities[key] += maxPossibility;
			maxPossibility     =  value;
		}

		int randomNumber = _rand.Next(maxPossibility);

		foreach ((string? key, int value) in possibilities)
			if (possibilities[key] >= randomNumber)
				map[x, y] = tileLibrary[key];

		if (map[x, y] == null)
			map[x, y] = tileLibrary["Deep Ocean"];


		if (x - 1 >= 0 && map[x - 1, y] == null)
			GenerateTile(x - 1, y, map, tileLibrary);
		if (x + 1 < map.GetLength(0) && map[x + 1, y] == null)
			GenerateTile(x + 1, y, map, tileLibrary);
		if (y - 1 >= 0 && map[x, y - 1] == null)
			GenerateTile(x, y - 1, map, tileLibrary);
		if (y + 1 < map.GetLength(1) && map[x, y + 1] == null)
			GenerateTile(x, y + 1, map, tileLibrary);
	}
}
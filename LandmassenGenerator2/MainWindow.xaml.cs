using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Color = System.Windows.Media.Color;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace LandmassenGenerator2;

/// <summary>Interaction logic for MainWindow.xaml</summary>
public partial class MainWindow : Window{
	private readonly Tiles?[,]                 _map;
	private readonly Dictionary<string, Tiles> _tileLibrary;

	public MainWindow() {
		_tileLibrary = LoadTiles();
		_map         = MapGenerator.Generate(_tileLibrary);

		InitializeComponent();


		DrawMap();
	}

	// private static void tilesToJSON() {
	// 	FileStream     fileStream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "file.json", FileMode.Create);
	// 	BufferedStream stream     = new BufferedStream(fileStream);
	//
	// 	Tiles tiles  = new Tiles("Deep Ocean", Color.FromRgb(0, 0, 139), new[] { "Ocean" },      new[] { 100 });
	// 	Tiles tiles2 = new Tiles("Ocean",      Color.FromRgb(0, 0, 255), new[] { "Deep Ocean" }, new[] { 100 });
	//
	// 	stream.Write(JsonSerializer.SerializeToUtf8Bytes(new[] { tiles, tiles2 }, new JsonSerializerOptions {
	// 		WriteIndented = true
	// 	}));
	//
	// 	stream.Flush();
	// 	stream.Close();
	// }

	private static Dictionary<string, Tiles> LoadTiles() {
		BufferedStream fileStream =
			new(new FileStream(AppDomain.CurrentDomain.BaseDirectory + "file.json", FileMode.Open));
		Tiles[] tiles = JsonSerializer.Deserialize<Tiles[]>(fileStream)!;

		return tiles.ToDictionary(tile => tile.name);
	}

	private void DrawMap() {
		for (int y = 0; y < _map.GetLength(1); y++){
			for (int x = 0; x < _map.GetLength(0); x++){
				Line line = new Line {
					Width = 2,
					Stroke = new SolidColorBrush(_map[x,y]!.Value.color)
				};
				line.X1 = x;
				line.X2 = x+1;
				line.Y2 = y;
				line.Y2 = y+1;
				Canvas.Children.Add(line);
			}
		}
	}
}
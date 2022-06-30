using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;

namespace LandmassenGenerator2;

/// <summary>Interaction logic for MainWindow.xaml</summary>
public partial class MainWindow : Window{
	private readonly Tiles?[,]                 _map;
	private readonly Dictionary<string, Tiles> _tileLibrary;

	public MainWindow() {
		_tileLibrary = LoadTiles();
		//_map         = MapGenerator.Generate(_tileLibrary);

		InitializeComponent();
	}

	private static Dictionary<string, Tiles> LoadTiles() {
		BufferedStream fileStream =
			new(new FileStream(AppDomain.CurrentDomain.BaseDirectory + "file.json", FileMode.Open));
		Tiles[] tiles = JsonSerializer.Deserialize<Tiles[]>(fileStream)!;

		return tiles.ToDictionary(tile => tile.name);
	}
}
using System.Drawing;

namespace LandmassenGenerator2;

public struct Tiles{
	public Tiles(string name, Color color, string[]? compatible = null, int[]? compatibleWeight = null) {
		this.name             = name;
		this.compatible       = compatible;
		this.compatibleWeight = compatibleWeight;
		this.color            = color;
	}

	public string    name             { get; }
	public string[]? compatible       { get; }
	public int[]?    compatibleWeight { get; }
	public Color     color            { get; }
}
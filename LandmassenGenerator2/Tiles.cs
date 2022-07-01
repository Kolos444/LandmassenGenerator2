
using System.Windows.Media;

namespace LandmassenGenerator2;

public struct Tiles{
	public Tiles(string name, Color color, string[]? compatible = null, int[]? compatibleWeight = null) {
		this.name             = name;
		this.compatible       = compatible;
		this.compatibleWeight = compatibleWeight;
		this.color            = color;
	}

	public string    name             { get; set; }
	public string[]? compatible       { get; set;  }
	public int[]?    compatibleWeight { get; set;  }
	public Color     color            { get; set;  }
}
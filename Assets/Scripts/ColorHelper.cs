using UnityEngine;
using System.Collections;

public class ColorHelper {

	public static Color Lerp(Color original, Color other, float t, ColorMode mode = ColorMode.replace) {
		switch(mode) {
		case ColorMode.replace:
			return Color.Lerp(original, other, t);
		
		case ColorMode.add:
			return Color.Lerp(original, original + other, t);

		case ColorMode.subtract:
			return Color.Lerp(original, original - other, t);

		case ColorMode.multiply:
			return Color.Lerp(original, original * other, t);

		case ColorMode.divide:
			return Color.Lerp(original, new Color(original.r / other.r, original.g / other.g, original.b / other.b, original.a / other.a), t);
		}

		return original;
	}

	public enum ColorMode {
		replace, add, subtract, multiply, divide
	}

}

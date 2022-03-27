using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palette
{
    public enum PaletteNames
    {
        CharacterA_Palette,
        CharacterB_Palette
    }

    private string PaletteName;
    private Color[] ColorSet;

    public Color this[int index]
    {
        get
        {
            if(index < ColorSet.Length)
                return ColorSet[index];
            else
            {
                Debug.LogError("Palette: Requested index outside range");
                return Color.black;
            }
        }
    }

    private static Color[] AColorSet = new Color[]
    {
        Color.white, Color.red, Color.green, Color.blue
    };

    private static Color[] BColorSet = new Color[]
    {
        Color.white, Color.yellow, Color.cyan, Color.magenta
    };

    private static Palette AColorPalette = new Palette(PaletteNames.CharacterA_Palette.ToString(), AColorSet);
    private static Palette BColorPalette = new Palette(PaletteNames.CharacterA_Palette.ToString(), BColorSet);

    private static List<Palette> CharacterPalettes = new List<Palette>
    {
        AColorPalette, BColorPalette
    };

    public static Palette GetCharacterPalette(string name)
    {
        foreach(Palette palette in CharacterPalettes)
        {
            if (palette.PaletteName == name) return palette;
        }
        Debug.LogError("Palette: No Palette named: " + name);
        return null;
    }

    private Palette(string name, Color[] colors)
    {
        PaletteName = name;
        ColorSet = colors;
    }
}

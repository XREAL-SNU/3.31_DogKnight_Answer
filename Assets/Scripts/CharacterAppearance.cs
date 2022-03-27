using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAppearance
{
    public Dictionary<string, GameObject> CustomParts = new Dictionary<string, GameObject>();
    private CharacterInfo _descripter;
    public CharacterInfo Descripter
    {
        get { return _descripter; }
    }

}

public class CharacterInfo
{
    public string CharacterName;
    public int Health;
    public CharacterPart[] Parts;

    public CharacterInfo(string name, int health)
    {
        this.CharacterName = name;
        this.Health = health;
    }
    public CharacterInfo() { }

    public CharacterPart this[string name]
    {
        get
        {
            CharacterPart part = null;
            for (int i = 0; i < Parts.Length; i++)
            {
                if (Parts[i].PartName.Equals(name))
                {
                    part = Parts[i];
                }
            }
            if (part == null)
            {
                Debug.LogError($"CharacterInfo: Property with name {name} does not exist");
            }
            return part;
        }
    }
}

public class CharacterPart
{
    public string PartName;
    public string PartPath;
    public CharacterPartProperty Properties;

    public CharacterPart(string name, string path)
    {
        this.PartName = name;
        this.PartPath = path;
    }

    public CharacterPart() { }

}

public class CharacterPartProperty
{
    private string PaletteName;
    private int Pick;

    public CharacterPartProperty(Palette.PaletteNames paletteName, int pick = 0)
    {
        this.PaletteName = paletteName.ToString();
        this.Pick = pick;
    }

    public CharacterPartProperty() { }

    public CharacterPartProperty SetProperty(Palette.PaletteNames paletteName, int pick)
    {
        PaletteName = paletteName.ToString();
        Pick = pick;
        return this;
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemPropertyType
{
    Damage, Heal, Drain, Defense
}

public class ItemProperty
{
    public static ItemProperty DamageItem_Flame = new ItemProperty(ItemPropertyType.Damage, "FlameItem", 4);
    public static ItemProperty DamageItem_FireSpear = new ItemProperty(ItemPropertyType.Damage, "FireSpearItem", 3);
    public static ItemProperty DamageItem_Hammer = new ItemProperty(ItemPropertyType.Damage, "HammerItem", 2);
    public static ItemProperty HealItem_HealStone = new ItemProperty(ItemPropertyType.Heal, "HealStoneItem", 4);
    public static ItemProperty DrainItem_Curese = new ItemProperty(ItemPropertyType.Drain, "CurseItem", 5);
    public static ItemProperty DefenseItem_Armor = new ItemProperty(ItemPropertyType.Defense, "ArmorItem", 3);

    public static ItemProperty[] ItemProperties = new ItemProperty[]
    {
        DamageItem_Flame, DamageItem_FireSpear, DamageItem_Hammer,
        HealItem_HealStone, DrainItem_Curese, DefenseItem_Armor
    };

    public static ItemProperty GetItemProperty(string name)
    {
        foreach(ItemProperty item in ItemProperties)
        {
            if(item.ItemName.Equals(name))
            {
                return item;
            }
        }
        return null;
    }

    private ItemProperty(ItemPropertyType type, string name, int num)
    {
        this.PropertyType = type.ToString();
        this.ItemName = name;
        this.ItemNumber = num;
        this.ItemAction = new ItemAction(type.ToString());
    }

    public string PropertyType;
    public string ItemName;
    public int ItemNumber;
    public ItemAction ItemAction;
}

public class ItemAction
{
    public string PropertyType;

    public ItemAction(string type)
    {
        this.PropertyType = type;
    }

    public void ItemActionFunc()
    {
        
    }
}

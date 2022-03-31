using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemPropertyType
{
    Damage, Heal
}

public class ItemProperty
{
    public static ItemProperty DamageItem_Flame = new ItemProperty(ItemPropertyType.Damage, "FlameItem", 4, 5f);
    public static ItemProperty DamageItem_FireSpear = new ItemProperty(ItemPropertyType.Damage, "FireSpearItem", 3, 10f);
    public static ItemProperty HealItem_HealStone = new ItemProperty(ItemPropertyType.Heal, "HealStoneItem", 4, 20f);

    // ItemProperties �����ؼ� foreach ������ ��ȸ�ϸ� Inventory���� subitem ����!?
    public static ItemProperty[] ItemProperties = new ItemProperty[]
    {
        DamageItem_Flame, DamageItem_FireSpear,
        HealItem_HealStone
    };

    // 
    public static ItemProperty GetItemProperty(string name)
    {
        foreach (ItemProperty item in ItemProperties)
        {
            if (item.ItemName.Equals(name))
            {
                return item;
            }
        }
        return null;
    }

    private ItemProperty(ItemPropertyType type, string name, int num, float action)
    {
        this.PropertyType = type.ToString();
        this.ItemName = name;
        this.ItemNumber = num;
        this.ItemAction = action;
    }

    public string PropertyType; // ������ ����
    public string ItemName; // ������ �̸�
    public int ItemNumber; // ������ ����
    public float ItemAction; // ������ Ŭ���� Ȱ���� ����
}

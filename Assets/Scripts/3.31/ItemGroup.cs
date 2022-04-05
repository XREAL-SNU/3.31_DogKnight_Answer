using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XReal.XTown.UI;
using System;


public class ItemGroup : UIBase
{
    enum GameObjects
    {
        ItemPanel,
        BackgroundImage
    }
    enum Texts
    {
        ItemTypeText
    }

    private string ItemGroupName;
    private int Number = 0;



    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        Bind<Text>(typeof(Texts));

        GetText((int)Texts.ItemTypeText).text = ItemGroupName;
        GameObject itemText = GetObject((int)Texts.ItemTypeText);

        foreach (ItemProperty itemproperty in ItemProperty.ItemProperties)
        {
            if(itemproperty.PropertyType == Enum.Parse(typeof(ItemPropertyType), ItemGroupName).ToString())
            {
                for (int i = 0; i < itemproperty.ItemNumber; i++)
                {
                    GameObject item = UIManager.UI.MakeSubItem<Item>(itemText.transform, itemproperty.ItemName).gameObject;
                    Item itemscript = item.GetOrAddComponent<Item>();
                    itemscript.SetInfo(itemproperty);
                }
            }
        }

    }

    public void SetInfo(string itemtype)
    {
        this.ItemGroupName = itemtype;
    }
}

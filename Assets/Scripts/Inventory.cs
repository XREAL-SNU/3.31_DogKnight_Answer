using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XReal.XTown.UI;

public class Inventory : UIPopup
{
    enum GameObjects
    {
        ContentPanel
    }
    enum Buttons
    {
        CloseButton
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnClick_Close);

        GameObject contentpanel = GetObject((int)GameObjects.ContentPanel);

        foreach(ItemPropertyType type in Enum.GetValues(typeof(ItemPropertyType)))
        {
            GameObject itemgroup = UIManager.UI.MakeSubItem<ItemGroup>(contentpanel.transform).gameObject;
            ItemGroup itemgroupscript = itemgroup.GetOrAddComponent<ItemGroup>();
            itemgroupscript.SetInfo(type.ToString());
        };
    }

    public void OnClick_Close(PointerEventData data)
    {
        ClosePopup();
    }
}

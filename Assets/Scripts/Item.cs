using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XReal.XTown.UI;

public class Item : UIBase
{
    enum GameObjects
    {
        BackgroundImage,
        ItemImage
    }

    private ItemProperty itemProperty;

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));

        GetObject((int)GameObjects.ItemImage).BindEvent(OnClick_ItemUse);
    }

    public void OnClick_ItemUse(PointerEventData data)
    {
        if (ItemProperty.GetItemProperty(this.itemProperty.ItemName).ItemNumber >= 1)
        {
            ItemProperty.GetItemProperty(this.itemProperty.ItemName).ItemNumber -= 1;
            Destroy(gameObject);
            this.transform.parent.parent.GetComponent<ItemGroup>()._settings();
        }
    }

    public void SetInfo(ItemProperty itemProperty)
    {
        this.itemProperty = itemProperty;
    }
}

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

    GameObject itemPanel;
    GameObject itemTextBackround;
    GameObject itemText;
    GridLayoutGroup gridLayout;
    VerticalLayoutGroup verticalLayout;


    private void Start()
    {
        Init();
        _settings();
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        Bind<Text>(typeof(Texts));

        GetText((int)Texts.ItemTypeText).text = ItemGroupName;

        itemPanel = GetObject((int)GameObjects.ItemPanel);
        itemText = GetObject((int)Texts.ItemTypeText);
        itemTextBackround = GetObject((int)GameObjects.BackgroundImage);
        gridLayout = itemPanel.GetComponent<GridLayoutGroup>();
        verticalLayout = this.transform.parent.GetComponent<VerticalLayoutGroup>();

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

    private void _setGrid()
    {
        gridLayout.padding = new RectOffset(0, 0, 0, 15);
        gridLayout.cellSize = new Vector2(150, 150);
        gridLayout.spacing = new Vector2(25, 25);
        gridLayout.startCorner = GridLayoutGroup.Corner.UpperLeft;
        gridLayout.startAxis = GridLayoutGroup.Axis.Horizontal;
        gridLayout.childAlignment = TextAnchor.UpperLeft;
        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = 6;
    }

    private void _panelSize()
    {
        CalNum();
        verticalLayout.spacing = 30;
        float Width = this.transform.parent.GetComponent<RectTransform>().sizeDelta.x - verticalLayout.padding.right - verticalLayout.padding.left;
        float d = gridLayout.cellSize.y + gridLayout.spacing.y;
        float a0 = gridLayout.padding.bottom;
        float panelHeight = Mathf.CeilToInt((float)Number / gridLayout.constraintCount) * d + a0;
        float textHeight = itemTextBackround.GetComponent<RectTransform>().sizeDelta.y;
        itemPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(Width, panelHeight);
        itemPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -textHeight - panelHeight / 2 + verticalLayout.spacing / 2);
        itemTextBackround.GetComponent<RectTransform>().sizeDelta = new Vector2(Width, textHeight);
        itemTextBackround.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, - textHeight / 2);
        itemTextBackround.GetComponent<RectTransform>().sizeDelta = new Vector2(Width, textHeight);
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(Width, panelHeight + textHeight);
        verticalLayout.spacing = 0;
    }

    public void _settings()
    {
        _setGrid();
        _panelSize();
    }

    public void CalNum()
    {
        Number = 0;
        foreach (ItemProperty itemproperty in ItemProperty.ItemProperties)
        {
            if (itemproperty.PropertyType == Enum.Parse(typeof(ItemPropertyType), ItemGroupName).ToString())
            {
                for (int i = 0; i < itemproperty.ItemNumber; i++)
                {
                    Number++;
                }
            }
        }
    }
}

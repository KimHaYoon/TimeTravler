using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public ItemData item;
    public ItemInfo info;
    public Image slotItemImage;

    private void Start()
    {
        item = new ItemData();
    }

    public void SetItem(ItemData Item)
    {
        if (Item.code == 0)
        {
            item = Item;
            gameObject.name = "Empty";
            slotItemImage.gameObject.SetActive(false);
            slotItemImage.sprite = null;
            slotItemImage.name = "0";
        }

        else if (Item.code > 0)
        {
            //Debug.Log(item.code);
            item = Item;
            string code = item.code.ToString();
            gameObject.name = item.name;

            Sprite sprite = (Sprite)Resources.Load("Item/ItemStandard/" + code.Substring(0, 4), typeof(Sprite));

            slotItemImage.gameObject.SetActive(true);
            slotItemImage.sprite = sprite;
            slotItemImage.name = code.Substring(0, 4);
        }
    }

    public void OnTriggerEnter2D()
    {
        if (gameObject.name == "Empty")
            return;

        info.SetItem(item);
        info.gameObject.SetActive(true);
        //info.gameObject.GetComponent<RectTransform>().position = 

        Vector3 position = gameObject.GetComponent<RectTransform>().position;

        info.gameObject.GetComponent<RectTransform>().position = new Vector3(position.x + 140, position.y);
    }

    public void OnTriggerExit2D()
    {
        info.gameObject.SetActive(false);
    }
}

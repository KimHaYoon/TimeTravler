using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public ItemData item;
    public ItemInfo info;

    private void Start()
    {
        item = new ItemData();
    }

    public void SetItem(ItemData Item)
    {
        if (item == null)
        {
            gameObject.name = "Empty";
        }

        else
        {
            item = Item;
            string code = item.code.ToString();
            gameObject.name = item.name;

            // 리소스 수정하는거 해결하기
            
            GameObject slotItemImage = GameObject.Find(gameObject.name + "/slotItemImage");
            Image image = slotItemImage.GetComponent<Image>();

            Sprite sprite = (Sprite)Resources.Load("Item/ItemStandard/" + code.Substring(0, 4), typeof(Sprite));
            
            image.sprite = sprite;
            slotItemImage.name = code.Substring(0, 4);
        }
    }

    public void OnTriggerEnter2D()
    {
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

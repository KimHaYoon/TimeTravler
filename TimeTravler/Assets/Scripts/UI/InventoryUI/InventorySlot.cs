using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour
{
    public ItemInfo info;
    public static bool sheild = true;

    public void OnPointerClick(BaseEventData Data)
    {
        Debug.Log("클릭");
        Item_string item = this.GetComponent<Item_string>();
        PointerEventData eventData = Data as PointerEventData;

        if (eventData.button == PointerEventData.InputButton.Left)
            Debug.Log("왼쪽클릭");
        //왼쪽클릭


        if (eventData.button == PointerEventData.InputButton.Right)
        {
            //오른쪽클릭
            Debug.Log("오른쪽 클릭");
            if (Inventory.instance.isSHOP == false)
            {//아이템 장착 및 소비
                if (item.code == null)
                    return;
                if (int.Parse(item.code.Substring(0,1)) < 3)
                switch (item.code.Substring(0, 1))
                {
                    case "2":
                            Itemslot.Add(this.gameObject);
                        /*int count = int.Parse(item.code.Substring(5,2)) - 1;
                        this.GetComponentInChildren<Text>().text = (count > 0 ? " " + count : " ");

                            if (count < 1)
                            {
                                Inventory.instance.pop_list(this.GetComponent<index>().Index);
                            }
                            else
                                item.code = item.code.Substring(0, 5) + (count < 10 ? "0" + count : "" + count);
                                */
                        break;

                    case "1":
                        Debug.Log("이큅 실행");
                        equip(item);
                        break;

                }

            }
        }
    }

    public void OnTriggerEnter2D()
    {
        if (this.GetComponent<Item_string>().code != null)
        {
            info.SetItem(ItemManager.instance.GetItemInfo(int.Parse(this.GetComponent<Item_string>().code.Substring(0, 5))));
            info.gameObject.SetActive(true);
        }
        //info.gameObject.GetComponent<RectTransform>().position = 

        Vector3 position = gameObject.GetComponent<RectTransform>().position;

        info.gameObject.GetComponent<RectTransform>().position = new Vector3(position.x -120, position.y - 40);
    }

    public void OnTriggerExit2D()
    {
        info.gameObject.SetActive(false);
    }



    void equip(Item_string part)
    {
        Item_string item = part;
        string item_string = Inventory.instance.copy(item.code);
        Item_string target = null;

        if (!(item_string.Substring(1, 1) == "5" && sheild == false))
            Inventory.instance.pop_list(this.GetComponent<index>().Index);

        switch (item_string.Substring(1, 1))
        {
            case "1":
                Debug.Log("머리장착");
                target = Inventory.instance.equipment_Head.GetComponent<Item_string>();
                Debug.Log(target.code);
                if (target.code != null)
                    Inventory.instance.Add(Inventory.instance.equipment_Head.GetComponent<Item_string>().code);
                break;

            case "2":
                target = Inventory.instance.equipment_Armor.GetComponent<Item_string>();
                if (target.code != null)
                    Inventory.instance.Add(Inventory.instance.equipment_Armor.GetComponent<Item_string>().code);
                break;

            case "3":
                target = Inventory.instance.equipment_Shoes.GetComponent<Item_string>();
                if (target.code != null)
                    Inventory.instance.Add(Inventory.instance.equipment_Shoes.GetComponent<Item_string>().code);
                break;

            case "4":
                Debug.Log("무기장착");
                target = Inventory.instance.equipment_weapon.GetComponent<Item_string>();
                if (target.code != null)
                {
                    Inventory.instance.Add(Inventory.instance.equipment_weapon.GetComponent<Item_string>().code);
                }
                if (int.Parse(item_string.Substring(2, 1)) != 0)
                {
                    Debug.Log("대검이다");
                    GameObject Shield = Inventory.instance.equipment_Shield;
                    Inventory.instance.Add(Shield.GetComponent<Item_string>().code);
                    Shield.GetComponent<Image>().sprite = Inventory.instance.closeImage;
                    Shield.GetComponent<Item_string>().code = null;
                    sheild = false;
                }
                else if (Inventory.instance.equipment_Shield.GetComponent<Item_string>().code == null)
                {
                    Inventory.instance.equipment_Shield.GetComponent<Image>().sprite = Inventory.instance.defaultImage;
                    sheild = true;
                }
                break;

            case "5":
                target = Inventory.instance.equipment_Shield.GetComponent<Item_string>();
                if (target.code != null)
                    Inventory.instance.Add(Inventory.instance.equipment_Shield.GetComponent<Item_string>().code);
                break;

        }
        Debug.Log(item.code);
        if (!(item_string.Substring(1, 1) == "5" && sheild == false))
        {
            target.code = item_string;
            target.GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/ItemStandard/" + target.code.Substring(0, 4));
        }
    }
}

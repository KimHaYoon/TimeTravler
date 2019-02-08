using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour
{
    
    public void OnPointerClick(BaseEventData Data)
    {
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

    void equip(Item_string part)
    {
        Item_string item = part;
        string item_string = Inventory.instance.copy(item.code);
        Item_string target = null;

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
                    Inventory.instance.Add(Inventory.instance.equipment_weapon.GetComponent<Item_string>().code);
                break;

            case "5":
                target = Inventory.instance.equipment_Shield.GetComponent<Item_string>();
                if (target.code != null)
                    Inventory.instance.Add(Inventory.instance.equipment_Shield.GetComponent<Item_string>().code);
                break;
               
        }
        Debug.Log(item.code);
        target.code = item_string;
        target.GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/ItemStandard/" + target.code.Substring(0, 4));
    }
}

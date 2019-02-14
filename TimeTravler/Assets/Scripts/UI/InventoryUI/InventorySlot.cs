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
        {
            Debug.Log("왼쪽클릭");
            if(item.code.Substring(0,1) == "2")
            for (int i = 0; i < Itemslot.instance.slots.Count; i++)
            {
                if (Itemslot.instance.slots[i].GetComponent<Item_string>().code.Equals(item.code))
                {
                        Player.Consume(false, item.code.Substring(0, 4), InventorySlot.GetType(item.code),
                            ItemManager.instance.GetOpt1_1(int.Parse(item.code.Substring(0, 5))),
                            ItemManager.instance.GetOpt2_1(int.Parse(item.code.Substring(0, 5))));
                        Itemslot.instance.slots[i].GetComponent<ItemquickSlot>().minus_item(Itemslot.instance.slots[i].GetComponent<Item_string>());
                    break;
                }
            }

        }


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
            else
            {
                GameObject slotnow = null;
                //돈버는 코드 필요
                for (int i = 0; i < Itemslot.instance.slots.Count; i++)
                {
                    slotnow = Itemslot.instance.slots[i];
                    if (slotnow.GetComponent<Item_string>().code.Equals(item.code))
                    {
                        slotnow.GetComponent<Item_string>().code = null;
                        slotnow.GetComponent<Image>().sprite = Inventory.instance.defaultImage;
                        slotnow.GetComponentInChildren<Text>().text = " ";
                        break;
                    }
                }
                Inventory.instance.pop_list(this.GetComponent<index>().Index);
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



    void equip(Item_string item)
    {
        string part;
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
                {
                    Inventory.instance.Add(Inventory.instance.equipment_Head.GetComponent<Item_string>().code);
                    part = Inventory.instance.equipment_Head.GetComponent<Item_string>().code;
                    Player.Consume(false, part.Substring(0, 4), InventorySlot.GetType(part),
                            ItemManager.instance.GetOpt1_1(int.Parse(part.Substring(0, 5))),
                            ItemManager.instance.GetOpt2_1(int.Parse(part.Substring(0, 5))));
                }
                break;

            case "2":
                target = Inventory.instance.equipment_Armor.GetComponent<Item_string>();
                if (target.code != null)
                {
                    Inventory.instance.Add(Inventory.instance.equipment_Armor.GetComponent<Item_string>().code);
                    part = Inventory.instance.equipment_Armor.GetComponent<Item_string>().code;
                    Player.Consume(false, part.Substring(0, 4), InventorySlot.GetType(part),
                            ItemManager.instance.GetOpt1_1(int.Parse(part.Substring(0, 5))),
                            ItemManager.instance.GetOpt2_1(int.Parse(part.Substring(0, 5))));
                }
                break;

            case "3":
                target = Inventory.instance.equipment_Shoes.GetComponent<Item_string>();
                if (target.code != null)
                {
                    Inventory.instance.Add(Inventory.instance.equipment_Shoes.GetComponent<Item_string>().code);
                    part = Inventory.instance.equipment_Shoes.GetComponent<Item_string>().code;
                    Player.Consume(false, part.Substring(0, 4), InventorySlot.GetType(part),
                            ItemManager.instance.GetOpt1_1(int.Parse(part.Substring(0, 5))),
                            ItemManager.instance.GetOpt2_1(int.Parse(part.Substring(0, 5))));
                }
                break;

            case "4":
                Debug.Log("무기장착");
                target = Inventory.instance.equipment_weapon.GetComponent<Item_string>();
 
                if (int.Parse(item_string.Substring(2, 1)) == 1)
                {
                    if (Inventory.instance.current_count >= Inventory.instance.inventory_max - 1 && Inventory.instance.equipment_Shield.GetComponent<Item_string>().code != null)
                    {
                        Inventory.instance.Add(item_string);
                        return;
                    }
                    Debug.Log("대검이다");
                    GameObject Shield = Inventory.instance.equipment_Shield;
                    Inventory.instance.Add(Shield.GetComponent<Item_string>().code);
                    part = Shield.GetComponent<Item_string>().code;
                    if (part != null)
                    {
                        Player.Consume(false, part.Substring(0, 4), InventorySlot.GetType(part),
                                ItemManager.instance.GetOpt1_1(int.Parse(part.Substring(0, 5))),
                                ItemManager.instance.GetOpt2_1(int.Parse(part.Substring(0, 5))));
                    }

                    Shield.GetComponent<Image>().sprite = Inventory.instance.closeImage;
                    Shield.GetComponent<Item_string>().code = null;
                    sheild = false;
                    Skill_window.instance.slot_now = Skill_window.instance.two_slot;
                }
                else if (Inventory.instance.equipment_Shield.GetComponent<Item_string>().code == null)
                {
                    Inventory.instance.equipment_Shield.GetComponent<Image>().sprite = Inventory.instance.defaultImage;
                    sheild = true;
                    if (int.Parse(item_string.Substring(2, 1)) == 0)
                        Skill_window.instance.slot_now = Skill_window.instance.one_slot;
                    else if (int.Parse(item_string.Substring(2, 1)) == 2)
                        Skill_window.instance.slot_now = Skill_window.instance.three_slot;
                }
                if (target.code != null)
                {
                    Inventory.instance.Add(Inventory.instance.equipment_weapon.GetComponent<Item_string>().code);
                    part = Inventory.instance.equipment_weapon.GetComponent<Item_string>().code;
                    Player.Consume(false, part.Substring(0, 4), InventorySlot.GetType(part),
                            ItemManager.instance.GetOpt1_1(int.Parse(part.Substring(0, 5))),
                            ItemManager.instance.GetOpt2_1(int.Parse(part.Substring(0, 5))));
                }
                skillslot.instance.update();
                break;

            case "5":
                target = Inventory.instance.equipment_Shield.GetComponent<Item_string>();
                if (target.code != null)
                {
                    Inventory.instance.Add(Inventory.instance.equipment_Shield.GetComponent<Item_string>().code);
                    part = Inventory.instance.equipment_Shield.GetComponent<Item_string>().code;
                    Player.Consume(false, part.Substring(0, 4), InventorySlot.GetType(part),
                            ItemManager.instance.GetOpt1_1(int.Parse(part.Substring(0, 5))),
                            ItemManager.instance.GetOpt2_1(int.Parse(part.Substring(0, 5))));
                }
                break;

        }
        Debug.Log(item.code);
        if (!(item_string.Substring(1, 1) == "5" && sheild == false))
        {
            target.code = item_string;
            target.GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/ItemStandard/" + target.code.Substring(0, 4));
            Player.Consume(true, target.code.Substring(0, 4), InventorySlot.GetType(target.code),
                           ItemManager.instance.GetOpt1_1(int.Parse(target.code.Substring(0, 5))),
                           ItemManager.instance.GetOpt2_1(int.Parse(target.code.Substring(0, 5))));
        }
    }

    public static int GetType(string code)
    {
        if (code.Substring(0, 1) == "1")
            return int.Parse(code.Substring(1, 1));
        else if (code.Substring(0, 1) == "2")
            switch(code.Substring(1, 1))
            {
                case "1":
                case "2":
                    return 6;

                case "3":
                    return int.Parse(code.Substring(3, 1)) + 6;
            }
        return 0;
    }
}

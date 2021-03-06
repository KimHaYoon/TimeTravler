﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour
{
    public static bool sheild = true;

    public void OnPointerClick(BaseEventData Data)
    {
        Debug.Log("클릭");
        Item_string item = this.GetComponent<Item_string>();
        PointerEventData eventData = Data as PointerEventData;

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("왼쪽클릭");
            if(Inventory.instance.isSHOP == false)
            if (item.code != null)
                if (item.code.Substring(0, 1) == "2")
                {
                    //퀵슬롯에 아이템이 있으면 감소
                    for (int i = 0; i < Itemslot.instance.slots.Count; i++)
                    {
                        if (Itemslot.instance.slots[i].GetComponent<Item_string>().code != null)
                            if (Itemslot.instance.slots[i].GetComponent<Item_string>().code.Equals(this.GetComponent<Item_string>().code))
                            {
                                Itemslot.instance.slots[i].GetComponent<ItemquickSlot>().minus_item(Itemslot.instance.slots[i].GetComponent<Item_string>());
                                break;
                            }
                    }
                    //인벤토리의 아이템을 감소 & consume실행
                    Inventory.instance.player.Consume(false, item.code.Substring(0, 4), InventorySlot.GetType(item.code),
                                    ItemManager.instance.GetOpt1_1(int.Parse(item.code.Substring(0, 5))),
                                    ItemManager.instance.GetOpt2_1(int.Parse(item.code.Substring(0, 5))));
                    int count = int.Parse(item.code.Substring(5, 2)) - 1;
                    string count_string = ((count < 10 ? "0" + count : "" + count));
                    item.code = Inventory.instance.copy(item.code.Substring(0, 5) + count_string);
                    //해당 슬롯의 갯수출력변경
                    this.GetComponentInChildren<Text>().text = (int.Parse(item.code.Substring(5, 2)) > 0 ?
                        " " + int.Parse(item.code.Substring(5, 2)) : " ");
                    if (count <= 0)
                    {
                        Inventory.instance.pop_list(this.GetComponent<index>().Index);
                        return;
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
            ItemData temp = ItemManager.instance.GetItemInfo(int.Parse(this.GetComponent<Item_string>().code.Substring(0, 5)));
            Debug.Log(temp.name);
            Debug.Log(temp.sellprice);
            Debug.Log(temp.buyprice);
            Inventory.instance.info.SetItem(ItemManager.instance.GetItemInfo(int.Parse(this.GetComponent<Item_string>().code.Substring(0, 5))));
            Inventory.instance.info.gameObject.SetActive(true);
        }
        //info.gameObject.GetComponent<RectTransform>().position = 

        Vector3 position = gameObject.GetComponent<RectTransform>().position;

        Inventory.instance.info.gameObject.GetComponent<RectTransform>().position = new Vector3(position.x -120, position.y - 40);
    }

    public void OnTriggerExit2D()
    {
        Inventory.instance.info.gameObject.SetActive(false);
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
                    Inventory.instance.player.Consume(false, part.Substring(0, 4), InventorySlot.GetType(part),
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
                    Inventory.instance.player.Consume(false, part.Substring(0, 4), InventorySlot.GetType(part),
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
                    Inventory.instance.player.Consume(false, part.Substring(0, 4), InventorySlot.GetType(part),
                            ItemManager.instance.GetOpt1_1(int.Parse(part.Substring(0, 5))),
                            ItemManager.instance.GetOpt2_1(int.Parse(part.Substring(0, 5))));
                }
                break;

            case "4":
                Debug.Log("무기장착");
                target = Inventory.instance.equipment_weapon.GetComponent<Item_string>();
 
                if (int.Parse(item_string.Substring(2, 1)) != 0)
                {
                    if (Inventory.instance.current_count >= Inventory.instance.inventory_max - 1 && Inventory.instance.equipment_Shield.GetComponent<Item_string>().code != null)
                    {
                        Inventory.instance.Add(item_string);
                        return;
                    }
                    Debug.Log("대검이다");
                    //방패빼기
                    GameObject Shield = Inventory.instance.equipment_Shield;
                    Inventory.instance.Add(Shield.GetComponent<Item_string>().code);
                    part = Shield.GetComponent<Item_string>().code;
                    if (part != null)
                    {
                        //뺀 방패만큼 방어력 감소
                        Inventory.instance.player.Consume(false, part.Substring(0, 4), InventorySlot.GetType(part),
                                ItemManager.instance.GetOpt1_1(int.Parse(part.Substring(0, 5))),
                                ItemManager.instance.GetOpt2_1(int.Parse(part.Substring(0, 5))));
                    }
                    //방패 금지 이미지
                    Shield.GetComponent<Image>().sprite = Inventory.instance.closeImage;
                    Shield.GetComponent<Item_string>().code = null;
                    sheild = false;
                    if (int.Parse(item_string.Substring(2, 1)) == 1)
                        Skill_window.instance.slot_now = Skill_window.instance.two_slot;
                    else if (int.Parse(item_string.Substring(2, 1)) == 2)
                        Skill_window.instance.slot_now = Skill_window.instance.three_slot;
                }
                else if (Inventory.instance.equipment_Shield.GetComponent<Item_string>().code == null)
                {
                    Inventory.instance.equipment_Shield.GetComponent<Image>().sprite = Inventory.instance.defaultImage;
                    sheild = true;
                    if (int.Parse(item_string.Substring(2, 1)) == 0)
                    {
                        Debug.Log("1번슬롯으로 변경");
                        Skill_window.instance.slot_now = Skill_window.instance.one_slot;
                    }
                }
                if (target.code != null)
                {
                    Inventory.instance.Add(Inventory.instance.equipment_weapon.GetComponent<Item_string>().code);
                    part = Inventory.instance.equipment_weapon.GetComponent<Item_string>().code;
                    Inventory.instance.player.Consume(false, part.Substring(0, 4), InventorySlot.GetType(part),
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
                    Inventory.instance.player.Consume(false, part.Substring(0, 4), InventorySlot.GetType(part),
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
            Inventory.instance.player.Consume(true, target.code.Substring(0, 4), InventorySlot.GetType(target.code),
                           ItemManager.instance.GetOpt1_1(int.Parse(target.code.Substring(0, 5))),
                           ItemManager.instance.GetOpt2_1(int.Parse(target.code.Substring(0, 5))));
        }
    }

    public static int GetType(string code)
    {
        if (code.Substring(0, 1) == "1")
            return int.Parse(code.Substring(1, 1));
        else if (code.Substring(0, 1) == "2")
            return int.Parse(code.Substring(1, 1)) + 5;

        return 0;
    }
}

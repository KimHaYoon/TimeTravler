using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Itemslot : MonoBehaviour
{
    public static Itemslot instance = null;
    GameObject firstslot = null;
    GameObject secondslot = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);

        firstslot = transform.GetChild(1).gameObject;
        firstslot.GetComponent<Item_string>().code = null;
        secondslot = transform.GetChild(2).gameObject;
        secondslot.GetComponent<Item_string>().code = null;
    }

    public static void Add(GameObject input)
    {
        Inventory inventory = Inventory.instance;
        Item_string first = instance.firstslot.GetComponent<Item_string>();
        Item_string second = instance.secondslot.GetComponent<Item_string>();
        string input_string = string.Copy(input.GetComponent<Item_string>().code);

        inventory.Add(first.code);
        inventory.clone(second, first);
        second.code = input_string;// == clone
        if(first.code != null)
        instance.firstslot.GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/ItemStandard/" + first.code.Substring(0, 4));
        //instance.firstslot.GetComponentInChildren<Text>().text = int.Parse(first.code.Substring(5, 2)) > 0 ? " " + int.Parse(first.code.Substring(5, 2)) : " ";
        if (second.code != null)
            instance.secondslot.GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/ItemStandard/" + second.code.Substring(0, 4));
        //instance.secondslot.GetComponentInChildren<Text>().text = int.Parse(second.code.Substring(5, 2)) > 0 ? " " + int.Parse(second.code.Substring(5, 2)) : " ";


    }
}




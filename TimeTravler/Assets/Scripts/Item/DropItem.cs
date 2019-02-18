using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    private Player player;

    public string item;
    public int count;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Item/ItemStandard/" + item.Substring(0, 4), typeof(Sprite));//아이템 번호에 맞춰서 설정
        GetComponent<Rigidbody2D>().velocity = new Vector2(UnityEngine.Random.Range(0f, 1f), 5);
    }


    void Update()
    {
        if (Inventory.instance.current_count >= 30)
        {
            Physics2D.IgnoreLayerCollision(10, 12, true);//충돌무시
        }
        else
        {
            Physics2D.IgnoreLayerCollision(10, 12, false);//충돌무시
        }
    }



}

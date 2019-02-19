using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrag : MonoBehaviour
{
    
    private bool drag = false;


    private string item;

    void Start()
    {
        item = transform.parent.GetComponent<DropItem>().item;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!drag)
            {
                if (Inventory.instance.current_count < 30)
                {
                    Inventory.instance.Add(item);
                    StartCoroutine(Drag(other.gameObject));
                    drag = true;
                }
            } 
        }
    }
    IEnumerator Drag(GameObject player)//아이템 드래그 코루틴
    {
        yield return new WaitForSeconds(0.3f);
        Vector3 distance = player.transform.position - transform.parent.position;
        for (int i = 0; i < 20; i++)
        {
            if(i < 10)
                transform.parent.GetComponent<Rigidbody2D>().velocity = distance * 2 + new Vector3(0f, 2f, 0f);
            else
                transform.parent.GetComponent<Rigidbody2D>().velocity = distance * 2 + new Vector3(0f, -2f, 0f);
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(transform.parent.gameObject);
    }
}

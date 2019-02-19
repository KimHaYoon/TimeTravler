using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSight : MonoBehaviour
{
    
    private GameObject monster;

    void Awake()
    {
        monster = transform.parent.Find("Monster").gameObject;
    }

    void Update()
    {
        transform.position = monster.transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
            monster.GetComponent<Monster>().TargetIn(other);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
            monster.GetComponent<Monster>().TargetStay(other);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
            monster.GetComponent<Monster>().TargetOut(other);
    }
}

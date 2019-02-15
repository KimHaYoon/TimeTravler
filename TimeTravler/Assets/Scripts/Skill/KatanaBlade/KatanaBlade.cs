using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaBlade : PlayerSkill
{
    [SerializeField]
    private GameObject katanaBladeHitPrefab;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        OffCollider();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Monster")
        {
            Monster monster = other.gameObject.transform.parent.GetComponent<Monster>();
            if (!monster.superArmor)
            {
                monster.Hurt(skillUser, isKnockBack, isCritical, damageRatio);
                Vector3 pos = monster.transform.position;
                if (transform.position.x - pos.x < 0)
                {
                    monster.direction = true;
                    if (monster.moveType)
                        pos.x += 0.1f;
                }
                else
                {
                    monster.direction = false;
                    if (monster.moveType)
                        pos.x -= 0.1f;
                }
                monster.transform.position = pos;

                Instantiate(katanaBladeHitPrefab, other.transform.position, Quaternion.identity);
            }
        }
    }

    void OnCollider()
    {
        myCollider.enabled = true;
    }

    void OffCollider()
    {
        myCollider.enabled = false;
    }
}

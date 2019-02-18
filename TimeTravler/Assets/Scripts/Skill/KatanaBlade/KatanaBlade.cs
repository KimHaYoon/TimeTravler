using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaBlade : PlayerSkill
{
    [SerializeField]
    private GameObject katanaBladeHitPrefab;
    private Player player;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        OffCollider();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
            transform.position = GameObject.Find("PlayerPart").transform.position + GameObject.Find("Pos_KatanaBlade").transform.position;

        if (player.facingLeft)
            transform.position -= new Vector3(GameObject.Find("Pos_KatanaBlade").transform.position.x * 2, 0, 0);
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

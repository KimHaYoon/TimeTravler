using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum pos_type
{
    top,
    bottom,
    left,
    right
}

public class balls : MonoBehaviour
{
    [SerializeField]
    private FlareBall flareBallPrefab;

    [SerializeField]
    private FlareBall_Hit flareBallHitPrefab;

    [SerializeField]
    private pos_type posType;

    private Vector3 tmp;
    private float accumulate_dist;

    void Start()
    {
        accumulate_dist = 0f;
    }
    
    void FixedUpdate()
    {
        if (flareBallPrefab.isShoot)
        {
            Shoot();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Monster")
        {
            Monster monster = other.gameObject.transform.parent.GetComponent<Monster>();
            if (!monster.superArmor)
            {
                monster.Hurt(flareBallPrefab.skillUser, flareBallPrefab.isKnockBack, 
                    flareBallPrefab.isCritical, flareBallPrefab.damageRatio);
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
            }

            Instantiate(flareBallHitPrefab, transform.position, Quaternion.identity);

            flareBallPrefab.DecreaseBallCount();
            Destroy(gameObject);
        }
    }

    void Shoot()
    {
        if (posType == pos_type.top)
        {
            tmp = transform.localPosition;
            tmp.y += flareBallPrefab.spreadSpeed;
        }
        else if (posType == pos_type.bottom)
        {
            tmp = transform.localPosition;
            tmp.y -= flareBallPrefab.spreadSpeed;
        }
        else if (posType == pos_type.left)
        {
            tmp = transform.localPosition;
            tmp.x -= flareBallPrefab.spreadSpeed;
        }
        else if (posType == pos_type.right)
        {
            tmp = transform.localPosition;
            tmp.x += flareBallPrefab.spreadSpeed;
        }
        transform.localPosition = tmp;

        accumulate_dist += flareBallPrefab.spreadSpeed;
        if (accumulate_dist > flareBallPrefab.limitDist)
        {
            flareBallPrefab.DecreaseBallCount();
            Destroy(gameObject);
        }
    }
}

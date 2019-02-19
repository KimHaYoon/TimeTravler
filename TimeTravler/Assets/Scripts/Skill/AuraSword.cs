using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraSword : PlayerSkill
{
    [SerializeField]
    private float maxRange;

    [SerializeField]
    private float speed;

    [SerializeField]
    private GameObject auraSwordHitPrefab;

    private Vector2 startPos;

    private Rigidbody2D myRigidbody;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        startPos = transform.position;
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    public override void FixedUpdate()
    {
        // 속도만큼 이동시킨다
        myRigidbody.velocity = direction * speed;
        RangeCheck();
    }

    void RangeCheck()
    {
        // 스킬객체가 최대사거리를 벗어나면 제거한다
        if (direction.x > Vector2.zero.x && transform.position.x < startPos.x + maxRange)
            return;
        else if (direction.x < Vector2.zero.x && transform.position.x > startPos.x - maxRange)
            return;

        Destroy(gameObject);
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

                if (direction == Vector2.right)
                    Instantiate(auraSwordHitPrefab, other.transform.position, Quaternion.identity);
                else if (direction == Vector2.left)
                    Instantiate(auraSwordHitPrefab, other.transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
            }
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("NoPassGround"))
        {
            Destroy(gameObject);
        }
    }
}

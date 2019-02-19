using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraSword : PlayerSkill
{
    [SerializeField]
    private float maxRange;

    [SerializeField]
    private float speed;

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
}

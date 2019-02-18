using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareBall : PlayerSkill
{
    [SerializeField]
    private float rotateSpeed;

    [SerializeField]
    private int ballsCount = 4;

    //[SerializeField]
    //private balls[] fBalls;
    
    public float spreadSpeed;
    public float limitDist;
    public bool isShoot;

    // Start is called before the first frame update
    public override void Start()
    {
        isShoot = false;
    }

    // Update is called once per frame
    public override void FixedUpdate()
    {
        if (!isShoot)
        {
            transform.position = skillUser.transform.position;
        }
        transform.Rotate(Vector3.forward, rotateSpeed);
    }

    public void DecreaseBallCount()
    {
        ballsCount -= 1;
        if (ballsCount <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Shoot()
    {
        isShoot = true;
    }
}

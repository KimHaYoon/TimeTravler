using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleSlash1 : PlayerSkill
{

    [SerializeField]
    private GameObject doubleSlash2Prefab;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void EndAnimation()
    {
        // DoubleSlash2 생성
        if (direction == Vector2.left)
        {
            GameObject tmp = Instantiate(doubleSlash2Prefab, skillUser.transform.position, Quaternion.Euler(new Vector3(0, 180, 0)));
            if (tmp)
                tmp.GetComponent<PlayerSkill>().Initialize(this.gameObject, Vector2.left);
        }
        else if (direction == Vector2.right)
        {
            GameObject tmp = Instantiate(doubleSlash2Prefab, skillUser.transform.position, Quaternion.identity);
            if (tmp)
                tmp.GetComponent<PlayerSkill>().Initialize(this.gameObject, Vector2.right);
        }

        base.EndAnimation();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerSkill : MonoBehaviour
{
    public float damageRatio;
    
    public bool isKnockBack;
    
    public bool isCritical;

    

    [HideInInspector]
    public GameObject skillUser;

    protected Vector2 direction;

    protected Collider2D myCollider;

    private LayerMask layerMask;

    // Start is called before the first frame update
    public virtual void Start()
    {
        myCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void FixedUpdate()
    {
        
    }

    public void Initialize(GameObject other, Vector2 direction)
    {
        // 초기화시 스킬사용자, 방향, 레이어 할당
        skillUser = other;
        this.direction = direction;

        //if (other.tag == "Player")
        //{
        //    gameObject.layer = LayerMask.NameToLayer("Player");
        //}
        //else if (other.tag == "Monster")
        //{
        //    gameObject.layer = LayerMask.NameToLayer("Monster");
        //}
    }

    //protected IEnumerator SkillCasting(float durationTime)
    //{
    //    yield return new WaitForSeconds(durationTime);

    //    Destroy(gameObject);
    //}

    public virtual void OnTriggerEnter2D(Collider2D other)
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
            }
        }
    }

    public virtual void EndAnimation()
    {
        Destroy(gameObject);
    }
}

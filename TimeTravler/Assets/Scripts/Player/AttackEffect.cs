using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    private void CreateAttackEffect()
    {
        transform.Find("Body").transform.Find("Weapon").transform.Find("Effect").gameObject.SetActive(true);
        transform.parent.transform.Find("PlayerAttackCollider").gameObject.SetActive(true);
    }

    private void DestroyAttackEffect()
    {
        transform.Find("Body").transform.Find("Weapon").transform.Find("Effect").gameObject.SetActive(false);
        transform.parent.transform.Find("PlayerAttackCollider").gameObject.SetActive(false);
    }
}

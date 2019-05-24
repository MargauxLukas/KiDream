using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossDeath : MonoBehaviour
{
    private Animator anim;
    public Animator bossAnim;
    public GameObject brossGrelotin;
    public GameObject boss;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void DeathAnim()
    {
        anim.SetBool("isDeath", true);
        StartCoroutine(waitBeforeInstantiate());
    }

    IEnumerator waitBeforeInstantiate()
    {
        yield return new WaitForSeconds(5f);
        bossAnim.SetBool("isScenePost", true);
        Instantiate(brossGrelotin, boss.transform);
    }
}

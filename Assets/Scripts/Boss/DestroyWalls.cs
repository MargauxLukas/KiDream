using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWalls : MonoBehaviour
{
    Animator animator;
    GameObject boss;
    bool isRage = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        boss = GameObject.Find("Boss");
    }
    void Update ()
    {
        isRage = boss.GetComponent<Boss>().isRage;

        if (isRage)
        {
            WallsEvent();
        }
	}

    void WallsEvent()
    {
        if (gameObject.name.Contains("Arrière") || gameObject.name.Contains("Estrade"))
        {
            animator.SetBool("isFalling", true);
            Destroy(gameObject, 1.625f);
        }
        else
        {
            StartCoroutine(WaitWallEvent());
        }
    }

    IEnumerator WaitWallEvent()
    {
        yield return new WaitForSeconds(0.9f);
        animator.SetBool("isFalling", true);
        Destroy(gameObject, 1.625f);

    }
}

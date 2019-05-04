using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAOE : MonoBehaviour
{
    Animator animator;
    public GameObject bomb;
    public GameObject fallingBomb;
    public GameObject ombreBomb;

    Vector2[,] bombTab = new Vector2[7, 4];

    bool isPlayed = false;

    public void Start()
    {
        animator = GetComponent<Animator>();

        float x = -3f;
        float y =  2f;
        for (int coll=0; coll<=3; coll++)
        {
            for (int ligne = 0; ligne <= 6; ligne++)
            {
                bombTab[ligne,coll] = new Vector2(x,y);
                x++;
            }
            y--;
            x = -3f;
        }
    }

    public void BombArea() //Test de tableau 
    {
        if (!isPlayed)
        {
            for (int ligne = 0; ligne <= 3; ligne++)
            {
                for (int coll = 0; coll <= 6; coll++)
                {
                    if ((coll == 0 && ligne == 0) || (coll == 6 && ligne == 0)
                                                  || (coll == 0 && ligne == 3)
                                                  || (coll == 6 && ligne == 3)
                                                  || (coll == 5 && ligne == 3)
                                                  || (coll == 1 && ligne == 3)) { }
                    else
                    {
                        Instantiate(bomb, bombTab[coll, ligne], Quaternion.identity);
                        animator.SetBool("isMoving", false);
                    }
                }
            }
        }
        isPlayed = true;
    }

    public void BombAreaRight() //Test tableau Right
    {
        if (!isPlayed)
        {
            for (int ligne = 0; ligne <= 3; ligne++)
            {
                for (int coll = 3; coll <= 6; coll++)
                {
                    if ((coll == 0 && ligne == 0) || (coll == 6 && ligne == 0)
                                                  || (coll == 0 && ligne == 3)
                                                  || (coll == 6 && ligne == 3)
                                                  || (coll == 5 && ligne == 3)
                                                  || (coll == 1 && ligne == 3)) {}
                    else
                    {
                        Instantiate(bomb, bombTab[coll, ligne], Quaternion.identity);
                        Instantiate(bomb, bombTab[coll, ligne], Quaternion.identity);
                        animator.SetBool("isMoving", false);
                    }
                }
            }
        }
        isPlayed = true;
    }

    public void BombAreaLeft() //Test tableau Left
    {
        float positionX = -3f;
        float positionY = 2f;
        if (!isPlayed)
        {
            for (int ligne = 0; ligne <= 3; ligne++)
            {
                for (int coll = 0; coll <= 3; coll++)
                {
                    if ((coll == 0 && ligne == 0) || (coll == 6 && ligne == 0)
                                                  || (coll == 0 && ligne == 3)
                                                  || (coll == 6 && ligne == 3)
                                                  || (coll == 5 && ligne == 3)
                                                  || (coll == 1 && ligne == 3)) {}
                    else
                    {
                        GameObject fBomb = Instantiate(fallingBomb, new Vector2(positionX, positionY+5f), Quaternion.identity);
                        fBomb.GetComponent<BombFalling>().target    = bombTab[coll, ligne];
                        fBomb.GetComponent<BombFalling>().positionX = positionX           ;
                        //fBomb.GetComponent<BombFalling>().positionY = positionY+5f;

                        //Instantiate(bomb, bombTab[ligne, coll], Quaternion.identity);

                        animator.SetBool("isMoving", false);
                    }
                }

                positionX++;
                positionY--;
            }
        }
        isPlayed = true;
    }

    public void BombAreaLeftToRight()
    {
        if (!isPlayed)
        {
            StartCoroutine(WaitAreaLTR());
        }
        isPlayed = true;
    }

    IEnumerator WaitAreaLTR()
    {
        float positionX = -3f;
        float positionY = 2f;
        for (int coll = 0; coll <= 3; coll++)
        {
            for (int ligne = 0; ligne <= 3; ligne++)
            {
                if ((coll == 0 && ligne == 0) || (coll == 6 && ligne == 0)
                                              || (coll == 0 && ligne == 3)
                                              || (coll == 6 && ligne == 3)
                                              || (coll == 5 && ligne == 3)
                                              || (coll == 1 && ligne == 3)) { }
                else
                {
                    GameObject fBomb = Instantiate(fallingBomb, new Vector2(positionX, positionY + 5f), Quaternion.identity);
                    GameObject shadowBomb = Instantiate(ombreBomb, bombTab[coll , ligne], Quaternion.identity);
                    fBomb.GetComponent<BombFalling>().target = bombTab[coll, ligne];
                    fBomb.GetComponent<BombFalling>().positionX = positionX;

                }
                positionY--;
            }
            yield return new WaitForSeconds(1f);

            positionY = 2f;
            positionX++;
        }
    }

    public void BombAreaRightToLeft()
    {

    }
}

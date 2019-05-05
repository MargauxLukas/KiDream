using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAOE : MonoBehaviour
{
    Animator animator;

    [Header("GameObject to attached")]
    public GameObject bomb       ;
    public GameObject fallingBomb;
    public GameObject shadowBomb ;

    Vector2[,] bombTab = new Vector2[7, 4];  //Tableau des positions des bombes
    float[,] bombCenterTab = new float[8, 2] { {   0f , 1.5f }, 
                                               { -0.5f, 1.4f }, 
                                               { -0.7f,   1f }, 
                                               { -0.5f, 0.6f }, 
                                               {    0f, 0.5f }, 
                                               {  0.5f, 0.6f }, 
                                               {  0.7f,   1f }, 
                                               {  0.5f, 1.4f }};


    bool isPlayed1 = false;
    bool isPlayed2 = false;
    bool isPlayed3 = false;

    public void Start()
    {
        animator = GetComponent<Animator>();

        float x = -3f;
        float y =  2f;
        for (int ligne=0; ligne<=3; ligne++)
        {
            for (int coll = 0; coll <= 6; coll++)
            {
                bombTab[coll,ligne] = new Vector2(x,y);
                x++;
            }
            y--;
            x = -3f;
        }
    }

    /*******************************************
    * Function : Bombe partout / Sert de test  *
    ********************************************/
    public void BombArea()
    {
        if (!isPlayed1)
        {
            for (int ligne = 0; ligne <= 3; ligne++)
            {
                for (int coll = 0; coll <= 6; coll++)
                {
                    if ((coll == 0 && ligne == 0) || (coll == 6 && ligne == 0)
                                                  || (coll == 0 && ligne == 3)
                                                  || (coll == 6 && ligne == 3)
                                                  || (coll == 5 && ligne == 3)
                                                  || (coll == 1 && ligne == 3)) {}
                    else
                    {
                        Instantiate(bomb, bombTab[coll, ligne], Quaternion.identity);
                        animator.SetBool("isMoving", false);
                    }
                }
            }
        }
        isPlayed1 = true;
    }

    /****************************************************
    * Function : Bombe sur la partie droite de la salle *
    *****************************************************/
    public void BombAreaRight()
    {
        if (!isPlayed1)
        {
            for (int ligne = 0; ligne <= 3; ligne++)
            {
                for (int coll = 3; coll <= 6; coll++)
                {
                    if ((coll == 6 && ligne == 0) || (coll == 6 && ligne == 3)
                                                  || (coll == 5 && ligne == 3)){}
                    else
                    {
                        Instantiate(bomb, bombTab[coll, ligne], Quaternion.identity);
                        animator.SetBool("isMoving", false);
                    }
                }
            }
        }
        isPlayed1 = true;
    }

    /****************************************************
    * Function : Bombe sur la partie gauche de la salle *
    *****************************************************/
    public void BombAreaLeft() //Test tableau Left
    {
        float positionX = -3f;
        float positionY =  2f;

        if (!isPlayed1)
        {
            for (int ligne = 0; ligne <= 3; ligne++)
            {
                for (int coll = 0; coll <= 3; coll++)
                {
                    if ((coll == 0 && ligne == 0) || (coll == 0 && ligne == 3)
                                                  || (coll == 1 && ligne == 3)) {}
                    else
                    {
                        GameObject fBomb = Instantiate(fallingBomb, new Vector2(positionX, positionY+5f), Quaternion.identity);
                        fBomb.GetComponent<BombFalling>().target = bombTab[coll, ligne];
                        animator.SetBool("isMoving", false);
                    }
                }
                positionX++;
                positionY--;
            }
        }
        isPlayed1 = true;
    }

    /*************************************************************
    * Function : Bombe tombe de gauche à droite jusqu'au millieu *
    **************************************************************/
    public void BombAreaLeftToRight()
    {
        if (!isPlayed1)
        {
            StartCoroutine(WaitAreaLTR());
        }
        isPlayed1 = true;
    }

    IEnumerator WaitAreaLTR()
    {
        float positionX = -3f;
        float positionY =  2f;

        for (int coll = 0; coll <= 3; coll++)
        {
            for (int ligne = 0; ligne <= 3; ligne++)
            {
                if ((coll == 0 && ligne == 0) || (coll == 0 && ligne == 3)
                                              || (coll == 1 && ligne == 3)) {}
                else
                {
                    GameObject fBomb  = Instantiate(fallingBomb, new Vector2(positionX, positionY + 5f), Quaternion.identity);
                    GameObject shadow = Instantiate(shadowBomb , bombTab[coll , ligne]                 , Quaternion.identity);
                    fBomb.GetComponent<BombFalling>().target     = bombTab[coll, ligne];
                    fBomb.GetComponent<BombFalling>().shadowBomb = shadow              ;
                }
                positionY--;
            }
            yield return new WaitForSeconds(1f); // Temps entre chaque colonne de bombe //A réduire si on veut que se soit plus rapide

            positionY = 2f;
            positionX++   ;
        }
    }

    /*************************************************************
    * Function : Bombe tombe de droite à gauche jusqu'au millieu *
    **************************************************************/
    public void BombAreaRightToLeft()
    {
        if (!isPlayed1)
        {
            StartCoroutine(WaitAreaRTL());
        }
        isPlayed1 = true;
    }

    IEnumerator WaitAreaRTL()
    {
        float positionX = 3f;
        float positionY = 2f;
        for (int coll = 6; coll >= 3; coll--)
        {
            for (int ligne = 0; ligne <= 3; ligne++)
            {
                if ((coll == 6 && ligne == 0) || (coll == 6 && ligne == 3)
                                              || (coll == 5 && ligne == 3)) {}
                else
                {
                    GameObject fBomb  = Instantiate(fallingBomb, new Vector2(positionX, positionY + 5f), Quaternion.identity);
                    GameObject shadow = Instantiate(shadowBomb , bombTab[coll, ligne]                  , Quaternion.identity);
                    fBomb.GetComponent<BombFalling>().target     = bombTab[coll, ligne];
                    fBomb.GetComponent<BombFalling>().shadowBomb = shadow              ;

                }
                positionY--;
            }
            yield return new WaitForSeconds(1f); // Temps entre chaque colonne de bombe //A réduire si on veut que se soit plus rapide

            positionY = 2f;
            positionX--   ;
        }
    }

    public void BombAreaMiddle()
    {
        if(!isPlayed2)
        {
            StartCoroutine(WaitAreaM());
        }
        isPlayed2 = true;
    }

    IEnumerator WaitAreaM()
    {
        float positionX = 0f;
        float positionY = 2.50f;

        for (int i = 0; i <= 8; i++)
        {
            GameObject fBomb = Instantiate(fallingBomb, new Vector2(positionX, positionY + 5f), Quaternion.identity);
            GameObject shadow = Instantiate(shadowBomb, new Vector2(positionX, positionY), Quaternion.identity);
            fBomb.GetComponent<BombFalling>().target = new Vector2(positionX, positionY);
            fBomb.GetComponent<BombFalling>().shadowBomb = shadow;
            fBomb.GetComponent<BombFalling>().explosionTime = 20f;

            positionY = positionY - 0.5f;

            yield return new WaitForSeconds(0.2f); // Temps entre chaque colonne de bombe //A réduire si on veut que se soit plus rapide
        }
    }

    public void BombAreaCenter()
    {
        if (!isPlayed3)
        {
            StartCoroutine(WaitAreaC());
        }
        isPlayed3 = true;
    }

    IEnumerator WaitAreaC() //Certainement moyen de faire un calcul mais flemme
    {
        int coll = 0;

        for (int i = 0; i <= 8; i++)
        {
            GameObject fBomb = Instantiate(fallingBomb, new Vector2(bombCenterTab[i, coll], bombCenterTab[i, coll + 1] + 5f), Quaternion.identity);
            GameObject shadow = Instantiate(shadowBomb, new Vector2(bombCenterTab[i, coll], bombCenterTab[i, coll + 1]), Quaternion.identity);
            fBomb.GetComponent<BombFalling>().target  = new Vector2(bombCenterTab[i, coll], bombCenterTab[i, coll + 1]);
            fBomb.GetComponent<BombFalling>().shadowBomb = shadow;
            fBomb.GetComponent<BombFalling>().explosionTime = 20f;
        }

        yield return new WaitForSeconds(1f);
    } 
}

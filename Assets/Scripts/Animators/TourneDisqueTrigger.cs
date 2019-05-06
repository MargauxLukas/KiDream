using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourneDisqueTrigger : MonoBehaviour
{
    Animator musicAnim;

    public AudioSource as0;
    public AudioSource as1;

    public CharacterController myPlayer;

    bool isPlaying = false;

    void Start()
    {
        musicAnim = GetComponentInParent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && isPlaying == false)
        {
            musicAnim.SetBool("isTouched", true);

            StopAllCoroutines();

            if(myPlayer.isDream == true)
            {
                as0.volume = 0.3f;
                as0.Play();
            }
            else
            {
                as0.volume = 0.3f;
                as0.Stop();
            }
            as1.volume = 0.3f;
            as1.Play();

            isPlaying = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            musicAnim.SetBool("isTouched", false);
            StartCoroutine(VolumeDown());

            isPlaying = false;
        }
    }

    IEnumerator VolumeDown()
    {

        for(float i = as0.volume; i != 0; i = i-0.01f)
        {
            Debug.Log(i);
            yield return new WaitForSeconds(0.2f);
            as0.volume = as0.volume - 0.01f;
            as1.volume = as1.volume - 0.01f;
            if (i < 0.01f)
            {
                as0.Stop();
                as1.Stop();
                break;
            }
        }

    }
}

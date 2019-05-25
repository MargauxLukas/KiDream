using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public GameObject Player;
    public GameObject playerDeath;
    public GameObject camera;
    public GameObject restartInfos;
    private Animator pAnimator;
    private Animator animator;
    private AudioSource audioS;

	void Start ()
    {
        audioS = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        pAnimator = playerDeath.GetComponent<Animator>();
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button6))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void deathScreen()
    {
        if (camera != null)
        {
            camera.GetComponent<AudioSource>().volume = 0f;
        }
        audioS.Play(0);
        animator.SetBool("isDead", true);
        pAnimator.SetBool("isDead", true);
        pAnimator.SetBool("isDream", Player.GetComponent<CharacterController>().isDream);
        StartCoroutine(WaitForFade());

    }

    IEnumerator WaitForFade()
    {
        yield return new WaitForSeconds(1);
        restartInfos.SetActive(true);
    }
}

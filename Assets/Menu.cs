using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private bool selectorInUse;

    public SpriteRenderer indicator;

    public GameObject frenchFlag;
    public GameObject englishFlag;

    //public static bool isFrench;

    public GameObject progressionBar;
    //public GameObject finalPosition;

    public AudioSource as0;
    public AudioSource as1;

    public Animation loadingScreenPop;

    public Animator animator;

    public MenuReaction menuReaction;

    private int levelIndex;

    void Awake()
    {
        StartCoroutine(AvoidDoubleClick(0.5f));
    }

    void OnTriggerStay2D (Collider2D collision)
    {
        float selector = Input.GetAxisRaw("ShootParticles");

        if(indicator != null)
        {
            indicator.enabled = true;
        }

        if(selector > 0 && selectorInUse == false)
        {
            selectorInUse = true;

            if (this.CompareTag("LanguageButton"))
            {
                ChangeLanguage();
            }

            switch (menuReaction)
            {
                case MenuReaction.MainMenu:
                    FadeToLevel(1);
                    break;
                case MenuReaction.PlayMenu:
                    FadeToLevel(2);
                    break;
                case MenuReaction.NewGame:
                    StartCoroutine(LoadAsynchronously(11));
                    break;
                case MenuReaction.ChapterMenu:
                    FadeToLevel(4);
                    break;
                case MenuReaction.OptionsMenu:
                    FadeToLevel(5);
                    OnFadeComplete();
                    break;
                case MenuReaction.Credits:
                    FadeToLevel(6);
                    break;
                case MenuReaction.QuitGame:
                    QuitGame();
                    break;
                case MenuReaction.Null:
                    break;
            }
        }
        else if(selector == 0)
        {
            selectorInUse = false;
        }
    }

    public void FadeToLevel(int i)
    {
        levelIndex = i;
        animator.SetTrigger("FadeIn_To_FadeOut");
        StartCoroutine(OnFadeComplete());
    }

    IEnumerator OnFadeComplete()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(as0.isPlaying == false)
        {
            as0.Play();
        }
        else if(as0.isPlaying == true)
        {
            if (as1.isPlaying == true)
            {
                return;
            }
            else
            {
                as1.Play();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        indicator.enabled = false;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Goodbye !");
    }

    IEnumerator LoadAsynchronously(int index)
    {
        loadingScreenPop.Play();
        Debug.Log(loadingScreenPop.clip.length);
        yield return new WaitForSeconds(loadingScreenPop.clip.length);
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);

        while(operation.isDone == false)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            Debug.Log(progress);
            progressionBar.transform.localPosition = new Vector3((-3.18636f * progress - 3.18636f), progressionBar.transform.localPosition.y, progressionBar.transform.localPosition.z);

            yield return null;
        }
    }

    IEnumerator AvoidDoubleClick(float time)
    {
        selectorInUse = true;
        yield return new WaitForSeconds(time);
        selectorInUse = false;
    }
    //205 - 611
    // -700 - 0
    //-3.186363

    public void ChangeLanguage()
    {
        if(GameLanguage.lang == Language.english)
        {
            englishFlag.SetActive(false);
            frenchFlag.SetActive(true);

            Debug.Log("Change language to French");
            GameLanguage.lang = Language.french;
        }
        else if(GameLanguage.lang == Language.french)
        {
            frenchFlag.SetActive(false);
            englishFlag.SetActive(true);

            Debug.Log("Change language to English");
            GameLanguage.lang = Language.english;
        }
    }

}

public enum MenuReaction {MainMenu, PlayMenu, NewGame, ChapterMenu, OptionsMenu, Credits, QuitGame, Null}
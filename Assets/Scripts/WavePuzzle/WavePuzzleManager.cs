using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class WavePuzzleManager : MonoBehaviour
{
    public Key1 key1;
    public Key2 key2;
    public Key3 key3;
    public Key4 key4;
    
    private int rotationSpeed = 10;
    private float angle = 0f;

    [SerializeField]
    private int waveEmissionCooldown;

    [SerializeField]
    private bool canLaunchWaves = false;
    private bool moveTarget = true;

    private bool isPlayingWinningSong = false;

    public bool wavePuzzleComplete = false;

    private bool puzzleEnded = false;

    public Transform targetRotator;
    public Transform waveTarget;
    public SpriteRenderer pressX;
    public ParticleSystem waveParticles;
    public Transform waveParticlesTransform;
    public CharacterController myPlayer;

    public List<AudioClip> DongSoundsList = new List<AudioClip>();

    public AudioSource winningSound;
    public AudioSource winningSound2;

    public AudioMixer myMixer;

    void Update()
    {
        PitchTweaker();

        StartCoroutine("WaitForNewNote");
        StartCoroutine("WaitForNewNote2");
        Emitter();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && puzzleEnded == false)
        {
            canLaunchWaves = true;

            InvokeRepeating("MoveWaveTarget", 0.01f, 0.01f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && puzzleEnded == false)
        {
            pressX.enabled = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            pressX.enabled = false;
            canLaunchWaves = false;

            CancelInvoke();
        }
    }

    public void Emitter()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button2) && canLaunchWaves == true && puzzleEnded == false)
        {
            Vector2 rotationVector = new Vector2(waveTarget.position.x - waveParticlesTransform.position.x, waveTarget.position.y - waveParticlesTransform.position.y);
            float angleValue = Mathf.Atan2(rotationVector.normalized.y, rotationVector.normalized.x) * Mathf.Rad2Deg;

            ParticleSystem.ShapeModule wpshape = waveParticles.shape;
            wpshape.rotation = new Vector3(0, 0, angleValue);

            waveParticles.Emit(1);
            StartCoroutine(WaitBeforeWaveAgain());
        }
    }

    public void MoveWaveTarget()
    {

        Vector2 rotationVector = new Vector2(waveParticlesTransform.position.x - waveTarget.position.x, waveParticlesTransform.position.y - waveTarget.position.y);
        float angleValue = Mathf.Atan2(rotationVector.normalized.y, rotationVector.normalized.x) * Mathf.Rad2Deg;

        if(Input.GetKey("joystick 1 button 5"))
        {
            angle = angle - 0.5f;
        }
        else if (Input.GetKey("joystick 1 button 4"))
        {
            angle = angle + 0.5f;
        }

        targetRotator.localEulerAngles = new Vector3(0, 0, angle);

    }


    /*public void MoveWaveTargetAutomatic()
    {

        waveTarget.RotateAround(new Vector3(-1.441f, -9.750812f, 0), Vector3.forward, rotationSpeed * Time.deltaTime);

    }*/

    public void ResetPuzzle()
    {
        key1.key1Done = false;
        key2.key2Done = false;
        key3.key3Done = false;
        key4.key4Done = false;
    }

    IEnumerator WaitBeforeWaveAgain()
    {
        canLaunchWaves = false;
        yield return new WaitForSeconds(waveEmissionCooldown);
        ResetPuzzle();
        canLaunchWaves = true;
    }

    IEnumerator WaitForNewNote()
    {

        // Musique de fin Reve
        if (wavePuzzleComplete == true && myPlayer.reve == true)
        {
            myMixer.SetFloat("MyPitch", 1f);

            puzzleEnded = true;

            isPlayingWinningSong = true;
            for (int i = 0; i < DongSoundsList.Count/2; i++)
            {
                wavePuzzleComplete = false;
                yield return new WaitForSeconds(1f);

                winningSound.clip = DongSoundsList[Random.Range(0, DongSoundsList.Count)];
                winningSound.Play();               
            }
        }

        //Musique de fin Cauchermar
        else if (wavePuzzleComplete == true && myPlayer.reve == false)
        {
            myMixer.SetFloat("MyPitch", 1.60f);

            puzzleEnded = true;
            isPlayingWinningSong = true;

            for (int i = 0; i < DongSoundsList.Count / 2; i++)
            {
                wavePuzzleComplete = false;
                yield return new WaitForSeconds(1f);

                winningSound.clip = DongSoundsList[Random.Range(0, DongSoundsList.Count)];
                winningSound.Play();
            }
        }
    }

    IEnumerator WaitForNewNote2()
    {
        //Musique décalée Reve
        if (isPlayingWinningSong == true && myPlayer.reve == true)
        {
            myMixer.SetFloat("MyPitch", 1f);

            for (int j = 0; j < DongSoundsList.Count/2; j++)
            {
                isPlayingWinningSong = false;
                wavePuzzleComplete = false;
                yield return new WaitForSeconds(0.5f);

                winningSound2.clip = DongSoundsList[Random.Range(0, DongSoundsList.Count)];
                winningSound2.Play();

                yield return new WaitForSeconds(0.5f);
            }
        }

        //Musique décalée Cauchemar
        else if (isPlayingWinningSong == true && myPlayer.reve == false)
        {
            myMixer.SetFloat("MyPitch", 1.60f);

            for (int j = 0; j < DongSoundsList.Count / 2; j++)
            {
                isPlayingWinningSong = false;
                wavePuzzleComplete = false;
                yield return new WaitForSeconds(0.5f);

                winningSound2.clip = DongSoundsList[Random.Range(0, DongSoundsList.Count)];
                winningSound2.Play();

                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    public void PitchTweaker()
    {
        if (myPlayer.reve == true)
        {
            myMixer.SetFloat("MyPitch", 1f);
        }
        else if (myPlayer.reve == false)
        {
            myMixer.SetFloat("MyPitch", 1.60f);
        }
    }

}

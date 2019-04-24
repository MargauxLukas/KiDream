using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public GameObject player;
    public GameObject CubeD;
    public GameObject fluff;

    public Camera mainCamera;
    public Camera cubeCamera;
    public Camera playerCamera;

    private Vector3 offset;
    private Vector3 offsetCube;

    private bool bSpawnEnemy;

    void Start()
    {
        offset = transform.position - player.transform.position;
        cubeCamera.enabled = false;
        playerCamera.enabled = false;
        mainCamera.enabled = true;
        bSpawnEnemy = true;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        CubeD = GameObject.Find("CubeD");

        if (CubeD != null)
        {
            Rigidbody2D rbCube = CubeD.GetComponent<Rigidbody2D>();

            transform.position = player.transform.position + offset;

            if (rbCube.velocity.x <= 0.4f)
            {
                mainCamera.enabled = true;
                cubeCamera.enabled = false;
                playerCamera.enabled = false;
            }
            else
            {
                mainCamera.enabled = false;
                cubeCamera.enabled = true;
                playerCamera.enabled = true;

                if (bSpawnEnemy == true)
                {
                    StartCoroutine(PuzzleAOE());
                    bSpawnEnemy = false;
                }
            }
        }
        else
        {
            mainCamera.enabled = true;
            cubeCamera.enabled = false;
            playerCamera.enabled = false;
        }
    }

    void PuzzleEnemy()
    {
        GameObject SpawnEnemy  = Instantiate(fluff, new Vector2(-2f,0.5f), Quaternion.identity);
        GameObject SpawnEnemy2 = Instantiate(fluff, new Vector2(0.5f, 0.5f), Quaternion.identity);
        GameObject SpawnEnemy3 = Instantiate(fluff, new Vector2(-2f, -2f), Quaternion.identity);
        GameObject SpawnEnemy4 = Instantiate(fluff, new Vector2(0.5f, -2f), Quaternion.identity);
    }

    IEnumerator PuzzleAOE()
    {
        yield return new WaitForSeconds(2f);
        GameObject.Find("AOE").GetComponent<AOESystem>().PlayAOEPuzzle();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuShooter : MonoBehaviour
{
    public List<ParticleSystem> materialList = new List<ParticleSystem>();

    public Transform myPSTransform;
    public ParticleSystem myPS;
    [Range(0, 0.62f)]
    public float joystickTolerance;
    public GameObject targetRotator;
    public Transform target;
    public bool canShoot;

    [Header("Clamping")]
    public bool allowClampHaut;
    public bool superiorEqualValue;
    [Range(-180, 180)]
    public int clampHaut;

    public bool allowClampBas;

    [Range(-180, 180)]
    public int clampBas;
    public bool inferiorEqualValue;
    public int clampRange;


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetAxisRaw("ShootParticles") != 0 && canShoot == true)
        {
            UseWave();
            canShoot = false;
        }
        if (Input.GetAxisRaw("ShootParticles") == 0)
        {
            canShoot = true;
        }

        float h1 = Input.GetAxis("HorizontalRight");
        float v1 = Input.GetAxis("VerticalRight");

        if (h1 > joystickTolerance || h1 < -joystickTolerance || v1 > joystickTolerance || v1 < -joystickTolerance)
        {
            float z = -(Mathf.Atan2(h1, v1) * 180 / Mathf.PI);

            if (z < clampHaut+clampRange && z > clampHaut-clampRange && allowClampHaut == true)
            {
                z = clampHaut;
            }

            if (z < clampBas + clampRange && z > clampBas - clampRange && allowClampBas == true)
            {
                z = clampBas;
            }

            if (superiorEqualValue == true && z > clampHaut)
            {
                z = clampHaut;
            }
            else if(inferiorEqualValue == true && z < clampBas)
            {
                z = clampBas;
            }

            targetRotator.transform.localEulerAngles = new Vector3(0f, 0f, z); // this does the actual rotaion according to inputs
        }
    }

    public void UseWave()
    {
        int index = Random.Range(0, materialList.Count);
        myPS = materialList[index];

        Vector2 rotationVector = new Vector2(target.position.x - myPSTransform.position.x, target.position.y - myPSTransform.position.y);
        float angleValue = Mathf.Atan2(rotationVector.normalized.y, rotationVector.normalized.x) * Mathf.Rad2Deg;

        ParticleSystem.ShapeModule wpshape = myPS.shape;
        wpshape.rotation = new Vector3(0, 0, angleValue);

        myPS.Emit(1);
    }
}

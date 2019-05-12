using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuShooter : MonoBehaviour
{
    //public List<ParticleSystem> materialList = new List<ParticleSystem>();

    //public Transform myPSTransform;
    //public ParticleSystem myPS;
    [Range(0, 0.62f)]
    public float joystickTolerance;
    public GameObject targetRotator;
    public Transform target;
    //public bool canShoot;

    [Header("Clamp Haut")]
    public bool allowClampHaut;
    public bool superiorEqualValue;
    [Range(-180, 180)]
    public int clampHaut;

    [Header("Clamp Middle 1")]
    public bool allowClampMid;
    [Range(-180, 180)]
    public int clampMid;

    [Header("Clamp Middle 2")]
    public bool allowClampMid2;
    [Range(-180, 180)]
    public int clampMid2;

    [Header("Clamp Bas")]
    public bool allowClampBas;
    public bool inferiorEqualValue;
    [Range(-180, 180)]
    public int clampBas;

    public int clampRange;

    public bool autoReturnToInitialPos = false;


    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        /*if (Input.GetAxisRaw("ShootParticles") != 0 && canShoot == true)
        {
            UseWave();
            canShoot = false;
        }
        if (Input.GetAxisRaw("ShootParticles") == 0)
        {
            canShoot = true;
        }*/

        float h1 = Input.GetAxis("HorizontalRight");
        float v1 = Input.GetAxis("VerticalRight");

        if (h1 > joystickTolerance || h1 < -joystickTolerance || v1 > joystickTolerance || v1 < -joystickTolerance)
        {
            float z = -(Mathf.Atan2(h1, v1) * 180 / Mathf.PI);

            //Clamp Haut
            if (z < clampHaut+clampRange && z > clampHaut-clampRange && allowClampHaut == true)
            {
                z = clampHaut;
            }

            //Clamp Bas
            if (z < clampBas + clampRange && z > clampBas - clampRange && allowClampBas == true)
            {
                z = clampBas;
            }

            //Clamp Mid 1
            if (z < clampMid + clampRange && z > clampMid - clampRange && allowClampMid == true)
            {
                z = clampMid;
            }

            //Clamp Mid 2
            if (z < clampMid2 + clampRange && z > clampMid2 - clampRange && allowClampMid2 == true)
            {
                z = clampMid2;
            }
           
            if (superiorEqualValue == true && z > clampHaut) //Max Angle Haut
            {
                z = clampHaut;
            }
            else if(inferiorEqualValue == true && z < clampBas) //Max Angle Bas
            {
                z = clampBas;
            }

            targetRotator.transform.localEulerAngles = new Vector3(0f, 0f, z); // this does the actual rotaion according to inputs
        }
        else if(autoReturnToInitialPos == true)
        {
            Vector3 curRot = targetRotator.transform.localEulerAngles;
            Vector3 homeRot;
            if (curRot.z > 180f)
            {
                homeRot = new Vector3(0f, 0f, 359.999f);
            }
            else
            {
                homeRot = Vector3.zero;
            }
            targetRotator.transform.localEulerAngles = Vector3.Slerp(curRot, homeRot, Time.deltaTime * 2);
        }
    }

    /*public void UseWave()
    {
        int index = Random.Range(0, materialList.Count);
        myPS = materialList[index];

        Vector2 rotationVector = new Vector2(target.position.x - myPSTransform.position.x, target.position.y - myPSTransform.position.y);
        float angleValue = Mathf.Atan2(rotationVector.normalized.y, rotationVector.normalized.x) * Mathf.Rad2Deg;

        ParticleSystem.ShapeModule wpshape = myPS.shape;
        wpshape.rotation = new Vector3(0, 0, angleValue);

        myPS.Emit(1);
    }*/
}

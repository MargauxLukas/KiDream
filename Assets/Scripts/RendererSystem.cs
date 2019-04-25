using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendererSystem : MonoBehaviour
{
    [Range(-10,10), SerializeField]
    private float offsetValue = 0;

    [SerializeField]
    private bool runOnlyOnceForStaticObjects = false;
	
	// Late Update
	void LateUpdate ()
    {
        OrderingLayers();
	}

    public void OrderingLayers()
    {

        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, (transform.position.y + offsetValue));

        if(runOnlyOnceForStaticObjects == true)
        {
            Destroy(this);
        }
    }
}

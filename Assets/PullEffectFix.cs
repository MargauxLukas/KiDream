using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullEffectFix : MonoBehaviour {

    public bool isPullable = true;
    
    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ActionObject") && this.GetComponentInChildren<ParticleSystem>().gameObject.name.Contains("Pull") == true)
        {
            ReactionToWave rtw = collision.gameObject.GetComponent<ReactionToWave>();
            rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            rtw.canBePulled = false;
            rb.velocity = Vector2.zero;
            Debug.Log("kinem");
        }
        else if (collision.gameObject.CompareTag("ActionObject") && this.GetComponentInChildren<ParticleSystem>().gameObject.name.Contains("Pull") == false)
        {
            ReactionToWave rtw = collision.gameObject.GetComponent<ReactionToWave>();
            rtw.canBePulled = true;
            rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.velocity = Vector2.zero;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ActionObject"))
        {
            ReactionToWave rtw = collision.gameObject.GetComponent<ReactionToWave>();
            rtw.canBePulled = true;
            rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.velocity = Vector2.zero;
        }
    }
}

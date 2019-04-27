using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] float explosionTime;
    [SerializeField] float timer;

    BoxCollider2D collider;

	void Start ()
    {
        collider = gameObject.GetComponent<BoxCollider2D>();
	}
	
	void Update ()
    {
        timer += Time.deltaTime;

        if (timer >= explosionTime)
        {
            Explode();
        }
	}

    public bool Explode()
    {
        Collider2D intersecting = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y), 0.1f);
        if (intersecting != null)
        {
            if (intersecting.tag == "Player")
            {
                Destroy(gameObject);
                Destroy(intersecting.gameObject);
            }
            return true;
        }
        else
        {
            Destroy(gameObject);
            return false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Explode();
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbShoot : MonoBehaviour {

    [SerializeField]
    private float speed;
    public Rigidbody2D rb;
    [SerializeField]
    public GameObject caster;

    // Use this for initialization
    void Start()
    {
        rb.velocity = transform.right * caster.GetComponent<PlayerController>().speed;
    }

    private void Update()
    {
        //Destroys lightening sprite if goes out of area
        if (rb.position.x > 25f || rb.position.x < -25f ||
            rb.position.y > 15f || rb.position.y < -15)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject == caster)
        {
            Physics2D.IgnoreCollision(hitInfo.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());

        }
        else
        {
            if (hitInfo.gameObject.layer == 8)
            {
                hitInfo.gameObject.GetComponent<PlayerController>().TakeDamage(10, gameObject.transform.position, true, 5);
            }
            Destroy(gameObject);
        }
    }
}

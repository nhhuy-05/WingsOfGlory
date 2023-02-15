using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bullet script
/// </summary>
public class Bullet : MonoBehaviour
{
    // time support
    Timer timer;

    // magnitude support
    const float Magnitude = 20f;
    // Start is called before the first frame update
    void Start()
    {
        // create timer
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = 2;
        timer.Run();

    }

    // Update is called once per frame
    void Update()
    {
        // check for timer finished
        if (timer.Finished)
        {
            // destroy self
            Destroy(gameObject);
        }
    }

    // add force for the bullet 
    public void ApplyForce(Vector3 direction)
    {
        // add force
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.AddForce(direction * Magnitude, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // remove velocity when collision with aircraft and helicopter
        if (collision.gameObject.CompareTag("Aircraft")|| collision.gameObject.CompareTag("Helicopter"))
        {
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.AddForce(Vector3.back * Magnitude, ForceMode2D.Impulse);
        }
    }
}

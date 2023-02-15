using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Aircraft class
/// </summary>
public class Aircraft : MonoBehaviour
{
    // field explosion
    [SerializeField]
    GameObject prefabExplosion;
    // health support
    int maxHealth = 100;
    int currentHealth;
    public HealthBar healthBar;
    
    // Start is called before the first frame update
    void Start()
    {
        // set health
        currentHealth = maxHealth;
        //heathOfShip.text = currentHealth.ToString();
        healthBar.SetMaxHealth((int)maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        // if the aircraft is out of the screen destroy it
        if (transform.position.x < ScreenUtils.ScreenLeft || transform.position.x > ScreenUtils.ScreenRight)
        {
            Destroy(gameObject);
        }

        // keep the aircraft don't rotate after collision with bullet
        transform.rotation = Quaternion.identity;
        // keep health bar don't rotate with sprite
        healthBar.transform.rotation = Quaternion.identity;
        healthBar.transform.position = transform.position + new Vector3(0, 0.75f, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // check for collision with bullet
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(50);
            if (currentHealth <= 0)
            {
                // destroy aircraft
                Destroy(gameObject);
                // create explosion
                Instantiate(prefabExplosion, transform.position, Quaternion.identity);
                AudioManager.Play(AudioClipName.Explosion);
            }
            // destroy bullet
            Destroy(collision.gameObject);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        //heathOfShip.text = currentHealth.ToString();
    }

    /// <summary>
    /// Starts the asteroid moving in the given direction
    /// </summary>
    /// <param name="direction">direction for the asteroid to move</param>
    /// <param name="position">position for the asteroid</param>
    public void Initialize(Direction direction, Vector3 position)
    {
        // set asteroid position
        transform.position = position;

        // set random angle based on direction
        float angle;
        float randomAngle = Random.value * 30f * Mathf.Deg2Rad;
        if (direction == Direction.Up)
        {
            angle = 75 * Mathf.Deg2Rad + randomAngle;
        }
        else if (direction == Direction.Left)
        {
            angle = 165 * Mathf.Deg2Rad + randomAngle;
        }
        else if (direction == Direction.Down)
        {
            angle = 255 * Mathf.Deg2Rad + randomAngle;
        }
        else
        {
            angle = -15 * Mathf.Deg2Rad + randomAngle;
        }

        // apply impulse force to get asteroid moving
        const float MinImpulseForce = 3f;
        const float MaxImpulseForce = 5f;
        Vector2 moveDirection = new Vector2(
            Mathf.Cos(angle), Mathf.Sin(angle));
        float magnitude = Random.Range(MinImpulseForce, MaxImpulseForce);
        GetComponent<Rigidbody2D>().AddForce(
            moveDirection * magnitude,
            ForceMode2D.Impulse);
    }
}

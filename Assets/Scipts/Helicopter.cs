using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
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
        // if the helicopter is out of the screen destroy it
        if (transform.position.x < ScreenUtils.ScreenLeft || transform.position.x > ScreenUtils.ScreenRight)
        {
            Destroy(gameObject);
        }

        // keep the helicopter don't rotate after collision with bullet
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
            TakeDamage(20);
            if (currentHealth <= 0)
            {
                // destroy helicopter
                Destroy(gameObject);
                // create explosion
                // Instantiate(prefabExplosion, transform.position, Quaternion.identity);
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
}

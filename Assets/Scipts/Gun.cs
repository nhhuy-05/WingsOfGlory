using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gun scipt
/// </summary>
public class Gun : MonoBehaviour
{
    // bullet support
    [SerializeField]
    GameObject bulletPrefab;
    
    // rotate support
    const float RotateDegreesPerSecond = 60f;

    // time to fire
    const float nextFire = 0.0f;
    float nextFireTime = 0f;

    // Start is called before the first frame update
    void Start()
    {   
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        // check for rotate input
        float rotationInput = Input.GetAxis("Horizontal");
        if (rotationInput != 0)
        {
            // calculate rotation amount and apply rotation
            float rotationAmount = RotateDegreesPerSecond * Time.deltaTime;
            if (rotationInput < 0)
            {
                rotationAmount *= -1;
            }
            transform.Rotate(Vector3.forward, rotationAmount);
        }
        nextFireTime += Time.deltaTime;
        // check for fire input
        if (Input.GetKey(KeyCode.Space) && nextFireTime > nextFire)
        {
            // create bullet
            GameObject bullet = Instantiate(bulletPrefab) as GameObject;
            bullet.transform.position = transform.GetChild(1).transform.position;
            bullet.transform.rotation = transform.rotation;
            // add force for the bullet following the gun direction
            Vector3 direction = transform.rotation * Vector3.up;
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.ApplyForce(direction);
            nextFireTime = 0f;
        }
    }
}

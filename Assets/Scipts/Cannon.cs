using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cannon scipt
/// </summary>
public class Cannon : MonoBehaviour
{
    // move support
    const float MoveUnitsPerSecond = 10f;

    // saved for efficiency
    float colliderHalfWidth;
    float colliderHalfHeight;
    
    // Start is called before the first frame update
    void Start()
    {
        BoxCollider2D collider2D = gameObject.transform.GetChild(1).GetComponent<BoxCollider2D>();
        colliderHalfWidth = collider2D.size.x;
        colliderHalfHeight = collider2D.size.y/2;
    }

    // Update is called once per frame
    void Update()
    {
        // move right and left
        float horizontalInput = Input.GetKey(KeyCode.V) ? -1 : Input.GetKey(KeyCode.B) ? 1 : 0;
        if (horizontalInput != 0)
        {
            // calculate move amount and apply movement
            float moveAmount = MoveUnitsPerSecond * Time.deltaTime;
            if (horizontalInput < 0)
            {
                moveAmount *= -1;
            }
            transform.Translate(Vector3.right * moveAmount);
        }
        ClampInScreen();
    }

    /// <summary>
    /// Clamps the character in the screen
    /// </summary>
    void ClampInScreen()
    {
        Vector3 positionGun = transform.GetChild(0).transform.position;
        Vector3 positionPillar = transform.GetChild(1).transform.position;

        // clamp horizontally
        if (positionPillar.x - colliderHalfWidth < ScreenUtils.ScreenLeft)
        {
            positionGun.x = ScreenUtils.ScreenLeft + colliderHalfWidth;
            positionPillar.x = ScreenUtils.ScreenLeft + colliderHalfWidth;
        }
        else if (positionPillar.x + colliderHalfWidth > ScreenUtils.ScreenRight)
        {
            positionGun.x = ScreenUtils.ScreenRight - colliderHalfWidth;
            positionPillar.x = ScreenUtils.ScreenRight - colliderHalfWidth;
        }

        // clamp vertically
        if (positionPillar.y + colliderHalfHeight > ScreenUtils.ScreenTop)
        {
            positionGun.y = ScreenUtils.ScreenTop - colliderHalfHeight;
            positionPillar.y = ScreenUtils.ScreenTop - colliderHalfHeight;
        }
        else if (positionPillar.y - colliderHalfHeight < ScreenUtils.ScreenBottom)
        {
            positionGun.y = ScreenUtils.ScreenBottom + colliderHalfHeight*1.65f;
            positionPillar.y = ScreenUtils.ScreenBottom + colliderHalfHeight;
        }

        transform.GetChild(0).transform.position = positionGun;
        transform.GetChild(1).transform.position = positionPillar;
    }
}

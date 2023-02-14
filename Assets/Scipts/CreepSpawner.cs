using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepSpawner : MonoBehaviour
{
    // field creeps
    [SerializeField]
    GameObject[] creeps = new GameObject[2];

    // field positions of the spawner
    [SerializeField]
    GameObject[] positions = new GameObject[2];

    // time support
    Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        // create timer
        timer = gameObject.GetComponent<Timer>();
        Reset();

    }
    void Reset()
    {
        // reset timer
        timer.Duration = Random.Range(0.5f, 1f);
        timer.Run();
    }

    // Update is called once per frame
    void Update()
    {
        // check for timer finished
        if (timer.Finished)
        {
            // spawn creep
            SpawnCreep();
            
            // reset timer
            Reset();
        }
        
    }

    // spawn creep move horizontal throwing screen
    private void SpawnCreep()
    {
        int indexCreep = Random.Range(0, creeps.Length);
        int indexPosition = Random.Range(0, positions.Length);

        // spawn random creep
        GameObject creep = Instantiate<GameObject>(creeps[indexCreep], positions[indexPosition].transform.position, Quaternion.identity);

        // add force to move horizontal throwing screen
        Vector2 vector1 = new Vector2(-1, 0);
        Vector2 vector2 = new Vector2(1, 0);
        Vector2 direction = (indexPosition == 0 ? vector1 : vector2);
        float magnitude = Random.Range(5, 15);
        creep.GetComponent<Rigidbody2D>().AddForce(direction * magnitude, ForceMode2D.Impulse);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody rb; // Get reference to rigidbody
    private GameManager gameManager;

    public int pointValue; // Amount of points you get for clicking on this object
 
    
    private float minSpeed = 10;
    private float maxSpeed = 15;
    private float maxTorque = 40;
    private float xRange = 4;
    private float ySpawnPos = -2;

    public ParticleSystem explosionParticle;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        rb = gameObject.GetComponent<Rigidbody>(); // Assign rigidbody
        
        
        // Add force and torque to the newly instantiated object
        rb.AddForce(RandomForce(), ForceMode.Impulse);
        rb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque());
        transform.position = RandomSpawnPos();
    }

    Vector3 RandomForce() // Generate random upward force
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque() // Generate random float
    {
        return Random.Range(-maxTorque, maxTorque);
    }
    
    Vector3 RandomSpawnPos() // Generate random position within bounds of xRange
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
    
    
    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            gameManager.UpdateScore(pointValue);
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver();
        }
        Destroy(gameObject);
    }
    
}

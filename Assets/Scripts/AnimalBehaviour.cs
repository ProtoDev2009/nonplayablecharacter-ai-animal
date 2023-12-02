using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public Rigidbody animalRb;
    public float minimumDistance = 5f;

    float rotationalForce = 2f;
    float latestChangeDirectionTime;
    float directionChangeTime = 3f;
    float characterVelocity = 2f;
    Vector3 movementDirection;
    Vector3 movementPerSecond;

    void Start(){
        latestChangeDirectionTime = 0f;
	    calculateDirection();
    }

    void calculateDirection(){
        //create a random direction vector with the magnitude of 1
        movementDirection = new Vector3(Random.Range(-1.0f, 1.0f), 0f, Random.Range(-1.0f, 1.0f)).normalized;
    }

    void Update(){
        movementPerSecond = movementDirection * characterVelocity;

        //if the changeTime was reached, calculate a new movement vector
	    if (Time.time - latestChangeDirectionTime > directionChangeTime){
		    latestChangeDirectionTime = Time.time;
		    calculateDirection();
	    }

        //check if player is nearby:
        if(Vector3.Distance(transform.position, player.position) <= minimumDistance) {characterVelocity = 10f; directionChangeTime = 0.5f; Debug.Log("Near Player");}
        else {characterVelocity = 2f; directionChangeTime = 3f;Debug.Log("Far Player");}

        //move enemy: 
	    transform.position = new Vector3(transform.position.x + (movementPerSecond.x * Time.deltaTime), transform.position.y,
        transform.position.z + (movementPerSecond.z * Time.deltaTime));

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movementDirection), rotationalForce * Time.deltaTime);
	    
    }
}
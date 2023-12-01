using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public Rigidbody animalRb;
    public float moveSpeed = 10f;
    public float minimumDistance = 5f;

    float rotationalForce = 2f;
    float latestChangeDirectionTime;
    readonly float directionChangeTime = 3f;
    float characterVelocity = 2f;
    Vector3 movementDirection;
    Vector3 movementPerSecond;

    void Start(){
        latestChangeDirectionTime = 0f;
	    calculateDirection();
    }

    void calculateDirection(){
        //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        movementDirection = new Vector3(Random.Range(-1.0f, 1.0f), 0f, Random.Range(-1.0f, 1.0f)).normalized;
	    movementPerSecond = movementDirection * characterVelocity;
    }

    void Update(){
        Vector3 lookAwayFromPlayerDirection = transform.position - player.position;

        //if the changeTime was reached, calculate a new movement vector
	    if (Time.time - latestChangeDirectionTime > directionChangeTime){
		    latestChangeDirectionTime = Time.time;
		    calculateDirection();
	    }

        //check if player is nearby:
        if(Vector3.Distance(transform.position, player.position) <= minimumDistance){
            //rotate enemy in direction opposite to player:
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookAwayFromPlayerDirection), rotationalForce * Time.deltaTime);

            //move player in direction opposite to player:
            transform.Translate(moveSpeed * lookAwayFromPlayerDirection * Time.deltaTime);
            Debug.DrawLine(transform.position, player.position, Color.blue, 2f);
            Debug.Log("Rigidbody Move Position" + Time.time);
        }
        else{
            //move enemy: 
	        transform.position = new Vector3(transform.position.x + (movementPerSecond.x * Time.deltaTime), transform.position.y,
            transform.position.z + (movementPerSecond.z * Time.deltaTime));

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movementDirection), rotationalForce * Time.deltaTime);
        }
	    
    }
}
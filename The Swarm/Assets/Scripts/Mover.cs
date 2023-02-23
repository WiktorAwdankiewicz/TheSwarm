using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    //Rigidbody2D unitRB;
    Vector3 direction;

    /*private void Start()
    {
        unitRB = GetComponent<Rigidbody2D>();
    }*/

    private void FixedUpdate()
    {
        // swarm unit moving towards player - method 1
        direction = GameObject.FindWithTag("Player").transform.position;
        transform.position = Vector3.MoveTowards(transform.position, direction, GameController.swarmLandUnitSpeed * Time.fixedDeltaTime);

        // swarm unit moving towards player - method 2
        // in this method units slows a little bit just bofre "meeting" with player
        //direction = GameObject.FindWithTag("Player").transform.position - transform.position;
        //transform.Translate(direction * GameController.swarmLandUnitSpeed);
    }
}

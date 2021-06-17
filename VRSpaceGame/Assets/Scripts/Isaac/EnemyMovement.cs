﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    ///////// THIS SHOULD BE ADDED THE THE HULL OF THE SHIP, THE WEAPONS CANNOT BE A CHILD OF THE OBJECT THIS SCRIPT IS ATTACHED TO

    // Rate at which the player moves
    public float speed;

    // Object containing the nodes the Enemy moves between
    public GameObject track;

    // Highest parented object in the hierarchy
    public Transform parentTransform;

    // First point on track
    Transform pointA;

    // Second point on track
    Transform pointB;

    // Speed the player rotates at
    public float rotateSpeed;

    // Current target
    Transform target;

    // direction of the target
    Vector3 direction;

    // Rotation required for enemy to face the target
    Quaternion lookRotation;

    // Start is called before the first frame update
    void Start()
    {
        pointA = track.transform.GetChild(0);
        pointB = track.transform.GetChild(1);

        // Set start position to point a and target position to point b
        parentTransform.position = pointA.position;
        target = pointB;
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(direction);

        // Get direction from ship hull to target
        direction = target.position - transform.position;

        // Get the required rotation to be facing target
        lookRotation = Quaternion.LookRotation(direction);

        // Apply appropriate rotation
        parentTransform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);

        // Update position
        parentTransform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        // Switch target point
        if (other.gameObject.tag == "Point A")
        {
            target = pointB;
        }
        if (other.gameObject.tag == "Point B")
        {
            Debug.Log("target switched");
            target = pointA;
        }
    }
}

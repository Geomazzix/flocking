using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockObjectController : MonoBehaviour
{
    private GameController _GameController;
    private ShowDestination _ShowDes;

    [SerializeField]
    private float _MoveSpeed, _GroupRange, _AvoidanceRange, _RotationSpeed;


    //Get the gameController.
    private void Awake()
    {
        _GameController = FindObjectOfType<GameController>();
        _ShowDes = FindObjectOfType<ShowDestination>();
    }


    //Create the movement of the fish.
    private void Update()
    {
        MaintainFlock(_GameController._AllFlockObjects);

        transform.Translate(Vector3.forward * _MoveSpeed * Time.deltaTime);
    }


    //Create the flock and maintain it by controlling its direction.
    private void MaintainFlock(FlockObjectController[] allSardines)
    {

        List<FlockObjectController> flockFishes = new List<FlockObjectController>();

        Vector3 avoidance = Vector3.zero;
        Vector3 center = Vector3.zero;

        int flockSize = 0;

        //Check if any fish is close enough to this fish, if so form a group.
        for (int i = 0; i < allSardines.Length; i++)
        {
            if (Vector3.Distance(allSardines[i].transform.position, transform.position) < _GroupRange)
            {
                flockFishes.Add(allSardines[i]);
                flockSize++;

                //Check if the fishes come to close to each other.
                if (Vector3.Distance(allSardines[i].transform.position, transform.position) < _AvoidanceRange)
                {
                    avoidance = avoidance + (transform.position - allSardines[i].transform.position);
                }
            }
        }


        //if this fish is part of a group, swim towards each other.
        if(flockSize > 0)
        {

            //Create a flockDestination to move towards.
            center = CalculateCenter(flockFishes) + (_GameController._GetFlockDestination - transform.position);

            Vector3 direction = (center + avoidance) - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), _RotationSpeed * Time.deltaTime);
        }
    }


    //Calculate the center of the group.
    private Vector3 CalculateCenter(List<FlockObjectController> flock)
    {   
        Vector3 center;

        //Set the center to the first index of the list.
        center = flock[0].transform.position;

        //Find the centerpoint in between all the fishes.
        for (int i = 1; i < flock.Count; i++)
        {
            center = Vector3.Lerp(center, flock[i].transform.position, 0.5f);
        }

        //Return the result.
        return center;
    }
}

using System.Collections.Generic;
using UnityEngine;

namespace Flocking
{
    /// <summary>Sardine instance which swims around in the level. It handles it's own flocking.</summary>
    public class Sardine : MonoBehaviour
    {
        private SardineDestinationManager _SardineDestinationManager;
        [SerializeField] private float _MoveSpeed = 2, _GroupRange = 2, _AvoidanceRange = 1.5f, _RotationSpeed = 1.0f;

        private void Start()
        {
            _SardineDestinationManager = FindObjectOfType<SardineDestinationManager>();
        }

        private void Update()
        {
            MaintainFlock(_SardineDestinationManager.AllSardine);
            MoveSaldineForward();
        }

        private void MaintainFlock(Sardine[] allSardines)
        {
            List<Sardine> flockFishes = new List<Sardine>();
            Vector3 avoidance = Vector3.zero;

            int flockSize = 0;
            foreach (Sardine sardine in allSardines)
            {
                //Check if the sardine is within group distance, so it can join/form a group.
                if (Vector3.Distance(sardine.transform.position, transform.position) >= _GroupRange) continue;

                flockFishes.Add(sardine);
                flockSize++;

                //Check if the sardine come to close to each other.
                if (Vector3.Distance(sardine.transform.position, transform.position) < _AvoidanceRange)
                    avoidance = avoidance + (transform.position - sardine.transform.position);
            }

            if (flockSize <= 0) return;

            Vector3 center = CalculateCenterFlock(in flockFishes) + (_SardineDestinationManager.GetFlockDestination - transform.position);
            Vector3 direction = (center + avoidance) - transform.position;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), _RotationSpeed * Time.deltaTime);
        }

        private Vector3 CalculateCenterFlock(in List<Sardine> flock)
        {
            Vector3 flockCenter = Vector3.zero;
            foreach (Sardine sardine in flock)
                flockCenter += sardine.transform.position;
            return flockCenter / flock.Count;
        }

        private void MoveSaldineForward()
        {
            transform.Translate(Vector3.forward * _MoveSpeed * Time.deltaTime);
        }
    }
}

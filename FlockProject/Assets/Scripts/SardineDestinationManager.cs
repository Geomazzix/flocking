using UnityEngine;

namespace Flocking
{
    /// <summary>Gives the flock a new randomly generated destination within the specified borders.</summary>
    public class SardineDestinationManager : MonoBehaviour
    {
        [Header("Fish settings")]
        [SerializeField] private int _FlockObjectCount;
        [SerializeField] private GameObject _FlockObjectPrefab;

        [Header("SpawnSpace and border for the fishes")]
        [SerializeField] private float _LeftWall;
        [SerializeField] private float _RightWall, _GroundWall, _UpperWall, _FrontSide, _BackSide;

        private Sardine[] _AllSardine;
        private Vector3 _FlockDestination;
        private float _DestinationTimer;

        public Sardine[] AllSardine => _AllSardine;
        public Vector3 GetFlockDestination => _FlockDestination;

        private void Awake()
        {
            for (int index = 0; index < _FlockObjectCount; index++)
            {
                Vector3 spawnPosition = spawnPosition = new Vector3(
                    x: Random.Range(_LeftWall, _RightWall),
                    y: Random.Range(_GroundWall, _UpperWall),
                    z: Random.Range(_FrontSide, _BackSide));
                Instantiate(_FlockObjectPrefab, spawnPosition, transform.rotation, transform);
            }
            _AllSardine = FindObjectsOfType<Sardine>();
        }

        private void Update()
        {
            HandleDestinationTimer();
        }

        private void HandleDestinationTimer()
        {
            _DestinationTimer -= Time.deltaTime;
            if (!(_DestinationTimer <= 0)) return;

            _FlockDestination = new Vector3(
                Random.Range(_LeftWall, _RightWall),
                Random.Range(_GroundWall, _UpperWall),
                Random.Range(_FrontSide, _BackSide));

            _DestinationTimer = Random.Range(2, 4);
        }
    }
}

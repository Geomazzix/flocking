using UnityEngine;

public class GameController : MonoBehaviour
{
    #region inspector
    [Header("Fish settings")]
    [SerializeField]
    private int _FlockObjectCount;
    [SerializeField]
    private GameObject _FlockObjectPrefab;
    [HideInInspector]
    public FlockObjectController[] _AllFlockObjects;


    [Header("SpawnSpace and border for the fishes")]
    [Space]
    [SerializeField]
    private float _LeftWall;
    [SerializeField]
    private float _RightWall, _GroundWall, _UpperWall, _FrontSide, _BackSide;


    [Header("Insert boxes for the not-moveable places.")]
    [Space]
    [SerializeField]
    private float _Left;
    [SerializeField]
    private float _Right, _Front, _Back, _Up, _Down;
    #endregion

    #region class variables
    private Vector3 _FlockDestination = Vector3.zero;
    private float _DestinationTimer;
    #endregion

    #region properties
    public Vector3 _GetFlockDestination
    {
        get { return _FlockDestination; }
    }
    #endregion

    //Spawn all the sardine within the invisible box.
    private void Awake()
    {
        Vector3 spawnPos = Vector3.zero;

        //Spawn all the sardine.
        for (int currSardine = 0; currSardine < _FlockObjectCount; currSardine++)
        {
            spawnPos = new Vector3(
                Random.Range(_LeftWall, _RightWall),
                Random.Range(_GroundWall, _UpperWall),
                Random.Range(_FrontSide, _BackSide));

            Instantiate(_FlockObjectPrefab, spawnPos, transform.rotation);
        }


        //Fill the array with all the sardine.
        _AllFlockObjects = FindObjectsOfType<FlockObjectController>();
    }


    //Create a timer to make set a destination for the flock.
    private void Update()
    {
        _DestinationTimer -= Time.deltaTime;

        //Set a new Destination.
        if(_DestinationTimer <= 0)
        {
            _FlockDestination = new Vector3(
                Random.Range(_LeftWall, _RightWall),
                Random.Range(_GroundWall, _UpperWall),
                Random.Range(_FrontSide, _BackSide));

            _DestinationTimer = Random.Range(2,4);
        }
    }
}

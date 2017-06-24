using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDestination : MonoBehaviour
{
    private GameController _GameController;
    private Vector3 _Dir;

    private void Awake()
    {
        _GameController = FindObjectOfType<GameController>();
    }

    private void Update()
    {
        transform.position = _Dir;
    }

    public void GetDirection(Vector3 direction)
    {
        _Dir = direction;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] private WaveConfig _waveConfig;
    private List<Transform> _waypoints;
    [SerializeField] private float _movementSpeed = 2f;
    private int _waypointIndex = 0;

    private void Start()
    {
        _waypoints = _waveConfig.GetWaypoints();
        transform.position = _waypoints[_waypointIndex].transform.position;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (_waypointIndex <= _waypoints.Count - 1)
        {
            Vector3 targetPos = _waypoints[_waypointIndex].transform.position;
            float movementThisFrame = _movementSpeed * Time.deltaTime;

            transform.position = Vector2.MoveTowards(
                transform.position,
                targetPos,
                movementThisFrame);

            if (transform.position == targetPos)
            {
                _waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

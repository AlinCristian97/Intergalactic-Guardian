using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 10f;
    private float _xMin, _xMax, _yMin, _yMax;
    
    void Start()
    {
        SetUpMoveBoundaries();
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        _xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        _xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        _yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        _yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * _movementSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * _movementSpeed;
        
        var newXPos = Mathf.Clamp((transform.position.x + deltaX), 
            _xMin + (transform.localScale.x / 2),
            _xMax - (transform.localScale.x / 2));
        var newYPos = Mathf.Clamp((transform.position.y + deltaY), 
            _yMin + (transform.localScale.y / 2),
            _yMax - (transform.localScale.y / 2));

        transform.position = new Vector2(newXPos, newYPos);
    }
}
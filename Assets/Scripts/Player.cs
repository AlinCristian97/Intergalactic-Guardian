using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 10f;
    
    void Start()
    {
        
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * _movementSpeed;
        var newXPos = transform.position.x + deltaX;
        
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * _movementSpeed;
        var newYPos = transform.position.y + deltaY;

        transform.position = new Vector2(newXPos, newYPos);
    }
}
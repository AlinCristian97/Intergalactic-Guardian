using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] private float _spinSpeed = 1f;

    private void Update()
    {
        transform.Rotate(0, 0, _spinSpeed * Time.deltaTime);
    }
}

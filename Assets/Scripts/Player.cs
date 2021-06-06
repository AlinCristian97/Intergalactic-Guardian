using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float _movementSpeed = 10f;
    [SerializeField] private int _health = 200;
    
    [Header("Projectile")]
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private float _projectileSpeed = 10f;
    [SerializeField] private float _projectileFiringPeriod;
    
    [Header("SFX")]
    [SerializeField] private AudioClip _deathSFX;
    [SerializeField] [Range(0, 1)] private float _deathSFXVolume = 0.7f;
    [SerializeField] private AudioClip _shootSFX;
    [SerializeField] [Range(0, 1)] private float _shootSFXVolume = 0.25f;
    
    private float _xMin, _xMax, _yMin, _yMax;

    private Coroutine _firingCoroutine;


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
        Fire();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();

        if (!damageDealer) { return; }
        
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        _health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(_deathSFX, Camera.main.transform.position, _deathSFXVolume);
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);

            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, _projectileSpeed);
        
            AudioSource.PlayClipAtPoint(_shootSFX, Camera.main.transform.position, _shootSFXVolume);
            
            yield return new WaitForSeconds(_projectileFiringPeriod);
        }
    }

    //TODO: Improve Shooting system
    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _firingCoroutine = StartCoroutine(FireContinuously());
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(_firingCoroutine);
        }
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
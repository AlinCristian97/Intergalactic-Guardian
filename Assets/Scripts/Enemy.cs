using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health = 100f;
    [SerializeField] private int _scoreValue = 150;
    
    [Header("Projectile")]
    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _shotCounter;
    [SerializeField] private float _minTimeBetweenShots = 0.2f;
    [SerializeField] private float _maxTimeBetweenShots = 3f;
    [SerializeField] private float _projectileSpeed = 10f;
    
    [Header("VFX")]
    [SerializeField] private GameObject _deathVFX;
    [SerializeField] private float _explosionDuration = 1f;

    [Header("SFX")]
    [SerializeField] private AudioClip _deathSFX;
    [SerializeField] [Range(0, 1)] private float _deathSFXVolume = 0.7f;
    [SerializeField] private AudioClip _shootSFX;
    [SerializeField] [Range(0, 1)] private float _shootSFXVolume = 0.25f;

    private void Start()
    {
        _shotCounter = Random.Range(_minTimeBetweenShots, _maxTimeBetweenShots);
    }

    private void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        _shotCounter -= Time.deltaTime;
        if (_shotCounter <= 0f)
        {
            Fire();
            _shotCounter = Random.Range(_minTimeBetweenShots, _maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(_projectile, transform.position, Quaternion.identity);
        
        AudioSource.PlayClipAtPoint(_shootSFX, Camera.main.transform.position, _shootSFXVolume);

        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -_projectileSpeed);
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
        FindObjectOfType<GameSession>().AddToScore(_scoreValue);
        Destroy(gameObject);
        GameObject explosion = Instantiate(_deathVFX, transform.position, transform.rotation);
        Destroy(explosion, _explosionDuration);
        AudioSource.PlayClipAtPoint(_deathSFX, Camera.main.transform.position, _deathSFXVolume);
    }
}

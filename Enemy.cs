using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 50;

    [Header("Shooting")]
    [SerializeField] bool isShooter = true;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 2f;
    [SerializeField] float maxTimeBetweenShots = 5f;
    [SerializeField] float projectileSpeed = 5f;
    [SerializeField] float explosionDuration = 1f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] GameObject deathVFX;
    
    [Header("Audio")]
    [SerializeField] AudioClip laserAudio;
    [Range(0,1)] [SerializeField] float laserAudioVolume = 0.5f;
    [SerializeField] AudioClip deathAudio;
    [Range(0,1)] [SerializeField] float deathAudioVolume = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountdownAndShoot();
    }

    private void CountdownAndShoot()
    {
        if (isShooter == true)
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0f)
            {
                Fire();
                shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
            }
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(laserAudio, Camera.main.transform.position, laserAudioVolume);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            Die();
        }
        else
        {
            DamageDealer damageDealer = other.GetComponent<DamageDealer>();
            if (!damageDealer) { return; }
            ProcessHit(damageDealer);
        }
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, Quaternion.identity);
        Destroy(explosion, explosionDuration);
        AudioSource.PlayClipAtPoint(deathAudio, Camera.main.transform.position, deathAudioVolume);
    }
}

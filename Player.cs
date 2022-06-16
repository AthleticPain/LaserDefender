using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Configuration Parameters

    [Header("Player")]
    [Range(0.1f, 100f)] [SerializeField] float movementSpeed = 10f;
    [SerializeField] float padding = 0.5f;
    [SerializeField] int health = 10000;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float explosionDuration;
    [SerializeField] AudioClip deathAudio;
    [Range(0,1)][SerializeField] float deathAudioVolume;
     
    [Header("Projectile")]
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] AudioClip laserAudio;
    [Range(0,1)][SerializeField] float laserAudioVolume;

    Coroutine firingCoroutine;
    Level level;
    
    float xMin;
    float xMax;
    float yMin;
    float yMax;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMovementBoundaries();
        level = FindObjectOfType<Level>();
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
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
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, Quaternion.identity);
        Destroy(explosion, explosionDuration);
        AudioSource.PlayClipAtPoint(deathAudio, Camera.main.transform.position, deathAudioVolume);
        level.WaitAndLoadNextScene();
        
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(laserAudio, Camera.main.transform.position, laserAudioVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }


    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;
        
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        
        transform.position = new Vector2(newXPos, newYPos);
    }

    private void SetUpMovementBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetLayer()
    {
        return gameObject.layer;
    }

}

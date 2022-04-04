using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    private Player _player;
    private Animator _anim;

    private AudioSource _audioSource;
    private float _fireRate = 3.0f;
    private float _canFire = -1;
    [SerializeField]
    private GameObject _laserPrefab;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();

        if (_player == null)
        {
            Debug.LogError("Player is NULL.");
        }

        _anim = GetComponent<Animator>();

        if(_anim == null)
        {
            Debug.LogError("Animator is NULL.");
        }
    }
        
    void Update()
    {
        CalculateMovement();

        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser =  Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
            
            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }
        }
    }

    void CalculateMovement()
    {
        /// <summary>
        /// move down 4 meters per second
        /// </summary>
        /// 

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        /// <summary>
        /// if bottom of screen
        /// respawn at top with new x position
        /// </summary>

        if (transform.position.y < -5f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        /// <summary>
        /// if other is Player
        /// Destroy us
        /// damage the Player
        /// </summary>
        //Player player = other.transform.GetComponent<Player>();

        if (other.tag == "Player")
        {
            if (_player != null)
            {
                _player.Damage();
            }

            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 2.8f);            
        }

        ///<summary>
        ///if other is laser
        ///laser
        ///Destroy us
        ///</summary>

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            { 
                _player.AddScore(10);
            }

            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.8f);           
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Player : MonoBehaviour
{
    private SpawnManager _spawnManager;
    private Vector3 Position => transform.position;

    [SerializeField]
    private float _speed = 5.2f;
    [SerializeField]
    private float _speedMultipler = 2.4f;
       
    [SerializeField]
    public GameObject _laserPrefab;
    [SerializeField]
    public GameObject _tripleShotPrefab;

    [SerializeField]
    private float _fireRate = 0.05f;
    private float _canFire = -0.5f;

    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private bool _isTripleShotActive = false;
    [SerializeField]
    private bool _isSpeedBoostEnabled = false;
    [SerializeField]
    private bool _isShieldsActive = false;

    [SerializeField]
    private GameObject _shieldVisualizer;
    [SerializeField]
    private GameObject _rightEngine, _leftEngine;

    [SerializeField]
    private int _score;

    private UIManager _uiManager;

    [SerializeField]
    private AudioClip _laserSoundClip;
    
    private AudioSource _audioSource;
    private GameManager _gameManager;

    void Start()
    {        

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _audioSource = GetComponent<AudioSource>();
        
        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL.");
        }

        if(_audioSource == null)
        {
            Debug.LogError("Audio Source on the Player is Null.");
        }
        else
        {
            _audioSource.clip = _laserSoundClip;
        }

        if (_gameManager._isCoopMode == false)
        {
            /// <summary>
            /// take the current position & assign it to (0, 0, 0)
            /// </summary>

            transform.position = new Vector3(0, -3, 0);

        }
    }
        
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            LaserFire();
        }
    }

    void CalculateMovement()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        /// <summary>
        /// This is a simplified version of the use control
        /// new Vector3 (1, 0, 0) * 4.25f *real time
        /// </ summary >
        
        Vector3 vector3 = new Vector3(horizontalInput, verticalInput, 0);
        Vector3 direction = vector3;
            
        transform.Translate(direction * _speed * Time.deltaTime);
                
        /// <summary
        /// if player position on the y is greater than 0
        /// y position = 0
        /// else if position on the y is less than -3.8f
        /// y pos = -3.8f
        /// </summary>


        if (Position.y >= -1.5f)
        {
            transform.position = new Vector3(Position.x, -1.5f, 0);
        }
        else if (Position.y <= -4.9f)
        {
            transform.position = new Vector3(Position.x, -4.9f, 0);
        }

        /// <summary>
        /// if player on the x > 11.3
        /// x pos = -11 
        /// else if player on the xis less than -11.3
        /// x pos = 11 
        /// </summary>


        if (Position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, Position.y, 0);
        }
        else if (Position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, Position.y, 0);
        }
    }

       void LaserFire()
    { 
        _canFire = Time.time + _fireRate;

        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, Position , Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, Position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }

        _audioSource.Play();
    }

    public void Damage()
    {
        if(_isShieldsActive == true)
        {
            _isShieldsActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }

        _lives -= 1;

        _uiManager.UpdateLives(_lives);

        if (_lives == 2)
        {
            _leftEngine.SetActive(true);
        }
        else if (_lives == 1)
        {
            _rightEngine.SetActive(true);
        }

        /// <summary>
        /// check if dead 
        /// Destroy us
        /// </summary>
         
        if (_lives < 1)
        {
            /// <summary>
            /// Communicate with Spawn Manager
            /// Let know to stop Spawning
            /// </summary>

            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
            SceneManager.LoadScene(1);
        }
    }

        
    /// <summary>
    /// In this section we are controlling the activation
    /// & deactivation of the TripleShot Powerup
    /// It will be remotely activated from the Powerup
    /// Script by the Trigger
    /// </summary>

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    /// <summary>
    /// In this section we are controlling the activation
    /// & deactivation of the Speed Boost Powerup
    /// It will be remotely activated from the Powerup
    /// Script by the Trigger
    /// </summary>

    public void SpeedBoostActive()
    {
        _isSpeedBoostEnabled = true;
        _speed *= _speedMultipler;
        StartCoroutine(SpeedBoostPowerDownRoution());
    }

    IEnumerator SpeedBoostPowerDownRoution()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostEnabled = false;
        _speed /= _speedMultipler;
    }

    /// <summary>
    /// In this section we are controlling the activation
    /// & deactivation of the Shields Powerup
    /// It will be remotely activated from the Powerup
    /// Script by the Trigger
    /// </summary>
    /// 

    public void ShieldsActive()
    {
        _isShieldsActive = true;
        _shieldVisualizer.SetActive(true);
    }

        public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

// ******************************* Variables ******************************************
    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private float _fireRate = 0.25f;
    private float _canFire = 0.0f;

    public bool canTripleShot = false;
    public bool canSpeedBoost = false;
    public bool shieldActive = false;

    public int lives = 3;

    private UIManager _uiManager;

    private GameManager _gameManager;

    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private GameObject _shieldGO;
    private SpawnManager _spawnManager;

    private AudioSource _audioSource;

// *************************************************************************************   
	private void Start () {

        transform.position = new Vector3(0, 0, 0);

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager != null)
        {
            _uiManager.UpdateLives(lives);
        }

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if (_spawnManager != null)
        {
            _spawnManager.StartSpawnRoutines();
        }

        _audioSource = GetComponent<AudioSource>();
    }
	


	private void Update () {

        Movement();

        if (Input.GetKeyDown(KeyCode.Space)) {

            Shoot();
            
        }

    }



    private void Shoot() {

        if (Time.time > _canFire) {

            _audioSource.Play();

            if (canTripleShot)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else { 
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            }
            _canFire = Time.time + _fireRate;
        }
    }


    private void Movement()
    {

        float horizontal_input = Input.GetAxis("Horizontal");
        float vertical_input = Input.GetAxis("Vertical");

        if (canSpeedBoost)
        {
            transform.Translate(Vector3.right * _speed * 3 * horizontal_input * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * 3 * vertical_input * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * _speed * horizontal_input * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * vertical_input * Time.deltaTime);
        }
       



        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        if (transform.position.x > 8.2f)
        {
            transform.position = new Vector3(8.2f, transform.position.y, 0);
        }
        else if (transform.position.x < -8.2f)
        {
            transform.position = new Vector3(-8.2f, transform.position.y, 0);
        }

    }


    
// ***************************** Triple Shot Powerup ******************************

    public IEnumerator TripleShotOff() {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }

    public void TripleShotOn() {
        canTripleShot = true;
        StartCoroutine(TripleShotOff());
    }


    
// ***************************** Speed Boost Powerup ******************************

    public void SpeedBoostOn()
    {
        canSpeedBoost = true;
        StartCoroutine(SpeedBoostOff());
    }


    public IEnumerator SpeedBoostOff()
    {
        yield return new WaitForSeconds(5.0f);
        canSpeedBoost = false;
    }

// ************************** Damage Control ***********************

    public void Damage()
    {
        if (shieldActive == true)
        {
            shieldActive = false;
            _shieldGO.SetActive(false);
            return;
        }

        lives -- ;
        _uiManager.UpdateLives(lives);

        if (lives < 1)
        {

            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _gameManager.gameOver = true;
            _uiManager.ShowTitleScreen();
            Destroy(this.gameObject);
        }
    }


    public void shieldOn()
    {
        shieldActive = true;
        _shieldGO.SetActive(true);
    }
}


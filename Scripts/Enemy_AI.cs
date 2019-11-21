using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour {


//**************************** Variables ***************************

    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    private GameObject _enemyExplosionPrefab;

    private UIManager _uiManager;

    [SerializeField]
    private AudioClip _clip;

// **********************************************************


	void Start () {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

    }
	
	void Update () {
        
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        
        if (transform.position.y < -7)
        {
            float randX = Random.Range(-7f, 7f);
            transform.position = new Vector3(randX, 7, 0);
        }


	}


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            if (other.transform.parent != null)
            {
                Destroy(other.transform.parent.gameObject);
            }

            Destroy(other.gameObject);
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);

            _uiManager.UpdateScore();
            _uiManager.UpdateLevel();
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
            Destroy(this.gameObject);
        }
        else if (other.tag == "Player")
        {
            Player p = other.GetComponent<Player>();
            if (p != null)
            {
                p.Damage();
            }

            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
            Destroy(this.gameObject);
        }


    }
}

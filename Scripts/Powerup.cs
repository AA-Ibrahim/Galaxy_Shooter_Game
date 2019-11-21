using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

    [SerializeField]
    private float _speed = 10.0f;
    [SerializeField]
    private int powerup_ID; // triple shot =0 speed boost=1  shield=2

    [SerializeField]
    private AudioClip _clip;
	
	// Update is called once per frame
	void Update () {

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -6)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") { 

            Player p = other.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);

            if (p != null) {

                if (powerup_ID == 0)
                {
                    p.TripleShotOn();
                }
                else if (powerup_ID == 1)
                {
                    p.SpeedBoostOn();
                }
                else if (powerup_ID == 2)
                {
                    p.shieldOn();
                }



            }
           
            Destroy(this.gameObject);
        }
    }
}

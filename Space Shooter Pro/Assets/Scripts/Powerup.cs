using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2.5f;

    private Player player;

    /// <summary>
    /// ID for Powerups
    /// 0 = Triple Shot
    /// 1 = Speed
    /// 2 = Shields
    /// </summary>
    [SerializeField]
    private int powerID;
    [SerializeField]
    private AudioClip _clip;


    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player")
        {
            player = GameObject.Find("Player").GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_clip, transform.position);

            if (player != null)
            {
                
                switch (powerID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldsActive();
                        break;
                    default:
                        break;
                }
            }
            
            Destroy(this.gameObject);
        }
                
    }
}

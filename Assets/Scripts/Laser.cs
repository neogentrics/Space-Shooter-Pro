using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    /// <summary>
    /// speed variable of 8
    /// </summary>
    [SerializeField]
    private float _speed = 8.0f;
    private bool _isEnemyLaser = false;

    void Update()
    {
        if (_isEnemyLaser == false)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
    }

    void MoveUp()
    {
        /// <summary>
        /// translate laser up
        /// Ceiling max is between 6.9 and 8
        /// </summary>

        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        /// <summary>
        /// if laser position is greater than 6.9 on the y
        /// destroy the object
        /// </summary>
        /// 

        if (transform.position.y > 7.2f)
        {
            /// <summary>
            /// Check to see if this object has a parent
            /// If true Destroy it 
            /// </summary>

            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }

        }
    }

    void MoveDown()
    {    

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y > -6.5f)
        {

            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }

        }
    }

    public void AssignEnemyLaser() => _isEnemyLaser = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag ==  "Player" && _isEnemyLaser == true)
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
        }
    }
}

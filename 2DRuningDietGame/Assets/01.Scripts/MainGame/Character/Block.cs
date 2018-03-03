using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    Vector2 _velocity = Vector2.zero;

	// Use this for initialization
	void Start ()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = _velocity;

	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector2 playerVelocity = GameManager.Instance.GetPlayer().GetVelocity();
        if(playerVelocity != _velocity)
        {
            _velocity = playerVelocity;
            gameObject.GetComponent<Rigidbody2D>().velocity = -_velocity;

        }
        if (transform.position.x < -15)
        {
            GameManager.Destroy(gameObject, 1.0f);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ("Player" == collision.tag)
        {
            GameManager.Destroy(gameObject);
            collision.gameObject.SendMessage("SetBlockSmash", true);
            collision.gameObject.SendMessage("ResetSpeed");
          
        }
    }


}

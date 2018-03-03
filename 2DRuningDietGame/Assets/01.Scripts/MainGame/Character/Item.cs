using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float AddHP;
    public float AddWeight;

    Vector2 _velocity = Vector2.zero;

    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = _velocity;

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerVelocity = GameManager.Instance.GetPlayer().GetVelocity();
        if (playerVelocity != _velocity)
        {
            _velocity = playerVelocity;
            gameObject.GetComponent<Rigidbody2D>().velocity = -_velocity;


        }

        // 자기 자신의 위치가 화면을 벗어났을 정도의 x 좌표라면 
        // 자기 자신을 파괴한다.
        if (-12.0f > transform.position.x)
        {
            GameManager.Destroy(gameObject, 1.0f);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ("Player" == collision.tag)
        {

            collision.gameObject.SendMessage("IncreaseHP", AddHP);
            collision.gameObject.SendMessage("AddWeight", AddWeight);
            collision.gameObject.SendMessage("SetBlockSmash", false);
            GameObject.Destroy(gameObject);
        }
    }
}

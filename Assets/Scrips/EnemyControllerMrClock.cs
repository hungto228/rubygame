using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerMrClock : MonoBehaviour
{
    Animator anim;
    Rigidbody2D body;
    public float speed = 3f;
    public bool vertical;
    public float changeTime = 3f;

    //smoke 
    public ParticleSystem smokeEffect;
    float timer;
    int direction = 1;

    // projectitle avilable
    bool broken = true;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        timer = changeTime;
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        // projectitle avilable
        if (!broken)
        {
            return ;
        }
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

  

    void FixedUpdate()
        
    {
        // projectitle avilable
        if (!broken)
        {
            return;
        }
        Vector2 position = body.position;
        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
            anim.SetFloat("Blendx", 0);

            anim.SetFloat("Blendy", direction);
            Debug.Log(direction + "vetical");
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            anim.SetFloat("Blendx", direction);
            anim.SetFloat("Blendy", 0);
            Debug.Log(direction + "hổintal");
        }
        body.MovePosition(position);
    }
    //void OnCollisionEnter2D(Collision2D other)
    //{
    //    RubyController player = other.gameObject.GetComponent<RubyController>();

    //    if (player != null)
    //    {
    //        player.ChangeHeath(-1);
    //    }
    //}
    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            controller.ChangeHeath(-1);
        }
    }
    public void Fix()
    {
        broken = false;
        body.simulated = false;
        anim.SetTrigger("Fixed");
        smokeEffect.Stop();
    }
}

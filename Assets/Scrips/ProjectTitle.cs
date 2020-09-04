using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTitle : MonoBehaviour
{
    public AudioClip projetitleClip;
    Rigidbody2D body;
    // Start is called before the first frame update
   void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }


    //Update is called once per frame
    void Update()
    {
        if (transform.position.magnitude > 40f)
        {
            Destroy(gameObject);
        }
    }
    public void Launch(Vector2 direction, float force)
    {
        body.AddForce(direction * force);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        RubyController ruby = collision.GetComponent<RubyController>();
        if (ruby != null)
        {
            ruby.PlaySound(projetitleClip);
        }
        EnemyControllerMrClock e = collision.GetComponent<EnemyControllerMrClock>();
        if (e != null)
        {
            e.Fix();
          
        }
        
        Debug.Log("projectitle collision with" + collision.gameObject);
        Destroy(gameObject);
    }
    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    EnemyControllerMrClock e = collision.collider.GetComponent<EnemyControllerMrClock>();
    //    if (e != null)
    //    {
    //        e.Fix();
    //    }
    //    Debug.Log("projectitle collision with" + collision.gameObject);
    //    Destroy(gameObject);
    //}
}

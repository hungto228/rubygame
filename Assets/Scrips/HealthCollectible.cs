using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public AudioClip collectedClip;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        RubyController rubyController = collision.GetComponent<RubyController>();
        if (rubyController != null)
        {
            if (rubyController.heath < rubyController.maxHeath)
            {
                rubyController.ChangeHeath(1);
                Destroy(gameObject);
                rubyController.PlaySound(collectedClip);
            }
          
        }
        //Debug.Log("emter player" + collision);
    }
}

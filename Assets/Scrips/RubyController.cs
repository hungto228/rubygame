using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
   public GameObject dameEffect;
    public GameObject collectEffect;
    //projectitle 
    public GameObject projecTitleprefab;
    public float speed = 3f;
    public int maxHeath = 5;
    //repeat dame
    public float timeInvincible = 2.0f;
    public int heath
    {
        get
        {
            return currentHeath;
        }
    }
   public int currentHeath;

    bool isInvincible;
    float invincibleTimer;
    Rigidbody2D body;
    float horizontal;
    float vertical;

    // move  in blend
    Animator anim;
    Vector2 lookDiretion = new Vector2(1, 0);

    //audioSouce
    AudioSource audioSource;
    // Start is called before the first frame update

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        currentHeath = maxHeath;
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 10;

        audioSource = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
       
      horizontal = Input.GetAxis("Horizontal");
       vertical = Input.GetAxis("Vertical");

        //update move blend 
        Vector2 move = new Vector2(vertical, horizontal);
        //Sử dụng Mathf.Approximately thay vì == vì cách máy tính lưu trữ số thực có nghĩa là
        //có một sự mất mát nhỏ về độ chính xác.
        if (!Mathf.Approximately(move.x,0.0f)||!Mathf.Approximately(move.y,0.0f))
        {
            lookDiretion.Set(move.y, move.x);
            // để làm cho chiều dài của nó bằng 1
            lookDiretion.Normalize();
        }
        anim.SetFloat("Look Y", lookDiretion.y);
        anim.SetFloat("Look X", lookDiretion.x);
        anim.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Launch();
        }
        //NPC
        // if (Input.GetMouseButton(0))
        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(body.position + Vector2.up * 0.2f, lookDiretion, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharater nonPlayerCharater = hit.collider.GetComponent<NonPlayerCharater>();
                if (nonPlayerCharater != null)
                {
                    nonPlayerCharater.DisplayDialog();
                }
              //  Debug.Log("Raycast has hit the object " + hit.collider.gameObject);
            }
        }
    }
   // horizontal
     void FixedUpdate()
    {
        Vector2 positon = transform.position;
        positon.x = positon.x +speed * horizontal * Time.deltaTime;
        positon.y = positon.y + speed * vertical * Time.deltaTime;
        body.MovePosition(positon);
    }
  public  void ChangeHeath(int amount)
    {
        if (amount < 0)
        {
            anim.SetTrigger("Hit");       
            if (isInvincible ){
                return;
            }
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        // nếu player còn 1 lượng máu và cố gắng loại bỏ ,
        //player sẽ chuyển sang trạng thái âm.currentHealth + số tiền) không bao giờ thấp hơn tham số thứ hai
        currentHeath = Mathf.Clamp(currentHeath + amount, 0, maxHeath);
        //uiHeathBar
        UiHeathBar.intance.SetValues(currentHeath / (float)maxHeath);
        Debug.Log(currentHeath + "/" + maxHeath);
     
    }
    //launch in projecttitle
    void Launch()
    {
        GameObject projecTitelObject = Instantiate(projecTitleprefab, body.position + Vector2.up * 0.5f, Quaternion.identity);
        ProjectTitle project = projecTitelObject.GetComponent<ProjectTitle>();
        project.Launch(lookDiretion, 300);
        anim.SetTrigger("Launch");
   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Damage")
        {
            Instantiate(dameEffect, collision.transform.position, Quaternion.identity);
        }
        if (collision.tag == "Collect")
        {
            Instantiate(collectEffect, collision.transform.position, Quaternion.identity);
        }
    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}

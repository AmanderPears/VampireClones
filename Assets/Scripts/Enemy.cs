using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float movementSpeed;
    public float positionOffset;
    public float healthPoint;
    Animator animator;


    public GameObject playerReference;
    public Player playerScript;

    private void Awake()
    {
        //set color to red
        //gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        movementSpeed = 0.25f;
        positionOffset = 0f;
        healthPoint = 1;
        animator = gameObject.GetComponent<Animator>();



        playerReference = GameObject.FindGameObjectWithTag("Player");
        playerScript = playerReference.GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        KillIfNoHp();
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        // Move our position a step closer to the target.
        var step = movementSpeed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, playerReference.transform.position, step);


        var vec = playerReference.transform.position - transform.position;
        var rot = Quaternion.LookRotation(vec);
        transform.rotation = rot;

        //set animation
        animator.SetTrigger("walk");
    }


    public void TakeDamage(float value)
    {
        this.healthPoint -= value;
    }

    void KillIfNoHp()
    {
        if (this.healthPoint <= 0)
        {
            //Debug.Log("I was killed...");
            Destroy(gameObject);

        }
    }
}

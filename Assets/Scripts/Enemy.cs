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

        //transform.position = getRandomPosition();


        playerReference = GameObject.FindGameObjectWithTag("Player");
        playerScript = playerReference.GetComponent<Player>();


    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        killIfNoHp();
        moveTowardsPlayer();
    }

    private void FixedUpdate()
    {
        
    }

    private void moveTowardsPlayer()
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


    private Vector3 getRandomPosition()
    {
        var lowerBound = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        var upperBound = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 1));


        var x = Random.Range(lowerBound.x, upperBound.x) + positionOffset;
        var y = positionOffset;
        var z = Random.Range(lowerBound.z, upperBound.z) + positionOffset;

        return new Vector3(x, y, z);
    }

    public void OnMouseDown()
    {
        transform.position = getRandomPosition();
    }


    public void TakeDamage(float value)
    {
        this.healthPoint -= value;
    }

    void killIfNoHp()
    {
        if (this.healthPoint <= 0)
        {
            //this.healthPoint = 1;
            //transform.position = getRandomPosition();
            //Debug.Log("I was killed...");
            Destroy(gameObject);

        }
    }
}

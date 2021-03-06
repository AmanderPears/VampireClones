using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float movementSpeed;

    public int healthPoints;
    public int maxHealthPoints;
    public HealthBar healthBar;

    public float pickupRadius;
    public int experienceLevel;
    public float experience;
    public float maxExperience;
    public ExperienceBar experienceBar;

    Animator animator;

    //public GameObject enemyReference;
    //public Enemy enemyScript;

    public GameObject MainCamera;
    private Quaternion cameraRotation;

    public GameObject attackPrefab;
    private List<Attack> attackList;

    public Renderer _renderer;

    public GameObject attacksParent;

    private void Awake()
    {
        //gameObject.GetComponent<Renderer>().material.color = Color.red;
        transform.position = new Vector3((20 / 2), 0, (20 / 2));
        movementSpeed = .3f;
        animator = gameObject.GetComponent<Animator>();


        maxHealthPoints = 20;
        healthPoints = maxHealthPoints;
        healthBar.SetMaxHealth(maxHealthPoints);
        healthBar.SetHealth(healthPoints);

        pickupRadius = 0.1f;
        experience = 0;
        experienceLevel = 0;
        maxExperience = 20 * experienceLevel * 1.2f;
        experienceBar.SetMaxExperience(100);
        experienceBar.SetExperience(0);
        experienceBar.SetLevel(1);

        ////enemy referebce
        //enemyReference = GameObject.FindGameObjectWithTag("Enemy");
        //enemyScript = enemyReference.GetComponent<Enemy>();

        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        cameraRotation = MainCamera.transform.rotation;


        attackList = new List<Attack>();

        _renderer = transform.GetChild(0).transform.GetComponent<Renderer>();
    }


    // Update is called once per frame
    void Update()
    {
        animate();

        movement();

        //Debug.Log(transform.position);
        updateAttack();
    }

    //Detect collisions between the GameObjects with Colliders attached
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("there was collosiog: " + collision.gameObject.tag);

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "enemy")
        {
            Debug.Log(--healthPoints);
            healthBar.SetHealth(healthPoints);
        }
    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        //If the GameObject has the same tag as specified, output this message in the console
    //        Debug.Log(healthPoints--);
    //        //StartCoroutine(wait(0.5f));
    //        wait(1);
    //        Debug.Log("Col Stay");
    //    }
    //}

    //IEnumerator wait(float waitTimeSeconds)
    //{
    //    yield return new WaitForSeconds(waitTimeSeconds);
    //}


    void movement()
    {
        var move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.position += move.normalized * movementSpeed * Time.deltaTime;
    }

    void animate()
    {

        //cross
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            MainCamera.transform.rotation = cameraRotation; 
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.localRotation = Quaternion.Euler(0, 90f, 0);
            MainCamera.transform.rotation = cameraRotation;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            MainCamera.transform.rotation = cameraRotation;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localRotation = Quaternion.Euler(0, 270f, 0);
            MainCamera.transform.rotation = cameraRotation;
        }


        //diagonal
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))
        {
            transform.localRotation = Quaternion.Euler(0, 45f, 0);
            MainCamera.transform.rotation = cameraRotation;
        }

        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            transform.localRotation = Quaternion.Euler(0, 135f, 0);
            MainCamera.transform.rotation = cameraRotation;
        }

        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            transform.localRotation = Quaternion.Euler(0, 225f, 0);
            MainCamera.transform.rotation = cameraRotation;
        }


        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
        {
            transform.localRotation = Quaternion.Euler(0, 315f, 0);
            MainCamera.transform.rotation = cameraRotation;
        }


        if (Input.GetKey(KeyCode.RightArrow)
            || Input.GetKey(KeyCode.LeftArrow)
            || Input.GetKey(KeyCode.UpArrow)
            || Input.GetKey(KeyCode.DownArrow))
        {
            animator.SetTrigger("walk");
        }

        if (Input.GetKeyUp(KeyCode.RightArrow)
            || Input.GetKeyUp(KeyCode.LeftArrow)
            || Input.GetKeyUp(KeyCode.UpArrow)
            || Input.GetKeyUp(KeyCode.DownArrow))
        {
            animator.ResetTrigger("walk");
            animator.SetTrigger("idle");
        }

    }


    private void FixedUpdate()
    {
        attack();
        PickUpXpOrb();
    }


    void attack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.5f, 1 << LayerMask.NameToLayer("enemy"));

        float smallestDistance = float.MaxValue;
        Collider nearestCollider = null;

        foreach (var hitCollider in hitColliders)
        {
            //Debug.Log(hitCollider.name);

            float dist = Vector3.Distance(
                hitCollider.transform.GetComponent<Renderer>().bounds.center,
                _renderer.bounds.center);


            if (dist < smallestDistance)
            {
                //Debug.Log(smallestDistance + " | " + dist);
                smallestDistance = dist;
                nearestCollider = hitCollider;
            }


        }

        if (nearestCollider != null)
        {
            //Debug.Log("Attacking: " + nearestCollider.gameObject.transform.parent.name);
            createAttack(nearestCollider.transform.GetComponent<Renderer>().bounds.center);
        }

    }

    private bool attackOnCooldown = false;
    void createAttack(Vector3 destination)
    {
        if (attackOnCooldown) return;
        //Debug.Log("ATTACKING!!");
        var gameobj = Instantiate(attackPrefab, _renderer.bounds.center, Quaternion.identity);
        //gameobj.tag = "attacks";
        //gameobj.layer = LayerMask.NameToLayer("attacks");

        var gameMeshObj = gameobj.transform.GetChild(0).transform.GetChild(0).gameObject;
        gameMeshObj.AddComponent<BoxCollider>();
        var rigidBody = gameMeshObj.AddComponent<Rigidbody>();
        rigidBody.useGravity = false;
        Attack tmp = new Attack(gameobj, destination);
        attackList.Add(tmp);
        StartCoroutine(attackCooldown());
    }

    IEnumerator attackCooldown()
    {
        attackOnCooldown = true;
        yield return new WaitForSeconds(5f);
        attackOnCooldown = false;
    }

    void updateAttack()
    {
        attackList.RemoveAll(a => !a.obj.activeSelf);
        //Debug.Log("count: " + attackList.Count);
        foreach (var attack in attackList)
        {
            if (!attack.obj.activeSelf)
            {
                continue;
            }


            if (attack.obj.transform.position.Equals(attack.dest))
            {
                attack.obj.SetActive(false);
                continue;
            }


            attack.obj.transform.position = Vector3.MoveTowards(attack.obj.transform.position, attack.dest, 0.5f * Time.deltaTime);

        }
    }


    void updateHealthBar()
    {
        healthBar.SetMaxHealth(maxHealthPoints);
        healthBar.SetHealth(healthPoints);
    }




    void PickUpXpOrb()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, pickupRadius, 1 << LayerMask.NameToLayer("pickups"));


        foreach (var hitCollider in hitColliders)
        {
            var xpOrb = hitCollider.GetComponent<XpOrb>();
            xpOrb.picked = true;
        }

    }

    public void UpdateExperience(float value = 2f)
    {
        experience += value * 2;

        if (experience >= maxExperience)
        {
            experienceLevel++;
            experience -= maxExperience;
            maxExperience = 20 * experienceLevel * 1.2f;
        }

        experienceBar.SetExperience((experience / maxExperience) * 100);
        experienceBar.SetLevel(experienceLevel);
    }


}



class Attack
{
    public GameObject obj;
    public Vector3 dest;

    public Attack(GameObject obj, Vector3 dest)
    {
        this.obj = obj;
        this.dest = dest;
    }
}
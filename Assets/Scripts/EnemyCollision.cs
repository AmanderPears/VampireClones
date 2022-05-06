using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //Debug.Log("ASDASD");
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("EnemyChild: " + collision.transform.tag + " | " + collision.transform.name);
        //var obj = collision.transform.gameObject;
        //Debug.Log("Parent: " + obj.tag + " | " + obj.name);

        Debug.Log("ENEMY COLLISION");

        if (collision.transform.tag.ToLower().Equals("attacks"))
        {
            var script = transform.GetComponentInParent<Enemy>();
            script.TakeDamage(1);

            collision.gameObject.SetActive(false);
        }
    }
}

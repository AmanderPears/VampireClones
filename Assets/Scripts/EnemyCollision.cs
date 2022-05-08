using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("EnemyChild: " + collision.transform.tag + " | " + collision.transform.name);
        //var obj = collision.transform.gameObject;
        //Debug.Log("Parent: " + obj.tag + " | " + obj.name);

        //Debug.Log("ENEMY COLLISION");

        if (collision.transform.tag.ToLower().Equals("attacks"))
        {
            var script = transform.GetComponentInParent<Enemy>();
            script.TakeDamage(1);

            collision.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.ToLower().Equals("attacks"))
        {
            //Debug.Log("Collision with other");
            var script = transform.GetComponentInParent<Enemy>();
            script.TakeDamage(1);

            other.gameObject.SetActive(false);
        }
    }
}

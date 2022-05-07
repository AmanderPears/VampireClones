using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    public Player player;
    public HealthBar healthBar;

    private bool isInvulnerable = false;

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.transform.name + " | " + collision.transform.tag);
        healthBar.SetHealth(--player.healthPoints);
    }


    private void OnCollisionStay(Collision collision)
    {
        if (!isInvulnerable)
        {
            healthBar.SetHealth(--player.healthPoints);
            StartCoroutine(DamageCooldown(0.5f));
        }
    }

    IEnumerator DamageCooldown(float value)
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(value);
        isInvulnerable = false;
    }
}

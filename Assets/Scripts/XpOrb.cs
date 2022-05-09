using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpOrb : MonoBehaviour
{
    private GameObject player;
    private Player playerScript;

    public bool picked = false;
    public float moveSpeedFactor = 0.3f;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        if (picked)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                playerScript._renderer.bounds.center,
                moveSpeedFactor * Time.deltaTime
                );

            if (transform.position.Equals(playerScript._renderer.bounds.center)) {
                Destroy(transform.gameObject);
                Debug.Log("dead");
            }
        }
    }
}

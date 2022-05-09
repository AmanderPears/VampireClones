using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{

    public float gameWidth;
    public float gameHeight;
    public float gameScale;

    public GameObject enemy_1;
    public GameObject enemy_2;

    public List<GameObject> enemyList = new List<GameObject>();


    public GameObject xpOrbParent;
    public GameObject xpOrb;

    private void Awake()
    {
        gameHeight = 20;
        gameWidth = 20;
        gameScale = 1;

        //might have to change
        xpOrbParent = Instantiate(xpOrbParent);
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnEnemy", 0, 5);
    }

    private int enemyCount = 0;
    void spawnEnemy()
    {

        var prefab = Random.Range(0f, 1f) <= 0.5f ? enemy_1 : enemy_2;
        GameObject _enemy = Instantiate(prefab, GetRandomPosition(), Quaternion.identity);
        _enemy.transform.parent = transform;
        _enemy.gameObject.name = "enemy_" + enemyCount++;
        _enemy.transform.localScale = new Vector3(5, 5, 5);

        var script = _enemy.AddComponent<Enemy>();
        script.xpOrbParent = xpOrbParent;
        script.xpOrb = xpOrb;

        var objMesh = _enemy.GetComponentInChildren<SkinnedMeshRenderer>();

        var boxCollider = objMesh.gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = true;

        objMesh.gameObject.AddComponent<EnemyCollision>();

        var rigidBody = objMesh.gameObject.AddComponent<Rigidbody>();
        rigidBody.useGravity = false;
        rigidBody.constraints = RigidbodyConstraints.FreezePositionY;
        rigidBody.mass = float.MaxValue;

        _enemy.tag = "enemy";

        int layerMaskIndex = LayerMask.NameToLayer("enemy");
        _enemy.layer = layerMaskIndex;
        for (int childIndex = 0; childIndex < _enemy.transform.childCount; childIndex++)
        {
            _enemy.transform.GetChild(childIndex).gameObject.layer = layerMaskIndex;
        }

        enemyList.Add(_enemy);
    }

    Vector3 GetRandomPosition()
    {
        var lowerBoundLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.farClipPlane));
        var upperBoundRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.farClipPlane));


        float x, y = 0, z;

        //if (((int)Random.Range(0, 2)) == 0)
        if (Random.value > 0.5f)
        {
            //Debug.Log("x varies, z fixed");
            x = Random.Range(lowerBoundLeft.x, upperBoundRight.x);
            //z = (int)Random.Range(0, 2) == 0 ? upperBoundRight.z : lowerBoundLeft.z;
            z = Random.value > 0.5f ? upperBoundRight.z : lowerBoundLeft.z;
        }
        else
        {
            //Debug.Log("z varies, x fixed");
            //x = (int)Random.Range(0, 2) == 0 ? upperBoundRight.x : lowerBoundLeft.x;
            x = Random.value > 0.5f ? upperBoundRight.x : lowerBoundLeft.x;
            z = Random.Range(upperBoundRight.z, lowerBoundLeft.z);
        }

        return new Vector3(x, y, z);
    }
}

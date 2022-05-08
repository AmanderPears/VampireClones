using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{

    public float gameWidth;
    public float gameHeight;
    public float gameScale;

    public GameObject enemy_1;
    public GameObject enemy_2;


    private GameObject[] enemyList;

    private GameObject spawnPlane;

    private void Awake()
    {
        gameHeight = 20;
        gameWidth = 20;
        gameScale = 1;


        //createSpawnPlane();
    }

    // Start is called before the first frame update
    void Start()
    {

        //enemyList = new GameObject[5];

        //for (int i = 0; i < 5; i++)
        //{
        //    var prefab = Random.Range(0f, 1f) <= 0.5f ? enemy_1 : enemy_2;
        //    float x = Random.Range(0, gameWidth);
        //    float z = Random.Range(0, gameHeight);
        //    enemyList[i] = Instantiate(prefab, new Vector3(x, 0, z), Quaternion.identity);
        //    enemyList[i].transform.parent = transform;
        //    enemyList[i].gameObject.name = "enemy_" + i;
        //    enemyList[i].transform.localScale = new Vector3(5, 5, 5);
        //    enemyList[i].AddComponent<Enemy>();

        //    var objMesh = enemyList[i].GetComponentInChildren<SkinnedMeshRenderer>();
        //    objMesh.gameObject.AddComponent<BoxCollider>();
        //    objMesh.gameObject.AddComponent<EnemyCollision>();
        //    var rigidBody = objMesh.gameObject.AddComponent<Rigidbody>();
        //    rigidBody.useGravity = false;
        //    rigidBody.constraints = RigidbodyConstraints.FreezePositionY;
        //    enemyList[i].tag = "enemy";

        //    int layerMaskIndex = LayerMask.NameToLayer("enemy");
        //    enemyList[i].layer = layerMaskIndex;
        //    for (int childIndex = 0; childIndex < enemyList[i].transform.childCount; childIndex++)
        //    {
        //        enemyList[i].transform.GetChild(childIndex).gameObject.layer = layerMaskIndex;
        //    }




        //}

        InvokeRepeating("spawnEnemy", 0, 1);

    }

    private int enemyCount = 0;
    void spawnEnemy()
    {

        var prefab = Random.Range(0f, 1f) <= 0.5f ? enemy_1 : enemy_2;
        GameObject _enemy = Instantiate(prefab, getRandomPosition(), Quaternion.identity);
        _enemy.transform.parent = transform;
        _enemy.gameObject.name = "enemy_" + enemyCount++;
        _enemy.transform.localScale = new Vector3(5, 5, 5);
        _enemy.AddComponent<Enemy>();

        var objMesh = _enemy.GetComponentInChildren<SkinnedMeshRenderer>();

        var boxCollider = objMesh.gameObject.AddComponent<BoxCollider>();
        //boxCollider.isTrigger = true;

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
    }

    Vector3 getRandomPosition()
    {
        var lowerBoundLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.farClipPlane));
        var upperBoundRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.farClipPlane));


        float x, y = 0, z;

        if (((int)Random.Range(0, 2)) == 0)
        {
            //Debug.Log("x varies, z fixed");
            x = Random.Range(lowerBoundLeft.x, upperBoundRight.x);
            z = (int)Random.Range(0, 2) == 0 ? upperBoundRight.z : lowerBoundLeft.z;
        }
        else
        {
            //Debug.Log("z varies, x fixed");
            x = (int)Random.Range(0, 2) == 0 ? upperBoundRight.x : lowerBoundLeft.x;
            z = Random.Range(upperBoundRight.z, lowerBoundLeft.z);
        }

        return new Vector3(x, y, z);
    }
}

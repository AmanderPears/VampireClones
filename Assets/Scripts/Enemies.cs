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

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        
    }

    private int enemyCount = 0;
    void spawnEnemy()
    {

        var prefab = Random.Range(0f, 1f) <= 0.5f ? enemy_1 : enemy_2;
        float x = Random.Range(0, gameWidth);
        float z = Random.Range(0, gameHeight);
        GameObject _enemy = Instantiate(prefab, new Vector3(x, 0, z), Quaternion.identity);
        _enemy.transform.parent = transform;
        _enemy.gameObject.name = "enemy_" + enemyCount++;
        _enemy.transform.localScale = new Vector3(5, 5, 5);
        _enemy.AddComponent<Enemy>();

        var objMesh = _enemy.GetComponentInChildren<SkinnedMeshRenderer>();
        objMesh.gameObject.AddComponent<BoxCollider>();
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


    void createSpawnPlane()
    {

        float w = 2f;
        float h = 2f;

        var player = GameObject.FindGameObjectWithTag("Player");
        spawnPlane = new GameObject();
        var mf = spawnPlane.AddComponent<MeshFilter>();
        var mr = spawnPlane.AddComponent<MeshRenderer>();

        var m = new Mesh();
        m.vertices = new Vector3[]
        {
            new Vector3(0,0,0),
            new Vector3(w,0,0),
            new Vector3(w,h,0),
            new Vector3(0,h,0)
        };

        m.uv = new Vector2[]
        {
            new Vector2(0,0),
            new Vector2(1,0),
            new Vector2(1,1),
            new Vector2(0,1)
        };

        m.triangles = new int[] { 0, 1, 2, 0, 2, 3 };

        mf.mesh = m;
        m.RecalculateBounds();
        m.RecalculateNormals();


        spawnPlane.transform.Rotate(new Vector3(90, 0, 0));
        spawnPlane.transform.parent = player.transform;
        spawnPlane.transform.position = player.transform.position;
        //spawnPlane.transform.


    }
}

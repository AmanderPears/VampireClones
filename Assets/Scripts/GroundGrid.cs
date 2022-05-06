using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGrid : MonoBehaviour
{

    private int height = 20;
    private int width = 20;
    private float gridSpaceSize = 1f;

    [SerializeField] private GameObject groundCellPrefab;
    private GameObject[,] groundGrid;
    private GameObject groundGridParent;


    private void CreateGroundGrid()
    {
        groundGrid = new GameObject[height, width];

        if (groundCellPrefab == null)
        {
            Debug.LogError("ERROR: no prefab...");
        }

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                groundGrid[x, y] = Instantiate(groundCellPrefab, new Vector3(x * gridSpaceSize, 0f, y * gridSpaceSize), Quaternion.identity);
                groundGrid[x, y].transform.parent = transform;
                groundGrid[x, y].gameObject.name = "Ground Grid ( X: " + x.ToString() + ", Z: " + y.ToString() + ")";
            }
        }

    }

    private void Awake()
    {
        groundGridParent = GameObject.FindGameObjectWithTag("GroundGrid");
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateGroundGrid();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

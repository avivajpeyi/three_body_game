using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrid : MonoBehaviour
{

    public int rows = 5;
    public int cols = 8;
    public float tilesize = 1;
    public GameObject tilePrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        Generate();
    }

    public void Generate()
    {
        
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                GameObject t = Instantiate(tilePrefab);
                t.transform.position = new Vector2(
                    c * tilesize,
                    r * -tilesize
                );
            }
        }

        float gwidth = cols * tilesize;
        float gheight = rows * tilesize;
        float halftile = tilesize / 2;
        transform.position = new Vector2(
            -gwidth /2 + halftile,
             gheight/ 2 - halftile
            );

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

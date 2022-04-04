using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class SpawnPoints : MonoBehaviour
{
    private List<Transform> pts = new List<Transform>();

    [SerializeField] private Transform PlayerSpawnPt;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            if (child!=PlayerSpawnPt)
                pts.Add(child);
        }
        
    }

    private int GetRandId()
    {
        return UnityEngine.Random.Range(0, pts.Count);
    }

    public List<Transform> GetSpawnPoints(int num)
    {
        List<Transform> rndPts = new List<Transform>();
        List<int> ids = new List<int>(); 
        rndPts.Add(PlayerSpawnPt);
        
        while(rndPts.Count < num)
        {
            int newId = GetRandId();
            while (ids.Contains(newId))
            {
                newId = GetRandId();
            }
            rndPts.Add(pts[newId]);
            ids.Add(newId);
        }

        
        return rndPts;
    }
}
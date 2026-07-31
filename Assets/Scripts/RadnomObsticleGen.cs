using System;
using UnityEngine;

public class RadnomObsticleGen : MonoBehaviour
{

    public enum PrefabObsticle { platform, rod, cabel };

    public GameObject prefabPlatform;
    public GameObject prefabRod;
    public GameObject prefabCabel;
    private float nextY = 0f;

    int buffor = 0;
    float Margin = 1f;
    float DrawnBefore = 0;
    float DrawnNow = 0;


    private void Start()
    {

        nextY = 5;
    }

    public void FixedUpdate()
    {

        if (transform.position.y*2 > nextY)
        {
            GenerateRandObsticles();  
        }

    }
    
    public void GenerateRandObsticles()
    {
        SmartHingeScaler scaler = GetComponentInChildren<SmartHingeScaler>();
        PrefabObsticle drawn = (PrefabObsticle)UnityEngine.Random.Range(0, 3 - buffor);

        DrawnNow = DrawnBefore;
       
        
        while (Math.Abs(DrawnNow - DrawnBefore) < Margin)
        {
            DrawnNow = UnityEngine.Random.Range(-5, 5); 
        }

        DrawnBefore = DrawnNow;

        Vector3 position = new Vector3(DrawnNow+PolesGeneration.GeneralX, nextY, 0);

        GameObject Place;
        if (drawn == PrefabObsticle.platform) { Place = prefabPlatform; buffor = 0; Margin = 1f; }
        else if (drawn == PrefabObsticle.rod) { Place = prefabRod; buffor = 0; Margin = 1f; }
        else { Place = prefabCabel; nextY -= 4f; buffor = 1; Margin = 3f; }

        float rotationY = (UnityEngine.Random.value > 0.5f) ? 0f : 180f;
        Quaternion randomRotation = Quaternion.Euler(0, rotationY, 0);

        GameObject newPlatform = Instantiate(Place, position, randomRotation);

        nextY += 5f;

        transform.root.BroadcastMessage("AdjustMe", transform, SendMessageOptions.DontRequireReceiver);
    }
}

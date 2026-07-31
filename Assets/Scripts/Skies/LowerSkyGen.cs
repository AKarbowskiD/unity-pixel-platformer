using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LowerSkyGen : MonoBehaviour
{

    public enum PrefabDeco { CloudA,CloudB,CloudC,Birds };

    public GameObject prefabCloudA;
    public GameObject prefabCloudB;
    public GameObject prefabCloudC;
    public GameObject prefabBirds;
    
    public static float nextY = 0f;
    private float Drawn = 0f;


    private void Start()
    {

        nextY = 8f;
    }

    private void FixedUpdate()
    {
        if (transform.position.y * 2 > nextY)
        {
            GenerateRadnomDeco();
        }
    }

    private void GenerateRadnomDeco()
    {
        for (int i = 0; i < 2; i++)
        {
            PrefabDeco drawn = (PrefabDeco)UnityEngine.Random.Range(0, 4);

            Drawn = UnityEngine.Random.Range(-15, 15);

            Vector3 position = new Vector3(Drawn + PolesGeneration.GeneralX, nextY + UnityEngine.Random.Range(-2f, 2f), 1f);

            GameObject Place;
            if (drawn == PrefabDeco.CloudA) { Place = prefabCloudA; }
            else if (drawn == PrefabDeco.CloudB) { Place = prefabCloudB; }
            else if (drawn == PrefabDeco.CloudC) { Place = prefabCloudC; }
            else { Place = prefabBirds; }


            Instantiate(Place, position, Quaternion.identity);

            Drawn = 0;
            nextY += 5f;

        }
        nextY += 10f;
    }
}

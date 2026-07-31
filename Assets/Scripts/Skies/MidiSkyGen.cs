using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MidiSkyGen : MonoBehaviour
{

    public enum PrefabDeco { BaloonA, BaloonB, Plane};

    public GameObject prefabBaloonA;
    public GameObject prefabBaloonB;
    public GameObject prefabPlane;
    
    public static float nextY = 510f;
    private float Drawn = 0f;



    private void Start()
    {
        nextY = 510f;
    }

    private void FixedUpdate()
    {
        if (800 > nextY)
        {
            GenerateRadnomDeco();
        }
    }

    private void GenerateRadnomDeco()
    {
            PrefabDeco drawn = (PrefabDeco)UnityEngine.Random.Range(0, 4);

            Drawn = UnityEngine.Random.Range(-15, 15);


            Vector3 position = new Vector3(Drawn + PolesGeneration.GeneralX, nextY + UnityEngine.Random.Range(-2f, 2f), 1f);

            GameObject Place;
            if (drawn == PrefabDeco.BaloonA) { Place = prefabBaloonA; }
            else if (drawn == PrefabDeco.BaloonB) { Place = prefabBaloonB; }
            else { Place = prefabPlane; }

            Instantiate(Place, position, Quaternion.identity);

            Drawn = 0;
        
        nextY += 20f;
    }
}

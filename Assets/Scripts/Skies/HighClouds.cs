using UnityEngine;

public class HighCloudsGen : MonoBehaviour
{

    public enum PrefabDeco { CloudAA, CloudBB, CloudCC, CloudDD, CloudEE, BaloonC };

    public GameObject prefabCloudAA;
    public GameObject prefabCloudBB;
    public GameObject prefabCloudCC;
    public GameObject prefabCloudDD;
    public GameObject prefabCloudEE; 
    public GameObject prefabBaloonC;

    public static float nextY = 810f;
    private float Drawn = 0f;



    private void Start()
    {
        nextY = 810f;
    }

    private void FixedUpdate()
    {
        if (1200 > nextY)
        {
            GenerateRadnomDeco();
        }
    }

    private void GenerateRadnomDeco()
    {
        PrefabDeco drawn = (PrefabDeco)UnityEngine.Random.Range(0, 6);

        Drawn = UnityEngine.Random.Range(-15, 15);


        Vector3 position = new Vector3(Drawn + PolesGeneration.GeneralX, nextY + UnityEngine.Random.Range(-2f, 2f), 1f);

        GameObject Place;
        if (drawn == PrefabDeco.CloudAA) { Place = prefabCloudAA; }
        else if (drawn == PrefabDeco.CloudBB) { Place = prefabCloudBB; }
        else if (drawn == PrefabDeco.CloudCC) { Place = prefabCloudCC; }
        else if (drawn == PrefabDeco.CloudDD) { Place = prefabCloudDD; }
        else if (drawn == PrefabDeco.CloudEE) { Place = prefabCloudEE; }
        else { Place = prefabBaloonC; }

        Instantiate(Place, position, Quaternion.identity);

        Drawn = 0;

        nextY += 25f;
    }
}

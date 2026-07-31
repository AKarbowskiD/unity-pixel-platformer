using UnityEngine;

public class SpaceEdgeGen : MonoBehaviour
{

    public enum PrefabDeco { Satelite, Rocket, AsteroidA, AsteroidB };

    public GameObject prefabSatelite;
    public GameObject prefabRocket;
    public GameObject prefabAsteroidA;
    public GameObject prefabAsteroidB;


    public static float nextY = 1210f;
    private float Drawn = 0f;



    private void Start()
    {
        nextY = 1210f;
    }

    private void FixedUpdate()
    {
        if (2000 > nextY)
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
        if (drawn == PrefabDeco.Satelite) { Place = prefabSatelite; }
        else if (drawn == PrefabDeco.Rocket) { Place = prefabRocket; }
        else if (drawn == PrefabDeco.AsteroidA) { Place = prefabAsteroidA; }
        else { Place = prefabAsteroidB; }

        Instantiate(Place, position, Quaternion.identity);

        Drawn = 0;

        nextY += 50f;
    }
}

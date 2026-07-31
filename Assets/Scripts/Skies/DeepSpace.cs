using UnityEngine;

public class DeepSpace : MonoBehaviour
{

    public enum PrefabDeco { Astornaut, Rock, Trash, SolarPanel, RocketFull };

    public GameObject prefabAstornaut;
    public GameObject prefabRock;
    public GameObject prefabTrash;
    public GameObject prefabSolarPanel;
    public GameObject prefabRocketFull;

    public static float nextY = 2010f;
    private float Drawn = 0f;



    private void Start()
    {
        nextY = 2010f;
    }

    private void FixedUpdate()
    {
        if (PolesGeneration.height > nextY)
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
        if (drawn == PrefabDeco.Astornaut) { Place = prefabAstornaut; }
        else if (drawn == PrefabDeco.Rock) { Place = prefabRock; }
        else if (drawn == PrefabDeco.Trash) { Place = prefabTrash; }
        else if (drawn == PrefabDeco.SolarPanel) { Place = prefabSolarPanel; }
        else { Place = prefabRocketFull; }

        Instantiate(Place, position, Quaternion.identity);

        Drawn = 0;

        nextY += 40f;
    }
}

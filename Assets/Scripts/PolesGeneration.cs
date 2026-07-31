using UnityEngine;

public class PolesGeneration : MonoBehaviour
{
    public GameObject FuseboxPrefab;
    public GameObject PolePrefab;
    public GameObject PoleTopPrefab;
    public GameObject GrassPrefab;
    public GameObject CabelPrefab;
    public GameObject SkyPrefab;

    public GameObject Player;

    public enum SkyLevel
    {
        LowerSky,        // 0-500m
        MidiSky,         // 500-800m
        HighClouds,      // 800-1200m
        SpaceEdge,       // 1200-2000m
        DeepSpace        // 2000m+
    }

    [Header("Biom Names")]
    public GameObject lowerSky;
    public GameObject midiSky;
    public GameObject highClouds;
    public GameObject spaceEdge;
    public GameObject deepSpace;

    public SkyLevel currentLevel = SkyLevel.LowerSky;

    public static float height = 100f;
    public static float GeneralX = 0f;
    static float SpaceHeight = 50f;


    private void OnTriggerEnter2D(Collider2D other)
    {
        LowerSkyGen.nextY = 0f;
        MidiSkyGen.nextY = 510f;
        HighCloudsGen.nextY = 810f;
        SpaceEdgeGen.nextY = 1210f;
        DeepSpace.nextY = 2010f;

        if (!other.CompareTag("Player")) return;

        height += 100f;
        GeneralX += 50f;
        

        Vector3 nextFusePos = new Vector3(GeneralX, height+1.8f, -0.7f);
        GameObject newFuseBox = Instantiate(FuseboxPrefab, nextFusePos, Quaternion.identity);
        PolesGeneration newScripts = newFuseBox.GetComponent<PolesGeneration>();
       
        CameraTrigger oldTrigger = GetComponent<CameraTrigger>();
        CameraTrigger newTrigger = newFuseBox.AddComponent<CameraTrigger>();

        newFuseBox.tag = this.tag;
        newScripts.FuseboxPrefab = this.FuseboxPrefab;

        newTrigger.cameraScript = oldTrigger.cameraScript;
        newTrigger.newX = oldTrigger.newX + 50f; 
        newTrigger.bufferTime = oldTrigger.bufferTime;

        newScripts.PolePrefab = this.PolePrefab;
        newScripts.PoleTopPrefab = this.PoleTopPrefab;
        newScripts.GrassPrefab = this.GrassPrefab;
        newScripts.CabelPrefab = this.CabelPrefab;

        newScripts.lowerSky = this.lowerSky;
        newScripts.midiSky = this.midiSky;
        newScripts.highClouds = this.highClouds;
        newScripts.spaceEdge = this.spaceEdge;
        newScripts.deepSpace = this.deepSpace;

        newScripts.Player = this.Player;

        SpawnFullSky();

        Vector3 posGrass = new Vector3(GeneralX, 0f, -0.01f);
        GameObject Grass = Instantiate(GrassPrefab, posGrass, Quaternion.identity);

        Vector3 posPole = new Vector3(GeneralX, height/2, -0.5f);
        GameObject Pole = Instantiate(PolePrefab, posPole, Quaternion.identity);
        Pole.transform.localScale = new Vector3(1f, height/100, 1f);

        Vector3 posPoleTop = new Vector3(GeneralX, height-1, -0.6f);
        GameObject PoleTop = Instantiate(PoleTopPrefab, posPoleTop, Quaternion.identity);

        Vector3 posCabel = new Vector3(GeneralX + 12f - 50f, (height/2)-50f, 0f);
        GameObject Cabel = Instantiate(CabelPrefab, posCabel, Quaternion.identity);
        Cabel.transform.localScale = new Vector3(1, height/100-1, 1f);

        if (height >= 2000)
        {
            Vector3 posSpace = new Vector3(GeneralX, 2100+SpaceHeight, 0.1f);
            GameObject space = Instantiate(deepSpace, posSpace, Quaternion.identity);
            SpriteRenderer sr = space.GetComponent<SpriteRenderer>();
            sr.size = new Vector2(50f, height - 2000);
        }

    }

    void SpawnFullSky()
    {
        (GameObject prefab, float y)[] skyData = {
        (lowerSky, 250f),
        (midiSky, 650f),
        (highClouds, 1000f),
        (spaceEdge, 1600f),
        (deepSpace, 2025f)
    };

        foreach (var biom in skyData)
        {
            Instantiate(biom.prefab, new Vector3(GeneralX, biom.y, 10f), Quaternion.identity);
        }
    }

}

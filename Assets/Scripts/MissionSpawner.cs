using UnityEngine;

public class MissionSpawner : MonoBehaviour
{
    public GameObject[] missionPrefabs;
    public GameObject[] environmentPrefabs;

    public static MissionSpawner Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        // Here we instantiate the mission prefab and environment according the value in the PlayerPrefs.

        int missionNumber = PlayerPrefs.GetInt(Constants.MISSION_KEY);

        GameObject missionObject = Instantiate(missionPrefabs[missionNumber]) as GameObject;
        //GameObject environmentObject = Instantiate(environmentPrefabs[missionNumber]);

        Aircraft aircraft = FindObjectOfType<Aircraft>();

        if (missionPrefabs[missionNumber].GetComponent<Mission>().missionType == MissionType.Military)
        {
            aircraft.gameObject.AddComponent<DamagedEngine>();
            aircraft.gameObject.AddComponent<AircraftFuel>();
        }
    }
}
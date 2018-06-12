using UnityEngine;
using UnityEngine.UI;

public enum MissionType
{
    Checkpoints, Landing, Military
}

public class MissionController : MonoBehaviour
{
    public static MissionController Instance;

    public GameObject[] missionPrefabs;
    public GameObject[] aircraftPrefabs;

    [SerializeField] private Button[]  MissionButtons;
    [SerializeField] private GameObject aircraftImagesPanel;
    [SerializeField] private GameObject imageToInstantiate;
    [SerializeField] private GameObject shop;
    [SerializeField] private GameObject loading;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        int lastMission = PlayerPrefs.GetInt(Constants.LAST_MISSION_KEY, 0);

        for (int i = 0; i <= lastMission; i++)
        {
            MissionButtons[i].interactable = true;
        }
    }

    public void SaveMission(int missionNumber)
    {
        PlayerPrefs.SetInt(Constants.MISSION_KEY, missionNumber);
        SetMissionProps(missionNumber);
    }

    public void SetMissionProps(int missionIndex)
    {
        DestroyLastImages();

        MissionType missionType = missionPrefabs[missionIndex].GetComponent<Mission>().missionType;

        for (int i = 0; i < aircraftPrefabs.Length; i++)
        {
            Aircraft aircraft = aircraftPrefabs[i].GetComponent<Aircraft>();
            
            if (missionType == MissionType.Checkpoints)
            {
                SelectActiveAircrafts(aircraft, AircraftType.Normal, i);
            }

            else if (missionType == MissionType.Landing)
            {
                SelectActiveAircrafts(aircraft, AircraftType.Normal, i);
            }

            else if (missionType == MissionType.Military)
            {
                SelectActiveAircrafts(aircraft, AircraftType.Military, i);
            }
        }
    }

    private void SelectActiveAircrafts(Aircraft aircraft, AircraftType aircraftType, int index)
    {
        if (aircraft.aircraftData.aircraftType == aircraftType)
        {
            GameObject image = Instantiate(imageToInstantiate, aircraftImagesPanel.transform, false);
            image.GetComponent<Image>().sprite = aircraft.aircraftData.Photo;

            image.GetComponent<Button>().onClick.AddListener(() => new SceneController().StartScene(1));
            image.GetComponent<Button>().onClick.AddListener(() => AircraftController.Instance.ChangeActiveAircraft(index));
            image.GetComponent<Button>().onClick.AddListener(() => loading.SetActive(true));

            image.transform.Find("Price").GetComponent<Text>().text = aircraft.aircraftData.Price + " Coins";

            if (aircraft.aircraftData.Locked)
            {
                //TODO: make the button buy the item.
                image.GetComponent<Button>().interactable = false;
                Button buyBtn = image.transform.Find("BuyBtn").GetComponent<Button>();
                buyBtn.gameObject.SetActive(true);
                buyBtn.onClick.AddListener(() => shop.SetActive(true));
            }
        }
    }

    private void DestroyLastImages()
    {
        for (int i = 0; i < aircraftImagesPanel.transform.childCount; i++)
        {
            Destroy(aircraftImagesPanel.transform.GetChild(i).gameObject);
        }
    }
}
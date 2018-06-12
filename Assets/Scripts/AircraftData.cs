using UnityEngine;

[CreateAssetMenu(fileName = "Aircraft", menuName = "Aircraft")]
public class AircraftData : ScriptableObject
{
    public AircraftType aircraftType;

    public Sprite Photo;
    public int Price;
    public bool Locked;
}
using UnityEngine;

[System.Serializable]
public class DirectionInfo
{
    public string name;        // Optional label
    public Vector2 direction;  // Unit vector
    public float degrees;      // 0° to 360°
    public string piLabel;     // Like "π/4"

    public string ToRandomLabel()
    {
        int pick = Random.Range(0, 3);
        switch (pick)
        {
            case 0: return direction.ToString("F1");       // Coordinates
            case 1: return piLabel;                         // Pi value
            case 2: return degrees + "°";                   // Degree
            default: return name;
        }
    }
}

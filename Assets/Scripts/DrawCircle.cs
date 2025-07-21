using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DrawCircle : MonoBehaviour
{
    [Header("Circle Settings")]
    public float radius = 5f;
    public int segments = 100;

    [Header("Spoke Settings")]
    public bool drawSpokes = true;
    public float spokeWidth = 0.025f;
    public Material spokeMaterial;

    private void Start()
    {
        DrawUnitCircle();
        if (drawSpokes) DrawSpokes();
    }

    private void DrawUnitCircle()
    {
        LineRenderer circleLine = GetComponent<LineRenderer>();
        circleLine.positionCount = segments + 1;
        circleLine.useWorldSpace = false;
        circleLine.loop = true;
        circleLine.widthMultiplier = 0.05f;

        circleLine.material = new Material(Shader.Find("Sprites/Default"));
        circleLine.startColor = Color.gray;
        circleLine.endColor = Color.gray;

        float angleStep = 2 * Mathf.PI / segments;

        for (int i = 0; i <= segments; i++)
        {
            float angle = i * angleStep;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            circleLine.SetPosition(i, new Vector3(x, y, 0));
        }
    }

    private void DrawSpokes()
    {
        Vector2[] directions = new Vector2[]
        {
            new Vector2(1, 0),                         // Right
            new Vector2(Mathf.Sqrt(0.5f), Mathf.Sqrt(0.5f)),   // Top-right
            new Vector2(0, 1),                         // Up
            new Vector2(-Mathf.Sqrt(0.5f), Mathf.Sqrt(0.5f)),  // Top-left
            new Vector2(-1, 0),                        // Left
            new Vector2(-Mathf.Sqrt(0.5f), -Mathf.Sqrt(0.5f)), // Bottom-left
            new Vector2(0, -1),                        // Down
            new Vector2(Mathf.Sqrt(0.5f), -Mathf.Sqrt(0.5f))   // Bottom-right
        };

        for (int i = 0; i < directions.Length; i++)
        {
            GameObject spoke = new GameObject("Spoke_" + i);
            spoke.transform.parent = transform;

            LineRenderer lr = spoke.AddComponent<LineRenderer>();
            lr.positionCount = 2;
            lr.useWorldSpace = false;
            lr.widthMultiplier = spokeWidth;

            lr.material = spokeMaterial != null ? spokeMaterial : new Material(Shader.Find("Sprites/Default"));
            lr.startColor = Color.gray;
            lr.endColor = Color.gray;

            lr.SetPosition(0, Vector3.zero);
            lr.SetPosition(1, directions[i].normalized * radius);
        }
    }
}

using UnityEngine;

public class GridPreview : MonoBehaviour
{
    public int width = 10;
    public int height = 20;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        for (int x = 0; x <= width; x++)
            Gizmos.DrawLine(new Vector3(x - 0.5f, -0.5f), new Vector3(x - 0.5f, height - 0.5f));

        for (int y = 0; y <= height; y++)
            Gizmos.DrawLine(new Vector3(-0.5f, y - 0.5f), new Vector3(width - 0.5f, y - 0.5f));
    }
}

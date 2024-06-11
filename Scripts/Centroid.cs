using UnityEngine;

public class Centroid : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public Vector2 coords;

    public void Set_Centroid_Color(Color color)
    {
        spriteRenderer.color = color;
    }

    public Color Get_Centroid_Color()
    {
        return this.spriteRenderer.color;
    }

    public void Set_Coords(Vector2 coords)
    {
        this.coords = coords;
        this.gameObject.transform.position = coords;
    }
}

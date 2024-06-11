using System.Collections.Generic;
using UnityEngine;

public class Main_Colors : MonoBehaviour
{

    public List<Color> centroid_colors;

    public Color Get_Available_Color()
    {
        Color selectedColor = centroid_colors[0];
        centroid_colors.RemoveAt(0);
        return selectedColor;
    }
}

using TMPro;
using UnityEngine;

public class Object : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public TMP_Text name_txt;
    public Centroid centroid = null;
    public double dist_to_centroid = -1;

    void Awake()
    {
        Set_Name();

     
    }

    private void Set_Name() { name_txt.text = gameObject.name; }

    public void Set_Centroid(Centroid centroid)
    {
        this.centroid = centroid;

        spriteRenderer.color = this.centroid.Get_Centroid_Color();
    }

   
   

    
}

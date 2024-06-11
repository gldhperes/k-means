using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Best_Choice : MonoBehaviour
{
    private TMP_Text this_text;

    void Awake()
    {
        this.this_text = GetComponent<TMP_Text>();
    }

    public void Set_Text(string text)
    {
        this.this_text.text = text;
    }
    
    public  void Set_Text_Color(Color color)
    {
        this_text.color = color;
    }

}

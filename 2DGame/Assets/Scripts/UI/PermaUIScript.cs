using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PermaUIScript : MonoBehaviour
{
    public int gems = 0;
    public int hearts = 5;
    public TextMeshProUGUI gemAmount;
    public TextMeshProUGUI heartAmount;

    public static PermaUIScript perm;

    public void Start()
    {
        if(!perm)
        {
            perm = this;
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void Reset()
    {
        gems = 0;
        gemAmount.text = gemAmount.ToString();
        hearts = 4;
        heartAmount.text = heartAmount.ToString();
    }
}

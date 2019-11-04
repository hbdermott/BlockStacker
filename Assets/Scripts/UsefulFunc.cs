using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UsefulFunc
{
    public static Color GetColor()
    {
        Color color;
        int val = Random.Range(0, 7);
        switch (val)
        {
            case 0:
                color = new Color(1, 0, 0);
                break;
            case 1:
                color = new Color(0, 1, 0);
                break;
            case 2:
                color = new Color(0, 0, 1);
                break;
            case 3:
                color = new Color(0, 1, 1);
                break;
            case 4:
                color = new Color(1, 1, 0);
                break;
            case 5:
                color = new Color(1, 0, 1);
                break;
            default:
                color = new Color(1, 1, 1);
                break;
        }
        return color;
    }

}

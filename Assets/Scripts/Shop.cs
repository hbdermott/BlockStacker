using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    UserData user;
    TMPro.TextMeshProUGUI currency;
    
    void Start()
    {
        user = SaveSystem.LoadUser();
        if (user == null)
            user = new UserData();
        currency = GameObject.FindGameObjectWithTag("UI").transform.GetChild(1).GetChild(1).GetComponent<TMPro.TextMeshProUGUI>();
        UpdatePoints();
    }

    private void UpdatePoints()
    {
        currency.text = user.points.ToString();
    }

    public void PurchaseRainbow()
    {
        if(user.points > 10)
        {
            user.Update(-10, 1, 0);
            SaveSystem.SaveUser(user);
            UpdatePoints();
        }
    }
    public void PurchaseTimeSlow()
    {
        if (user.points > 10)
        {
            user.Update(-10, 0, 1);
            SaveSystem.SaveUser(user);
            UpdatePoints();
        }
    }

}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class UserData
{
    public int highScore = 0;
    public int numGames = 0;

    public UserData(int pointVal = 0, int numGames = 0)
    {
        if (pointVal > highScore)
            highScore = pointVal;
    }
    public UserData(UserData user)
    {
        highScore = user.highScore;
    }
    public void Update(int pointVal = 0, int games = 0)
    {
        if (pointVal > highScore)
            highScore = pointVal;
        numGames += games;
    }

}
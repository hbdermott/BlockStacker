using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class UserData
{
    public int points;
    public int numStick;
    public int numFreeze;
    public int highScore;

    public UserData(int pointVal = 0, int numStickVal = 0, int numFreezeVal = 0)
    {
        points = pointVal;
        numStick = numStickVal;
        numFreeze = numFreezeVal;
        if (points > highScore)
            highScore = points;
    }
    public UserData(UserData user)
    {
        points = user.points;
        numStick = user.numStick;
        numFreeze = user.numFreeze;
        highScore = user.highScore;
    }
    public void Update(int pointVal = 0, int numStickVal = 0, int numFreezeVal = 0)
    {
        points += pointVal;
        numStick += numStickVal;
        numFreeze += numFreezeVal;
        if (pointVal > highScore)
            highScore = pointVal;
    }

}

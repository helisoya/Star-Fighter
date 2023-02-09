using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SAVEFILE
{
    public List<string> completedMissions = new List<string>();

    public int credits = 0;

    public List<int> itemsOwned = new List<int>();
}

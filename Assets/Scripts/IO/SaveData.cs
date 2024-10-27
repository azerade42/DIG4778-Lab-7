using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class SaveData
{
    public int Score;
    public float PlayerPositionX;
    public List<Vector3> Targets = new List<Vector3>();
}

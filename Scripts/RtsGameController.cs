using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RtsGameController : MonoBehaviour
{
    public List<GameObject> units;

    public void NewUnit(GameObject unit)
    {
        units.Add(unit);
    }
}

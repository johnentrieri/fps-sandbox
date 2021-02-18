using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headshot : MonoBehaviour
{
    [SerializeField] int headShotMultiplier = 5;

    public int GetHeadshotMultiplier() {
        return headShotMultiplier;
    }
}

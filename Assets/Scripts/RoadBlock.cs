using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBlock : MonoBehaviour
{
    private void Deactivate()
    {
        this.gameObject.SetActive(false);
    }
}

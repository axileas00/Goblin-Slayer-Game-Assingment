using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentDeactivator : MonoBehaviour
{
    private void Awake()
    {
         GetComponent<EnemySpawn>().enable = false;
    }
}

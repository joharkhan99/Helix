using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarDestroyer : MonoBehaviour
{
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

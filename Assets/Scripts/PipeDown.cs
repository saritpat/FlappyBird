using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeDown : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeUp : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

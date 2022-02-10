using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onDestroy : MonoBehaviour
{
    private void OnDestroy() {Destroy(transform.parent.gameObject);}
}

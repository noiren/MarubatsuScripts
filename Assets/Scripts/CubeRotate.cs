﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotate : MonoBehaviour
{

    void Update()
    {
        transform.Rotate(0.1f, 0.2f, 0.3f);
    }
}

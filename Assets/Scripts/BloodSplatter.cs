﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatter : MonoBehaviour
{
    public void DestroySelf()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}

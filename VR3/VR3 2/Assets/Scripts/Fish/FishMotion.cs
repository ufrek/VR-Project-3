﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMotion : MonoBehaviour
{
 
        private Material fishMaterial;
        void Start()
        {
            fishMaterial = this.GetComponentInChildren<Material>() ;
            fishMaterial.SetFloat("_MoveOffset", Random.Range(0.0f, 3.14f));
        }
}

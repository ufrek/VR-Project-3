//-----------------------------------------------------------------------
// <copyright file="CameraPointer.cs" company="Google LLC">
// Copyright 2020 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections;
using UnityEngine;

/// <summary>
/// Sends messages to gazed GameObject.
/// </summary>
public class CameraPointer : MonoBehaviour
{
    [SerializeField]
    private const float _maxDistance = 400;
    private GameObject _gazedAtObject = null;

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    public void Update()
    {
        // Casts ray towards camera's forward direction, to detect if a GameObject is being gazed
        // at.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance))
        {
            // GameObject detected in front of the camera.
      
            if (_gazedAtObject != hit.transform.gameObject)
            {
                // New GameObject.
                if(_gazedAtObject != null &&  _gazedAtObject.tag.Equals("Obj"))
                    _gazedAtObject?.SendMessage("OnPointerExit");
                
                _gazedAtObject = hit.transform.gameObject;
                
                if (_gazedAtObject != null && _gazedAtObject.tag.Equals("Obj"))
                    _gazedAtObject.SendMessage("OnPointerEnter");
            }
              
            
        }
        else
        {
            // No GameObject detected in front of the camera.
            if (_gazedAtObject != null && _gazedAtObject.tag.Equals("Obj"))
                _gazedAtObject?.SendMessage("OnPointerExit");
            
            _gazedAtObject = null;
        }

        // Checks for screen touches.
        if (Google.XR.Cardboard.Api.IsTriggerPressed || Input.GetMouseButtonDown(0))
        {
            if (_gazedAtObject != null && _gazedAtObject.tag.Equals("Obj"))
                _gazedAtObject?.SendMessage("OnPointerClick");
        }
    }
}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceObject : MonoBehaviour
{
    public GameObject obj;

    private GameObject spawnedObj;
    private ARRaycastManager _arRaycastManager;
    private Vector2 touchPosition;

    public Slider scaleSlider, rotationSlider;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
    }

    bool GetTouchPos(out Vector2 touchPos)
    {
        if (Input.touchCount > 0)
        {
            touchPos = Input.GetTouch(0).position;
            return true;
        }

        touchPos = default;
        return false;
    }

    void Update()
    {
        if (!GetTouchPos(out Vector2 touchPos))
            return;

        if (_arRaycastManager.Raycast(touchPos, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            if (spawnedObj == null)
            {
                spawnedObj = Instantiate(obj, hitPose.position, hitPose.rotation);
            }
            else
            {
                spawnedObj.transform.position = hitPose.position;
            }
        }

        spawnedObj.transform.rotation = Quaternion.Euler(0f, rotationSlider.value, 0f);
        spawnedObj.transform.localScale = new Vector3(scaleSlider.value,
            scaleSlider.value,
            scaleSlider.value);
    }
}

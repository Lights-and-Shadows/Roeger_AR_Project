using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceObject : MonoBehaviour
{
    public List<GameObject> mathObjects = new List<GameObject>();
    private Vector2 touchPosition;

    public Slider scaleSlider, rotationSlider;

    public TextMeshProUGUI currentObjText;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public Button nextButton, prevButton;

    public GameObject obj;

    private GameObject spawnedObj;
    private ARRaycastManager _arRaycastManager;

    private int selectedIndex;

    void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
        selectedIndex = 0;

        nextButton.onClick.AddListener(() => NextPrefab());
        prevButton.onClick.AddListener(() => PrevPrefab());

        obj = mathObjects[selectedIndex];
    }

    void NextPrefab()
    {
        // Remove the previous object from the scene and replace currently selected one with the next
        if (spawnedObj != null)
        {
            Destroy(spawnedObj);
            spawnedObj = null;
        }

        if (selectedIndex == mathObjects.Count - 1)
            selectedIndex = 0;
        else
            selectedIndex++;

        obj = mathObjects[selectedIndex];
    }

    void PrevPrefab()
    {
        if (spawnedObj != null)
        {
            Destroy(spawnedObj);
            spawnedObj = null;
        }

        if (selectedIndex == 0)
            selectedIndex = mathObjects.Count - 1;
        else
            selectedIndex--;

        obj = mathObjects[selectedIndex];
    }

    private bool IsPointerOverUI()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
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

        currentObjText.text = "Current Object: " + mathObjects[selectedIndex].name;

        if (!IsPointerOverUI())
        {
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
        }

        spawnedObj.transform.rotation = Quaternion.Euler(0f, rotationSlider.value, 0f);
        spawnedObj.transform.localScale = new Vector3(scaleSlider.value,
            scaleSlider.value,
            scaleSlider.value);
    }
}

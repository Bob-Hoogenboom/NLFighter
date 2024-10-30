using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private List<GameObject> cameraTrackables = new List<GameObject>();

    [SerializeField] private float pixelMargin = 50f;
    [SerializeField] private float minZoom = 3f;
    [SerializeField] private float maxZoom = 10f;
    [SerializeField] private float zoomSpeed = 1f;
    private float currentZoom = 5f;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        _camera.transform.localPosition = new Vector3(0f, 0f, currentZoom);
        _camera.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
        this.transform.rotation = Quaternion.Euler(-45f, 45f, 0f);
    }

    void Update()
    {
        if (cameraTrackables.Count == 0) return;

        Vector3 trackablesCenter = Vector3.zero;
        for (int i = 0; i < cameraTrackables.Count; i++)
        {
            trackablesCenter += cameraTrackables[i].transform.position;
        }
        trackablesCenter /= cameraTrackables.Count;
        transform.position = trackablesCenter;

        SetCorrectZoom();
    }


    private void SetCorrectZoom()
    {
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);

        float maxDistance = 0f;
        for (int i = 0; i < cameraTrackables.Count; i++)
        {
            float distance = Vector3.Distance(cameraTrackables[i].transform.position, this.transform.position);
            maxDistance = Mathf.Max(maxDistance, distance);
        }

        Vector2 characterScreenPoint = _camera.WorldToScreenPoint(cameraTrackables[0].transform.position);

        //increase zoom if characters are going out of screen
        if (characterScreenPoint.x < pixelMargin || characterScreenPoint.x > screenSize.x - pixelMargin || characterScreenPoint.y < pixelMargin || characterScreenPoint.y > screenSize.y - pixelMargin)
        {
            currentZoom += zoomSpeed * Time.deltaTime;
        }

        //decrease zoom if characters are getting closer to eachother
        if (characterScreenPoint.x > pixelMargin*2 && characterScreenPoint.x < screenSize.x - pixelMargin*2 && characterScreenPoint.y > pixelMargin*2 && characterScreenPoint.y < screenSize.y - pixelMargin*2)
        {
            currentZoom -= zoomSpeed * Time.deltaTime;
        }

        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        _camera.transform.localPosition = new Vector3(0f, 0f, currentZoom);
    }
}

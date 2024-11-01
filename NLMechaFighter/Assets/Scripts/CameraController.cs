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
    private float _currentZoom = 3f;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        _camera.transform.localPosition = new Vector3(0f, 0f, _currentZoom);
        Vector3 lookDirection = transform.position - _camera.transform.position;
        _camera.transform.rotation = Quaternion.LookRotation(lookDirection);
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

        foreach (var trackable in cameraTrackables)
        {
            Vector2 trackablePoint = _camera.WorldToScreenPoint(trackable.transform.position);

            //increase zoom if characters are going out of screen
            if (trackablePoint.x < pixelMargin || trackablePoint.x > screenSize.x - pixelMargin || trackablePoint.y < pixelMargin || trackablePoint.y > screenSize.y - pixelMargin)
            {
                _currentZoom += zoomSpeed * Time.deltaTime;
            }

            //decrease zoom if characters are getting closer to eachother
            if (trackablePoint.x > pixelMargin * 2 && trackablePoint.x < screenSize.x - pixelMargin * 2 && trackablePoint.y > pixelMargin * 2 && trackablePoint.y < screenSize.y - pixelMargin * 2)
            {
                _currentZoom -= zoomSpeed * Time.deltaTime;
            }
        }

        _currentZoom = Mathf.Clamp(_currentZoom, minZoom, maxZoom);
        _camera.transform.localPosition = new Vector3(0f, 0f, _currentZoom);
    }
}

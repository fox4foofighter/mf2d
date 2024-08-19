using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool IsFollowing = false;
    public float TraceSpeed = 0;

    // default INF
    public float xMin = Mathf.NegativeInfinity;
    public float xMax = Mathf.Infinity;
    public float yMin = Mathf.NegativeInfinity;
    public float yMax = Mathf.Infinity;
    public float zMin = Mathf.NegativeInfinity;
    public float zMax = Mathf.Infinity;
    public Transform Target = null;
    public bool IsMouseFollow = false;

    void Update()
    {
        if (!IsFollowing)
        {
            return;
        }
        Vector3 position = IsMouseFollow ? GetMousePosition() : GetTargetPosition();
        Vector3 fixedPosition = Vector2.Lerp(transform.position, position, TraceSpeed * Time.deltaTime);
        fixedPosition.x = Mathf.Clamp(fixedPosition.x, xMin, xMax);
        fixedPosition.y = Mathf.Clamp(fixedPosition.y, yMin, yMax);
        fixedPosition.z = Mathf.Clamp(fixedPosition.z, zMin, zMax);

        transform.position = fixedPosition;
    }

    private Vector3 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private Vector3 GetTargetPosition()
    {
        return Target.transform.position;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuParallex : MonoBehaviour
{
    public float multiplier = 1f;

    public float smoothedTime = 0.3f;

    private Vector2 beginPosition;

    private Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        beginPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        transform.position = Vector3.SmoothDamp(transform.position, beginPosition + (offset * multiplier), ref velocity,
            smoothedTime);
    }
}

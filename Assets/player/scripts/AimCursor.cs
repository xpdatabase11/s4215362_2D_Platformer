using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AimCursor : MonoBehaviour
{
    public Vector2 cursor_pos;
    public GameObject reticle;

    // Start is called before the first frame update
    void Start()
    {
        reticle = GetComponent<GameObject>();
        cursor_pos = new Vector2();
    }

    // Update is called once per frame
    void Update()
    {
        cursor_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursor_pos;
        transform.position = transform.position + new Vector3(0, 0, -2.0f);
    }
}

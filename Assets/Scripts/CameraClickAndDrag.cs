using UnityEngine;
using System.Collections;

public class CameraClickAndDrag : MonoBehaviour
{
    [SerializeField]
    private float _dragSpeed;

    private bool _isTouchEnabled;
    private bool _isMouseEnabled;

    private Vector2 _dragOrigin;

    private bool _hasInputStarted;


    void Start()
    {
        _isMouseEnabled = false;
        _isTouchEnabled = false;

        if (Input.touchSupported)
        {
            _isTouchEnabled = true;
        }

        if (Input.mousePresent)
        {
            _isMouseEnabled = true;
        }
    }

    void Update()
    {
        UpdateClickAndDrag();
    }

    void UpdateClickAndDrag()
    {
        if (_isMouseEnabled)
        {
            HandleMouseInput();
        }

        if (_isTouchEnabled)
        {
            HandleTouchInput();
        }

    }

    void HandleMouseInput()
    {
        if (!Input.GetMouseButton(0))
        {
            _hasInputStarted = false;
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            _dragOrigin = Input.mousePosition;
            _hasInputStarted = true;
        }

        if (_hasInputStarted)
        {
            HandleGeneralInput(Input.mousePosition);
        }
    }

    void HandleTouchInput()
    {
        if (Input.touchCount == 0 || Input.touchCount > 1)
        {
            _hasInputStarted = false;
            return;
        }

        if (!_hasInputStarted)
        {
            _dragOrigin = Input.GetTouch(0).position;
            _hasInputStarted = true;
        }

        if (_hasInputStarted)
        {
            HandleGeneralInput(Input.GetTouch(0).position);
        }

    }

    void HandleGeneralInput(Vector2 currentPos)
    {
        Vector3 pos = Camera.main.ScreenToViewportPoint(currentPos - _dragOrigin);
        Vector3 translation = new Vector3(pos.x * _dragSpeed, pos.y * _dragSpeed, 0f);

        transform.Translate(translation, Space.World);
    }

}

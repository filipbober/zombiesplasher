using UnityEngine;
using System.Collections;

public class CameraClickAndDrag : MonoBehaviour
{
    public enum CameraMode { Mode2D, Mode3D, Count };

    [SerializeField]
    CameraMode Mode = CameraMode.Mode2D;

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
        //// -----------------------
        //Vector3 dir = Camera.main.ScreenToViewportPoint(currentPos - _dragOrigin);
        //Vector3 translation = new Vector3(dir.x * _dragSpeed, dir.y * _dragSpeed, 0f);

        //transform.Translate(translation, Space.World);
        //// -----------------------

        //// if camera currentPos - mouseCurrentPos < proximity -> return;
        //Vector2 currentCameraPos = transform.position;
        //Vector2 currentMousePos = Camera.main.ScreenToViewportPoint(currentPos);
        //if (Vector2.Distance(currentCameraPos, currentMousePos) < 0.1) return;
        //Debug.Log(Vector2.Distance(currentCameraPos, currentMousePos));


        // Mose - move from A (origin) to B (currentPos) = translation
        // Camera - move from A (transform.position) to B (transform.position + (-translation)







        //Vector2 mouseTransition = currentPos - _dragOrigin;
        //Vector2 cameraTransition = new Vector2(transform.position.x + mouseTransition.x, transform.position.y + mouseTransition.y);

        //Debug.Log(mouseTransition);

        //transform.Translate(cameraTransition, Space.World);

        Vector3 dir;
        Vector3 translation;
        if (Mode == CameraMode.Mode2D)
        {
            dir = Camera.main.ScreenToViewportPoint(currentPos - _dragOrigin);
            translation = new Vector3(dir.x * _dragSpeed, dir.y * _dragSpeed, 0f);
            _dragOrigin = currentPos;
        }
        else
        {
            dir = Camera.main.ScreenToViewportPoint(currentPos - _dragOrigin);
            translation = new Vector3(dir.x * _dragSpeed, 0f, dir.y * _dragSpeed);          
            _dragOrigin = currentPos;
        }

        //transform.Translate(translation, Space.World);
        transform.Translate(translation, Space.Self);

    }

}

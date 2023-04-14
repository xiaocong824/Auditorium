using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRay : MonoBehaviour
{
    [SerializeField]
    Texture2D _moveCursorTexture;
    [SerializeField]
    Texture2D _resizeCursorTexture;
    [SerializeField]
    private LayerMask _interactableLayerMask;

    private Camera _camera;

    private const string EFFECTOR_CENTER_TAG_NAME = "EffectorCenter";
    private const string EFFECTOR_EDGE_TAG_NAME = "EffectorEdge";

    private Transform _currentColliderTransform;

    private bool _isMoving;
    private bool _isResizing;

    

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        
    }
   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D collider = CastRay();
            //START interaction
            if(collider != null)
            {
                _currentColliderTransform = collider.transform;
                if (_currentColliderTransform.CompareTag(EFFECTOR_CENTER_TAG_NAME))
                {
                   _isMoving = true;
                }
                else if(_currentColliderTransform.CompareTag(EFFECTOR_EDGE_TAG_NAME))
                {
                    _isResizing = true;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            //STOP interaction
            _isMoving = false;
            _isResizing = false;
            _currentColliderTransform = null;
        }
        if(_isMoving)
        {
            DoMove();
        }
        if (_isResizing)
        {
            DoResize();
        }
    }

    private void FixedUpdate()
    {
        CastRay();
    }

    private Collider2D CastRay()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, _interactableLayerMask);

        if(hit.collider != null)
        {
           // _currentColliderTransform = hit.collider.transform;

            if(hit.collider.CompareTag(EFFECTOR_CENTER_TAG_NAME))
            {
                //Debug.Log("EffectorCenter");
                Cursor.SetCursor(_moveCursorTexture, new Vector2(_moveCursorTexture.width / 2, _moveCursorTexture.height / 2), CursorMode.ForceSoftware);
               
            }
            else if(hit.collider.CompareTag(EFFECTOR_EDGE_TAG_NAME))
                //Debug.Log("EffectorEdge");
                Cursor.SetCursor(_resizeCursorTexture, new Vector2(_resizeCursorTexture.width / 2, _resizeCursorTexture.height / 2), CursorMode.ForceSoftware);
            else
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            }

            
        }
        return hit.collider;
        //else
        // {

        //if(!_isMoving && !_isResizing)
        //_currentColliderTransform = null;
        // Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

        //}
    }
    private void DoMove()
    {
        //Vector3 currentPosition = _currentColliderTransform.position;
        // _camera.ScreenToWorldPoint(Input.mousePosition); //将鼠标在屏幕上的坐标(Input.mousePosition)转换为相对于相机的世界坐标位置
        Vector3 screenToWorldPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
        screenToWorldPoint.z = 0;
        _currentColliderTransform.parent.position = screenToWorldPoint;
    }
    
    private void DoResize()
    {
        // Debug.Log("resize");
        Vector2 transformPos = _currentColliderTransform.position;
        Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);

        CircleShape circleShape = _currentColliderTransform.GetComponent<CircleShape>();
        if(circleShape != null)
        {
            circleShape.Radius = Mathf.Clamp(Vector2.Distance(transformPos, mousePos), 0.6f, 1.5f);
        }
        AreaEffector2D areaEffector = _currentColliderTransform.GetComponent<AreaEffector2D>();
        if (areaEffector != null)
        {
            float radiusNormalized = (circleShape.Radius - 0.6f) / (1.5f - 0.6f); // normalize the radius value between 0 and 1
            float forceMagnitude = Mathf.Lerp(0.06f, 0.15f, radiusNormalized); // use Mathf.Lerp to interpolate between the min and max force magnitude based on the normalized radius value
            areaEffector.forceMagnitude = forceMagnitude;
        }

    }
}

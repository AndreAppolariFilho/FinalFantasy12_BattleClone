using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float minDistance = 10;
    [SerializeField] private float maxDistance = 20;
    [SerializeField] private GameObject target;
    public float updateSpeed = 20;
    public float moveSpeed = 10;
    public float currentDistance = 5;
    private string moveYAxis = "Right Y Axis";
    private string moveXAxis = "Right X Axis";
    private GameObject ahead;
    private MeshRenderer _renderer;
    public float hidePosition = 1.5f;
    public Vector3 initialPosition;
    
    void Start()
    {
        ahead = new GameObject("ahead");
        _renderer = target.gameObject.GetComponent<MeshRenderer>();
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
        
    {
        
        
        ahead.transform.position = target.transform.position + target.transform.forward * (maxDistance * 0.25f) + new Vector3(0, target.transform.lossyScale.y * 0.75f, 0);
        float yDirection = Input.GetAxisRaw(moveYAxis);
        float xDirection = Input.GetAxisRaw(moveXAxis);
        if (yDirection != 0)
        { 
            
            currentDistance += yDirection * moveSpeed * Time.deltaTime;
            currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);
            transform.position = Vector3.MoveTowards(
                transform.position,
                target.transform.position + Vector3.up * currentDistance - target.transform.forward * (currentDistance + maxDistance * 0.5f),
                updateSpeed*Time.deltaTime
           );
            
        }
        else
        {
            //transform.Translate(Vector3.right * Time.deltaTime * xDirection * moveSpeed);
        }
        transform.LookAt(target.transform.position + new Vector3(0, target.transform.lossyScale.y * 0.75f, 0));

    }
}

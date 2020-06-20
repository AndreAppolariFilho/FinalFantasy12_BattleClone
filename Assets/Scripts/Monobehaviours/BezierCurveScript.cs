using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurveScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject startPoint;
    [SerializeField] private GameObject endPoint;
    private LineRenderer lineRenderer;
    public int currentPosition = 1;
    private int maxPointsPerFrame = 1;
    [SerializeField] private int maxPoints = 10;
    private float currentTime = .0f;
    private float maxTime = 20;
    private float dist = 0;
    private int currentIndex = 1;
    private List<Vector3> points = new List<Vector3>();
    public Color c1 = Color.yellow;
    public Color c2 = Color.red;


    // Update is called once per frame
    public void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        
        if (startPoint)
        {
            lineRenderer.SetPosition(0, new Vector3(startPoint.transform.position.x, startPoint.transform.position.y + startPoint.GetComponent<BoxCollider>().bounds.size.y, startPoint.transform.position.z));
            lineRenderer.positionCount = 1;
            dist = Vector3.Distance(startPoint.transform.position, endPoint.transform.position);
            
        }
    }
    public void OnEnable()
    {
        lineRenderer = GetComponent<LineRenderer>();

        if (startPoint)
        {
            lineRenderer.SetPosition(0, new Vector3(startPoint.transform.position.x, startPoint.transform.position.y + startPoint.GetComponent<BoxCollider>().bounds.size.y, startPoint.transform.position.z));
            lineRenderer.positionCount = 1;
            dist = Vector3.Distance(startPoint.transform.position, endPoint.transform.position);

        }
    }
    public void Reset()
    {
        if (startPoint && lineRenderer)
        {
            lineRenderer.SetPosition(0, new Vector3(startPoint.transform.position.x, startPoint.transform.position.y + startPoint.GetComponent<BoxCollider>().bounds.size.y, startPoint.transform.position.z));
             
            currentPosition = 1;
            currentIndex = 1;
            lineRenderer.positionCount = maxPoints;
            dist = Vector3.Distance(startPoint.transform.position, endPoint.transform.position);

        }
    }
    public void Update()
    {   
        if(endPoint)
        {   
            
           DrawCurve(startPoint, endPoint);
           currentTime = maxTime;
        }
    }
    public void SetTarget(GameObject startPoint, GameObject endPoint)
    {
        this.startPoint = startPoint;
        currentIndex = 1;
        
        lineRenderer.SetPosition(0, new Vector3(startPoint.transform.position.x, startPoint.transform.position.y + startPoint.GetComponent<BoxCollider>().bounds.size.y, startPoint.transform.position.z));
        
        
        this.endPoint = endPoint;
        lineRenderer.positionCount = currentIndex + 1;
        
    }
    private double TruncateDecimal(double value, int precision)
    {
        double step = Math.Pow(10, precision);
        double tmp = Math.Truncate(step * value);
        return tmp / step;
    }
    public void DrawCurve(GameObject startPoint, GameObject endPoint)
    {
        Vector3 p0 = new Vector3(startPoint.transform.position.x, startPoint.transform.position.y + startPoint.GetComponent<BoxCollider>().bounds.size.y, startPoint.transform.position.z);
        Vector3 p1 = new Vector3(endPoint.transform.position.x, endPoint.GetComponent<BoxCollider>().size.y, endPoint.transform.position.z);
        
        if (Vector3.Distance(lineRenderer.GetPosition(0), p0) > 0.01)
        {
            lineRenderer.SetPosition(0,p0);
            int index = 1;
            for (int i = 1; i < currentPosition; i++)
            {
                float t_aux = i / maxPoints;
                t_aux = i / 100.0f;
                lineRenderer.SetPosition(index, CalculateBezierCurve(t_aux, p0, p1));
                float ratio_ = (float)TruncateDecimal(1.0 / maxPoints, 2);
                if (((decimal)(t_aux) % (decimal)ratio_) == 0)
                {
                    index = (int)((decimal)(t_aux) / (decimal)ratio_);
                    
                }
            }
        }

        float t = currentPosition / maxPoints;
        
        t = currentPosition / 100.0f;
        
        lineRenderer.positionCount = currentIndex + 1;
        Vector3 newPosition = CalculateBezierCurve(t, p0, p1);
        
        lineRenderer.SetPosition(currentIndex,  newPosition);
        
        if (currentIndex < maxPoints)
        { 
            currentPosition++;
            
            float ratio = (float) TruncateDecimal(1.0 / maxPoints, 2);
            
            if (((decimal)(t) % (decimal)ratio) == 0)
            {   
                currentIndex = (int)((decimal)(t) / (decimal)ratio);
            }
            //Debug.Log(t);

        }

    }
    public Vector3 CalculateBezierCurve(float t, Vector3 startPoint, Vector3 endPoint)
    {
        
        Vector3 middlePoint = startPoint + ((endPoint - startPoint).normalized * 0.5f);
        middlePoint.y += Mathf.Clamp(10 * 1/Vector3.Distance(endPoint, startPoint),4,10);
        return Mathf.Pow(1 - t, 2) * startPoint + 2 *t* (1 - t) * middlePoint + Mathf.Pow(t, 2) * endPoint;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteLine : MonoBehaviour
{
    // Start is called before the first frame update
    private LineRenderer lineRenderer;
    private float counter;
    private float dist;
    private int maxPoints;
    
    public Transform origin;
    public Transform destination;
    public int currentPoint = 1;
    public float lineSpeed = 10f;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, origin.position);
        lineRenderer.SetWidth(.45f, .45f);
        dist = Vector3.Distance(origin.position, destination.position);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 firstPosition = lineRenderer.GetPosition(0);
        if(firstPosition != origin.position)
        {
            lineRenderer.SetPosition(0, origin.position);
            counter = 0;
            dist = Vector3.Distance(origin.position, destination.position);
            for (int i = 1; i < currentPoint;i++)
            {
                counter += .1f / lineSpeed;
                float x_ = Mathf.Lerp(0, dist, counter);
                Vector3 newPosition = x_ * Vector3.Normalize(destination.position - origin.position) + origin.position;
                if(newPosition != destination.position)
                { 
                    lineRenderer.SetPosition(i, newPosition);
                }
                else
                {
                    lineRenderer.positionCount = i + 1;
                }
            }
        }
        if(counter < dist)
        {
            counter += .1f / lineSpeed;

            Debug.Log(counter + " "+ dist);

            float x = Mathf.Lerp(0, dist, counter);

            Vector3 pointAtLongLine = x * Vector3.Normalize(destination.position - origin.position) + origin.position;
            
            if(lineRenderer.GetPosition(currentPoint) != destination.position)
            { 
                currentPoint += 1;

                lineRenderer.positionCount = currentPoint;

                lineRenderer.SetPosition(currentPoint - 1, pointAtLongLine);
            }
        }
    }
}

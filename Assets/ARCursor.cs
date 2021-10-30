using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARCursor : MonoBehaviour
{
    public GameObject cursorChildObject;
    public GameObject objectToPlace;
    public ARRaycastManager rayCastManager;

    public bool userCursor = true;

    // Start is called before the first frame update
    void Start()
    {
        cursorChildObject.SetActive(userCursor);
    }

    // Update is called once per frame
    void Update()
    {
        if (userCursor) { UpdateCursor(); }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (userCursor)
            {
                GameObject.Instantiate(objectToPlace, transform.position, transform.rotation);
            }
            else { 
		        List<ARRaycastHit> hits = new List<ARRaycastHit>();
		        rayCastManager.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
		        if (hits.Count > 0)
		        {
			        GameObject.Instantiate(objectToPlace, hits[0].pose.position, hits[0].pose.rotation);
		        }
	    
	        }
        }
    }

    void UpdateCursor() {
            Debug.Log("UPDATE CURSOR");
        Debug.Log(Camera.current);
        Debug.Log(Camera.main);
        Vector2 screenPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
            Debug.Log("UPDATED SCREEN POSITION " + screenPosition );

        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        rayCastManager.Raycast(screenPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
            Debug.Log("UPDATED LIST");

        if (hits.Count > 0) {
            Debug.Log("HIT");
            Debug.Log(hits);
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
	    }
    }
}

using EventDefinitions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PersistentGameManager.Instance.EventHub.AddListener<OnScreenClickedEvent, Vector2>(UpdateLocation);
        //Debug.Log("Listener start");
        
        MapTargetSetEventArgs args = new MapTargetSetEventArgs();
        args.param0 = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y);

        PersistentGameManager.Instance.EventHub.RaiseEvent(args);
        
    }

    // Update is called once per frame
    public void UpdateLocation(Vector2 newPosition)
    {
        //Debug.Log("New Position: " + newPosition);
        gameObject.transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);

        MapTargetSetEventArgs args = new MapTargetSetEventArgs();
        args.param0 = newPosition;

        PersistentGameManager.Instance.EventHub.RaiseEvent(args);
    }
}

using EventDefinitions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharacter : MonoBehaviour
{
    Animator animator;

    [SerializeField]
    bool shouldFollow;
    [SerializeField]
    Vector3 targetLoc;

    [SerializeField]
    Vector3 previousLoc;

    List<Vector2Int> path;

    // Start is called before the first frame update
    void Start()
    {
        PersistentGameManager.Instance.EventHub.AddListener<OnCharacterClickedEvent, string>(HandleCharacterClicked);
        PersistentGameManager.Instance.EventHub.AddListener<PollCharacterPositionEvent, string>(BroadcastLocation);
        //PersistentGameManager.Instance.EventHub.AddListener<Vector2>("MapTargetSet", TrackNewLocation);
        animator = GetComponent<Animator>();
        shouldFollow = false;
    }

    public void HandleCharacterClicked(string name)
    {
        if(name == this.name)
        {
            Debug.Log("Clicked on " + name);
            shouldFollow = !shouldFollow;

            OnCharacterSelectedEventArgs args = new OnCharacterSelectedEventArgs();
            if (shouldFollow)
            {
                args.param0 = name;
            }
            else
            {
                args.param0 = "";
            }
            PersistentGameManager.Instance.EventHub.RaiseEvent(args);
            animator.SetBool("IsActive", shouldFollow);
        }
    }
    public void BroadcastLocation(string name)
    {
        if(name == this.name)
        {
            BroadcastCharacterPositionEventArgs args = new BroadcastCharacterPositionEventArgs();
            args.param0 = new Vector2Int((int)previousLoc.x, (int)previousLoc.y) ;
        }
    }

    public void TrackNewLocation(Vector2 newLoc)
    {
        previousLoc = targetLoc;
        targetLoc = new Vector3(newLoc.x, newLoc.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldFollow)
        {
            transform.position = Vector3.Lerp(transform.position, targetLoc, 2*Time.deltaTime);
        }
    }
}

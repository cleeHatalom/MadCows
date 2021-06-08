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
        PersistentGameManager.Instance.EventHub.AddListener<MapTargetSetEvent, Vector2>(TrackNewLocation);
        PersistentGameManager.Instance.EventHub.AddListener<SpawnCharacterEvent, Vector3>(SpawnCharacter);
        animator = GetComponent<Animator>();
        shouldFollow = false;
    }
    private void SpawnCharacter(Vector3 startPos)
    {
        transform.position = targetLoc = previousLoc = startPos;
        Debug.Log("Go here: " + transform.position);

    }
    public void HandleCharacterClicked(string name)
    {
        if(name == this.name)
        {
            Debug.Log("Clicked on " + name);
            shouldFollow = !shouldFollow;
            /*
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
            */
            animator.SetBool("IsActive", shouldFollow);
        }
    }
    public void TrackNewLocation(Vector2 newLoc)
    {
        previousLoc = targetLoc;
        targetLoc = new Vector3(newLoc.x, newLoc.y, 0);

        if(shouldFollow)
        {
            StartCoroutine(WalkPath());
        }
    }

    public IEnumerator WalkPath()
    {
        //var graph = PersistentGameManager.Instance.LevelManager.GetNavData();
        while ((targetLoc-transform.position).sqrMagnitude > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, targetLoc, 2*Time.fixedDeltaTime); // Move objectToMove closer to b
            yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame
        }
    }
}

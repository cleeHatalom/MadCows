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
    // Start is called before the first frame update
    void Start()
    {
        PersistentGameManager.Instance.EventHub.AddListener<string>("OnCharacterSelected", HandleCharacterSelection);
        PersistentGameManager.Instance.EventHub.AddListener<Vector2>("MapTargetSet", TrackNewLocation);
        animator = GetComponent<Animator>();
        shouldFollow = false;
    }

    public void HandleCharacterSelection(string name)
    {
        Debug.Log("Clicked on " + name);
        if(name == this.name)
        {
            animator.SetTrigger("ClickedOn");
            shouldFollow = !shouldFollow;
        }

    }

    public void TrackNewLocation(Vector2 newLoc)
    {
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

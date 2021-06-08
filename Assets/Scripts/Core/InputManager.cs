/* 
 * Copyright 2021 (C) Hatalom Corporation - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventDefinitions;
using UnityEngine.Tilemaps;

public class InputManager : MonoBehaviour
{
    private Vector3 position;
    private float width;
    private float height;
    private float ratio;

    void Awake()
    {
        width = (float)Screen.width / (2);
        height = (float)Screen.height / (2);
        ratio = (float)Screen.width / (float)Screen.height;

        Debug.Log("Width: " + Screen.width + "Height: " + Screen.height + " Aspect Ratio: " + ratio);

        // Position used for the cube.
        position = new Vector3(0.0f, 0.0f, 0.0f);
    }
    void Update()
    {

        /*
    // Handle screen touches.
    if (Input.touchCount > 0)
    {
        Touch touch = Input.GetTouch(0);

        // Move the cube if the screen has the finger moving.
        if (touch.phase == TouchPhase.Moved)
        {
            Vector2 pos = touch.position;
            pos.x = (pos.x - width) / width;
            pos.y = (pos.y - height) / height;
            position = new Vector3(-pos.x, pos.y, 0.0f);

            // Position the cube.
            //transform.position = position;
            Debug.Log("Touch");
        }
        if (Input.touchCount == 2)
        {
            touch = Input.GetTouch(1);

            if (touch.phase == TouchPhase.Began)
            {
                // Halve the size of the cube.
                transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                // Restore the regular size of the cube.
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
        }
    }*/

        /*
         * Notes on below:
         * 
         * The implementation below is merely a proof of concept of the event system working. 
         * there may be a need to revisit how to handle input down the road.
         * Namely, the type references / GetComponent<T> are something that should be steered away from.
         */
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 pos = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D[] hits = Physics2D.RaycastAll(pos, Vector2.zero);
            if (hits.Length > 0)
            { 
                RaycastHit2D hit = hits[0];
                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.gameObject);
                    if (hit.collider.gameObject.GetComponent<TestCharacter>())
                    {
                        OnCharacterClickedEventArgs args = new OnCharacterClickedEventArgs();
                        args.param0 = hit.collider.gameObject.name;

                        PersistentGameManager.Instance.EventHub.RaiseEvent(args);
                    }
                    else if (hit.collider.gameObject.GetComponent<TilemapCollider2D>())
                    {
                        var grid = hit.collider.gameObject.GetComponentInParent<GridLayout>();

                        //var gridPos = grid.GetCellCenterWorld(new Vector3Int((int)hit.collider.ClosestPoint(pos).x, (int)hit.collider.ClosestPoint(pos).y, 0));
                        var gridPos = grid.WorldToCell(new Vector3(hit.collider.ClosestPoint(pos).x, hit.collider.ClosestPoint(pos).y, 0));

                        OnScreenClickedEventArgs args = new OnScreenClickedEventArgs();
                        args.param0 = hit.collider.ClosestPoint(pos);
                        Debug.Log(hit.collider.ClosestPoint(pos) + "=>" + gridPos);

                        PersistentGameManager.Instance.EventHub.RaiseEvent(args);
                    }
                }
            }
            //Debug.Log(position + " " + Input.mousePosition);

        }
        
    }
}

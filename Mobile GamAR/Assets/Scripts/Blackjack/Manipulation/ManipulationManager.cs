﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManipulationManager : MonoBehaviour
{
    GameObject selectedObject;

    public GameObject handToolbar;
    ToolbarManager toolbarManager;

    public DeckManager deckManager;

    public Transform arCamera;

    bool isMoving;
    bool isRotating;

    private void Start()
    {
        DeSelectObject();

        toolbarManager = handToolbar.GetComponent<ToolbarManager>();
    }

    void Update()
    {
        if (selectedObject == null)
        {
            return;
        }

        if (isMoving)
        {
            if (!toolbarManager.GetActive())
            {
                ClearManipulation();
                return;
            }
            // y of 0 keeps going below surface... not sure why
            selectedObject.transform.position = new Vector3(toolbarManager.toolbarTip.position.x, 0.01f, toolbarManager.toolbarTip.position.z);
        }

        else if (isRotating)
        {
            if (!toolbarManager.GetActive())
            {
                ClearManipulation();
                return;
            }

            Transform toolbarTip = toolbarManager.toolbarTip;
            selectedObject.transform.LookAt(toolbarTip);
            Quaternion rotation = selectedObject.transform.rotation;
            selectedObject.transform.rotation = new Quaternion(0, rotation.y, 0, rotation.w);
        }
    }

    public bool SelectObject(GameObject gameObject)
    {
        if (selectedObject != null)
        {
            return false;
        }
        selectedObject = gameObject;
        return true;
    }

    public void DeSelectObject()
    {
        ClearManipulation();
        selectedObject = null;
    }

    public void MoveObject()
    {
        isMoving = !isMoving;
    }

    public void RotateObject()
    {
        isRotating = !isRotating;
    }

    public void FlipCard()
    {
        if (selectedObject == null || !selectedObject.CompareTag("Card"))
        {
            return;
        }
        ClearManipulation();
        selectedObject.transform.Rotate(new Vector3(0, 0, 180));

    }

    public void DiscardCard()
    {
        if (selectedObject == null || !selectedObject.CompareTag("Card"))
        {
            return;
        }
        ClearManipulation(); // might be repetitive
        deckManager.AddCardToBottomOfDeck(selectedObject);
        DeSelectObject();
    }

    public void PeakCard()
    {
        //if (selectedObject == null || !selectedObject.CompareTag("Card"))
        //{
        //    return;
        //}
        //ClearManipulation();

        //if (!isPeaking)
        //{
        //    isPeaking = true;
        //}
        //else
        //{
        //    isPeaking = false;

        //    Vector3 position = selectedObject.transform.position;
        //    Quaternion rotation = selectedObject.transform.rotation;

        //    selectedObject.transform.position = new Vector3(position.x, 0.02f, position.z);
        //    selectedObject.transform.rotation = new Quaternion(0f, rotation.y, rotation.z, rotation.w);
        //}
    }

    public void DiscardChip()
    {
        if (selectedObject == null || !selectedObject.CompareTag("Chip"))
        {
            return;
        }
        ClearManipulation();
        Destroy(selectedObject);
        DeSelectObject();
    }

    void ClearManipulation()
    {
        isMoving = false;
        isRotating = false;
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjectController : MonoBehaviour
{

    //Step 3: Keycode variables
    public KeyCode nextObject = KeyCode.T;
    public KeyCode placeObject = KeyCode.Y;
    public KeyCode deleteObject = KeyCode.R;

    public KeyCode rotateLeft = KeyCode.Comma;
    public KeyCode rotateRight = KeyCode.Period;
    public KeyCode moveNorth = KeyCode.I;
    public KeyCode moveSouth = KeyCode.K;
    public KeyCode moveEast = KeyCode.L;
    public KeyCode moveWest = KeyCode.J;
    public KeyCode moveUp = KeyCode.U;
    public KeyCode moveDown = KeyCode.O;
    public KeyCode scaleUp = KeyCode.M;
    public KeyCode scaleDown = KeyCode.N;

    //Step 3: Speed variables
    public float rotateSpeed = 1f;
    public float moveSpeed = 1f;
    public float scaleSpeed = 2f;

    //Step 3: Prefab list variables
    private int currentObject = 0;
    private Object[] objectPrefabList;

    //Step 3: Selected object variable
    private GameObject selectedObject;

    //Step 3: PlacePlane reference
    public GameObject placePlane;

    void Start()
    {
        //Step 4.1: Load the prefabs
        objectPrefabList = Resources.LoadAll("Prefabs", typeof(GameObject));
    }

    void Update()
    {
        //Step 4.2: Iterate through object list
        if (Input.GetKeyDown(nextObject))
        {
            //Step 4.3: Checks if the currentObject item is in the bounds of the amount of prefabs.
            if (objectPrefabList.Length - 1 > currentObject)
            {
                currentObject = currentObject + 1;
            }
            else
            {
                currentObject += 0;
            }
        }

        //Step 5.1: Select object
        if (Input.GetMouseButtonDown(0))
        {
            var clickedObject = GetObjectAtMouse();
            if (clickedObject != null)
            {
                selectedObject = clickedObject;
            }
        }

        //Step 6.1: Placing object
        if (Input.GetKeyDown(placeObject))
        {
            PlaceObjectAtMouse((GameObject)objectPrefabList[currentObject]);
        }

        //Step 6.2: Deleting object
        if (Input.GetKeyDown(deleteObject))
        {
            if (selectedObject != null)
            {
                Destroy(selectedObject);
            }
        }

        //Step 7.1 Handling the movement of the selected object
        if (selectedObject != null)
        {
            HandleObjectMovement();
        }
    }

    //Step 5.2: Getting the object at the mouse position
    private GameObject GetObjectAtMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        //Step 5.3: Sending the raycast
        if (Physics.Raycast(ray, out hitInfo, 10000, 1 << LayerMask.NameToLayer("SelectLayer")))
        {
            return hitInfo.collider.gameObject;
        }
        return null;
    }

    //Step 6.3: Function for placing a object 
    private void PlaceObjectAtMouse(GameObject placeObject)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            //6.4 Placing the object
            GameObject currentPlaceableObject = Instantiate(placeObject);
            currentPlaceableObject.transform.parent = transform;
            currentPlaceableObject.transform.position = new Vector3(hitInfo.point.x, placePlane.transform.position.y, hitInfo.point.z);
            selectedObject = currentPlaceableObject;
        }
    }


    //Step 7.2: The movement function
    private void HandleObjectMovement()
    {
        //Step 7.3: Rotating
        if (Input.GetKey(rotateLeft))
        {
            selectedObject.transform.Rotate(new Vector3(0, 1, 0) * rotateSpeed);
        }

        if (Input.GetKey(rotateRight))
        {
            selectedObject.transform.Rotate(new Vector3(0, -1, 0) * rotateSpeed);
        }

        //Step 7.4: Moving
        if (Input.GetKey(moveNorth))
        {
            selectedObject.transform.Translate(Vector3.right * moveSpeed);
        }

        if (Input.GetKey(moveSouth))
        {
            selectedObject.transform.Translate(Vector3.left * moveSpeed);
        }

        if (Input.GetKey(moveEast))
        {
            selectedObject.transform.Translate(Vector3.forward * moveSpeed);
        }

        if (Input.GetKey(moveWest))
        {
            selectedObject.transform.Translate(Vector3.back * moveSpeed);
        }

        if (Input.GetKey(moveUp))
        {
            selectedObject.transform.Translate(Vector3.up * moveSpeed);
        }

        if (Input.GetKey(moveDown))
        {
            selectedObject.transform.Translate(Vector3.down * moveSpeed);
        }

        //Step 7.5: Scaling
        if (Input.GetKey(scaleUp))
        {
            selectedObject.transform.localScale += Vector3.one * scaleSpeed;
        }

        if (Input.GetKey(scaleDown))
        {
            selectedObject.transform.localScale -= Vector3.one * scaleSpeed;
        }
    }
}

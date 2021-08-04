using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Step 8.1: Implementing name spaces
using ArcGISMapsSDK.Components;
using Esri.GameEngine.Location;
using Esri.Core.Utils.Math;

public class PlacePlane : MonoBehaviour
{
    //Step 8.1: Variable for the location
    ArcGISLocationComponent location;

    void Start()
    {
        //Step 8.1: Getting the locatioin
        location = GetComponent<ArcGISLocationComponent>();
    }

    void Update()
    {
        //Step 8.2: Getting the coords of the cam
        var camPos = GetCurrentCoords();

        //Step 8.4: Setting the position to the camera position
        location.Latitude = (float)camPos.Latitude;
        location.Longitude = (float)camPos.Longitude;
    }

    //Step 8.2: The function go get the local coords
    private ArcGISGlobalCoordinatesPosition GetCurrentCoords()
    {
        //Step 8.3: Getting the renderer
        ArcGISRendererComponent render = GameObject.FindObjectOfType<ArcGISRendererComponent>();
        if (render != null)
        {
            //Step 8.3: Getting the origin and transforming it to latitude and longitude
            var localOrigin = render.RendererScene.ToCartesianCoord(render.RendererView.Camera.TransformationMatrix, false);
            var localPos = render.RendererScene.ToLatLonAlt(new Vector3d(localOrigin.x, localOrigin.y, localOrigin.z));

            return localPos;
        }
        return null;
    }
}

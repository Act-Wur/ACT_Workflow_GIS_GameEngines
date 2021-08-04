using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Step 2: SDK Components
using ArcGISMapsSDK.Components;
using Esri.GameEngine.Camera;
using Esri.GameEngine.Extent;
using Esri.GameEngine.Location;
using Esri.GameEngine.View;
using Esri.GameEngine.View.Event;
using Esri.Unity;

public class ApiScene_Wageningen : MonoBehaviour
{
    //Step 2: Position variables
    double latitude = 51.9691868;
    double longitude = 5.6653948;

    void Start()
    {
        //Step 2: API Key
        string apiKey = "";

        //Step 2: View mode
        var viewMode = Esri.GameEngine.Map.ArcGISMapType.Global;

        //Step 2: The Map Component
        var arcGISMap = new Esri.GameEngine.Map.ArcGISMap(viewMode);

        //Step 3: Set the Basemap
        arcGISMap.Basemap = new Esri.GameEngine.Map.ArcGISBasemap("https://www.arcgis.com/sharing/rest/content/items/8d569fbc4dc34f68abae8d72178cee05/data", apiKey);

        //Step 3: Create the Elevation
        arcGISMap.Elevation = new Esri.GameEngine.Map.ArcGISMapElevation(new Esri.GameEngine.Elevation.ArcGISImageElevationSource("https://elevation3d.arcgis.com/arcgis/rest/services/WorldElevation3D/Terrain3D/ImageServer", "Elevation", apiKey));

        //Step 3.1 Adding your custom layer
        var layer_1 = new Esri.GameEngine.Layers.ArcGIS3DModelLayer("C:/Local/Gebouwen_Wageningen_Scene.slpk", "Wageningen", 1.0f, true, apiKey);
        layer_1.Opacity = 0.9f;
        arcGISMap.Layers.Add(layer_1);


        //Step 4: For this tutorial, we continue with the global view mode.

        //Step 5: Adding the camera and controller
        ArcGISCameraComponent cameraGE = Camera.main.gameObject.AddComponent<ArcGISCameraComponent>();
        ArcGISCameraControllerComponent controller = Camera.main.gameObject.AddComponent<ArcGISCameraControllerComponent>();

        //Step 5: Setup of the position of the camera
        var position = new ArcGISGlobalCoordinatesPosition(latitude, longitude, 3000);
        var rotation = new ArcGISRotation(68, 0, 65);

        //Step 5: Adding the values to the camera
        cameraGE.Latitude = position.Latitude;
        cameraGE.Longitude = position.Longitude;
        cameraGE.Height = position.Altitude;
        cameraGE.Heading = rotation.Heading;
        cameraGE.Pitch = rotation.Pitch;
        cameraGE.Roll = rotation.Roll;

        //Step 6: Creating the render object and component
        GameObject renderContainer = new GameObject("RenderContainer");
        ArcGISRendererComponent renderer = renderContainer.AddComponent<ArcGISRendererComponent>();

        //Step 6: Creating the camera object
        ArcGISCamera camera = new ArcGISCamera("Camera", position, rotation);

        //Step 6: Creating the renderer viewer
        ArcGISRendererViewOptions options = new ArcGISRendererViewOptions();
        ArcGISRendererView rendererView = new ArcGISRendererView(arcGISMap, camera, options);

        //Step 6: Assinging the viewer to the renderer and camera components
        renderer.RendererView = rendererView;
        cameraGE.RendererView = rendererView;

        //Step 7: Getting the skybox
        var currentSky = GameObject.FindObjectOfType<UnityEngine.Rendering.Volume>();

        //Step 7: Check if the skybox exists
        if (currentSky)
        {
            //Step 7: Create the sky component and assign the camera and renderer components to it
            ArcGISSkyRepositionComponent skyComponent = currentSky.gameObject.AddComponent<ArcGISSkyRepositionComponent>();
            skyComponent.CameraComponent = cameraGE;
            skyComponent.RendererComponent = renderer;
        }

    }

    void Update()
    {

    }
}
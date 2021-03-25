using UnityEngine;
using System.Collections;
/// <summary>
/// code modified from:
/// https://medium.com/@mukulkhanna/creating-basic-underwater-effects-in-unity-9a9400bde928
/// </summary>
public class UnderWaterCamera : MonoBehaviour
{
    [SerializeField]
    float waterHeight = 262;                 //set to water object in hierarchy's height
    [SerializeField]
    GameObject waterObj;                    //set in inspector
    [SerializeField]
    Color underWaterColor;
    [SerializeField]
    Projector fxProjector;                 //makes water caustic fx
    [SerializeField]
    Material normalSky;
    [SerializeField]
    Material underWaterSky;
    private bool isUnderwater;
    private Color normalColor;
    private Color underwaterColor;
    private float fogDensity;

    // Use this for initialization
    void Start()
    {
        normalColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        underWaterColor = new Color(50, 185, 236, 255);

        underwaterColor = RenderSettings.fogColor;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = this.transform.position;
        pos.y = fxProjector.transform.position.y;
        //fxProjector.transform.position = pos;

        isUnderwater = transform.position.y < waterHeight;
       

        if (isUnderwater) 
            SetUnderwater();
        else 
            SetNormal();
    }

    void SetNormal()
    {
        RenderSettings.skybox = normalSky;
        Quaternion rotate = waterObj.transform.rotation;
        rotate.x = 0;
        waterObj.transform.rotation = rotate;
        fxProjector.enabled = false;
        this.GetComponent<Camera>().clearFlags = CameraClearFlags.Skybox;

        RenderSettings.fogColor = normalColor;
        RenderSettings.fog = false;

    }

    void SetUnderwater()
    {
        RenderSettings.skybox = underWaterSky;
        Quaternion rotate = waterObj.transform.rotation;
        rotate.x = 180;
        waterObj.transform.rotation = rotate;
        //this.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
        fxProjector.enabled = true;
        RenderSettings.fog = true;
        RenderSettings.fogColor = underwaterColor;
        

    }
}
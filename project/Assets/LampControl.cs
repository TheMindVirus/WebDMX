using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class LampControl : MonoBehaviour
{
    float intensity = 0.0f;
    float red = 0.0f;
    float green = 0.0f;
    float blue = 0.0f;
    float alpha = 0.0f;
    float pan = 0.0f;
    float tilt = 0.0f;
    float panmult = 0.0f;
    float tiltmult = 0.0f;
    float aperture = 0.0f;
    float range = 0.0f;

    GameObject Base = null;
    GameObject Arm = null;
    GameObject Head = null;
    GameObject Spot = null;
    GameObject Cone = null;

    //[DllImport("__Internal")]
    //public static extern void Alert(string message); //Awful, doesn't work.

    void Start()
    {
        Base = this.gameObject;
        Arm = Base.transform.GetChild(0).gameObject;
        Head = Arm.transform.GetChild(0).gameObject;
        Cone = Head.transform.GetChild(0).gameObject;
        Spot = Head.transform.GetChild(1).gameObject;

        SetColour(255.0f, 255.0f, 255.0f, 255.0f, 127.0f);
        SetPanTilt(0.0f, 0.0f, 1.0f, 1.0f);
        SetCone(30.0f, 100.0f);
        //Alert("[" + Base.name.ToUpper() + "]: Ready");
    }

    void Update()
    {
        
    }

    void SetColour(float Intensity = 255.0f, float Red = 255.0f, float Green = 255.0f, float Blue = 255.0f, float Alpha = 127.0f)
    {
        this.intensity = Intensity;
        this.red = Red;
        this.green = Green;
        this.blue = Blue;
        this.alpha = Alpha;
        Color colour = new Color(this.red, this.green, this.blue, this.alpha) / 255.0f;
        Cone.GetComponent<Renderer>().material.SetColor("_Color", colour);
        Cone.GetComponent<Renderer>().material.SetColor("_EmissionColor", colour);
        Spot.GetComponent<Light>().color = colour;
        Spot.GetComponent<Light>().intensity = this.intensity / 255.0f;
    }

    void SetPanTilt(float Pan = 0.0f, float Tilt = 0.0f, float PanMult = 1.0f, float TiltMult = 1.0f)
    {
        this.pan = Pan;
        this.tilt = Tilt;
        this.panmult = PanMult;
        this.tiltmult = TiltMult;
        Base.transform.eulerAngles = new Vector3(90.0f + (this.tilt * this.tiltmult), -90.0f + (this.pan * this.panmult), 0.0f);
    }

    void SetCone(float Aperture = 30.0f, float Range = 100.0f)
    {
        this.aperture = Aperture;
        this.range = Range;
        Cone.transform.localScale = new Vector3(this.aperture * 4.0f, this.aperture * 2.0f, this.range);
        Cone.transform.localPosition = new Vector3(Cone.transform.localPosition.x, Cone.transform.localPosition.y, this.range / 100.0f);
        Spot.GetComponent<Light>().spotAngle = this.aperture;
        Spot.GetComponent<Light>().range = this.range;
    }

    //REQUIRED HELPER FUNCTIONS BECAUSE OF STUPID 1 PARAMETER LIMIT WITHOUT JSON ON SENDMESSAGE

    public void SetIntensity(float Intensity) { SetColour(Intensity, this.red, this.green, this.blue, this.alpha); }
    public void SetRed(float Red) { SetColour(this.intensity, Red, this.green, this.blue, this.alpha); }
    public void SetGreen(float Green) { SetColour(this.intensity, this.red, Green, this.blue, this.alpha); }
    public void SetBlue(float Blue) { SetColour(this.intensity, this.red, this.green, Blue, this.alpha); }
    public void SetAlpha(float Alpha) { SetColour(this.intensity, this.red, this.green, this.blue, Alpha); }

    public void SetPan(float Pan) { SetPanTilt(Pan, this.tilt, this.panmult, this.tiltmult); }
    public void SetTilt(float Tilt) { SetPanTilt(this.pan, Tilt, this.panmult, this.tiltmult); }
    public void SetPanMult(float PanMult) { SetPanTilt(this.pan, this.tilt, PanMult, this.tiltmult); }
    public void SetTiltMult(float TiltMult) { SetPanTilt(this.pan, this.tilt, this.panmult, TiltMult); }

    public void SetAperture(float Aperture) { SetCone(Aperture, this.range); }
    public void SetRange(float Range) { SetCone(this.aperture, Range); }
}

using UnityEngine;


namespace DroneModel.Animations {
public class RotatingPropellers : MonoBehaviour {
    
    public float power = 1;
    public bool counterclockwise = false;
    public bool animationActivated = false;
    
    void Update() { if (animationActivated) transform.Rotate(0, 0, power * 700 * Time.deltaTime * (counterclockwise ? -1 : 1)); }
}
}

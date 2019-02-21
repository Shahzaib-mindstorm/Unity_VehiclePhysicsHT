using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace VehiclePhysicsHT
{
[CustomEditor(typeof(WheelColliderComponent))]
public class WheelColliderComponentEditor : Editor
{
    
    WheelColliderComponent coll;

    
    // This function is called when the object becomes enabled and active.
    void OnEnable()
    {
        coll = (WheelColliderComponent)target;
    }

    void OnSceneGUI() {



    }

    private void drawCirlce(Vector3 center) {

        Handles.DrawWireArc(center, Vector3.up, Vector3.right, 360f, .3f);

    }

}
}
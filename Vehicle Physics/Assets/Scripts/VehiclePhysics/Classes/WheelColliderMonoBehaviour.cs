using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VehiclePhysicsHT
{
public abstract class WheelColliderMonoBehavior : MonoBehaviour {

    public abstract void UpdateSuspension();
    public abstract void CalculateForces();
    public abstract void ApplyForces();

}
}
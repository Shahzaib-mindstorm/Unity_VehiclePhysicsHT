
namespace VehiclePhysicsHT
{
public struct Friction {

    public float staticCoefficientForward;
    public float rollingCoefficientForward;
    public float dynamicCoefficientForward;

    public float staticCoefficientLateral;
    public float rollingCoefficientLateral;
    public float dynamicCoefficientLateral;

    public Friction(float sCF, float rCF, float dCF, float sCL, float rCL, float dCL) {
        
        staticCoefficientForward  = sCF;
        rollingCoefficientForward = rCF;
        dynamicCoefficientForward = dCF;

        staticCoefficientLateral  = sCL;
        rollingCoefficientLateral = rCL;
        dynamicCoefficientLateral = dCL;

    }

}
}
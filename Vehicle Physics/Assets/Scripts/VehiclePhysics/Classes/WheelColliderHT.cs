using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VehiclePhysicsHT
{

[System.Serializable]
public abstract class WheelColliderHT : MonoBehaviour
{
    [Header("Wheel")]
    [SerializeField, Tooltip("The tire Radius. SI -> m.")]
    protected float m_tireRadius = 0.5f;

    [SerializeField, Tooltip("The mass of whole wheel. Tire and Rim. SI -> kg")]
    protected float m_wheelMass = 25f;

    [SerializeField]
    protected float m_tireWidth = 0.2f;


    [Header("Suspension")]
    
    [SerializeField]
    protected float m_SpringForce = 25000.0f;
    
    [SerializeField]
    protected float m_DampingForce = 3000.0f;
    
    [SerializeField]
    protected float m_SpringLength = 0.5f;
    
    [SerializeField, Range(0.0f, 1.0f)] 
    protected float m_TargetPosition = 0.5f;

    [SerializeField]
    protected GameObject m_Visual;

    [SerializeField]
    protected Rigidbody m_ParentRigidbody;


    protected float m_MotorTorque = 0.0f;

    protected RaycastHit hit;

    private float m_currentSpringForce = 0.0f;
    private float m_currentDamperForce = 0.0f;

    protected float m_compression = 0f;
    protected float m_prevCompression = 0f;

    protected Vector3 m_PrevPosition = new Vector3(0.0f, 0.0f, 0.0f);

    protected Vector3 m_Velocity = new Vector3(0.0f, 0.0f, 0.0f);
    protected Vector3 m_PrevVelocity = new Vector3(0.0f, 0.0f, 0.0f);

    protected Vector3 m_Acceleration = new Vector3(0.0f, 0.0f, 0.0f);

    protected Vector3 m_Total_Force = new Vector3(0.0f, 0.0f, 0.0f);

    protected bool m_isGrounded = false;


    //
    // Private Fields
    //
    private float m_TargetDistance;


    //
    // Public Getter/Setter
    //

    public float TireRadius 
    {
        get { return this.m_tireRadius; }
    }

    public float MotorTorque {
        set { m_MotorTorque = value; }
    }

    //
    // Private Getter/Setter
    //

    ///<summary>
    /// Return the maximal distance of suspension length added to the tire radius.
    ///</summary>
    protected float maxTireDistance {
        get { return m_tireRadius + m_SpringLength;}
    }

    protected float forwardForce {
        get { return m_Total_Force.z; }
        set { m_Total_Force.z = value; }
    }

    // **************
    // Methods
    // **************

    ///<summary>
    /// Calculates the compression of the spring and if the wheel is grounded.
    ///</summary>
    protected virtual void UpdateSuspension() 
    {

        Vector3 origin = transform.position;

        m_prevCompression = m_compression;
        if(Physics.Raycast(origin, -transform.up, out hit, maxTireDistance)) {
            
            m_isGrounded = true;
            m_compression = m_SpringLength + m_tireRadius - (hit.point - transform.position).magnitude;

        } else 
        {
            m_isGrounded = false;
            m_compression = 0f;
        }
    }

    ///<summary>
    /// If the wheel is grounded the forces will be calculated based on compression
    ///</summar>
    protected virtual void CalculateForces() 
    {
        
    }


    protected virtual void CalculateSuspensionForce() 
    {
        m_currentSpringForce = (m_compression - m_SpringLength * m_TargetDistance) * m_SpringForce;
        m_currentDamperForce = ((m_compression - m_prevCompression) / Time.fixedDeltaTime) * m_DampingForce;
    }

    protected virtual void CalculateFriction() 
    {
        Vector3 globalVelocity = (transform.position - m_PrevPosition) / Time.fixedDeltaTime;

        m_Velocity = Quaternion.Inverse(m_Visual.transform.localRotation) * globalVelocity;

        m_Acceleration = (m_Velocity - m_PrevVelocity) / Time.fixedDeltaTime;

        float tireForce = m_MotorTorque / m_tireRadius;

    }

    protected virtual void ApplyForces() 
    {
        
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    protected virtual void Start()
    {
        if(m_ParentRigidbody == null)
            m_ParentRigidbody = GetComponentInParent<Rigidbody>();

        m_TargetDistance = m_TargetPosition * m_SpringLength;
        m_PrevPosition = transform.position;
    }

}

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooglyEye : MonoBehaviour
{
    public Transform Eye;
    [Range(0.5f, 10f)]
    public float Speed = 1f;
    [Range(0f, 5f)]
    public float GravityMultiplier = 1f;
    [Range(0.01f, 0.98f)]
    public float Bounciness = 0.4f;

    private Vector3 _origin;
    private Vector3 _velocity;
    private Vector3 _lastPosition;

    void Start()
    {
        _origin = Eye.localPosition;
        _lastPosition = transform.position;
    }

    void Update()
    {
        const float maxDistance = 0.25f;

        var currentPosition = transform.position;

        var gravity = transform.InverseTransformDirection(Physics.gravity);

        _velocity += gravity * GravityMultiplier * Time.deltaTime;
        _velocity += transform.InverseTransformVector((_lastPosition - currentPosition)) * 500f * Time.deltaTime;
        _velocity.z = 0f;

        var position = Eye.localPosition;

        position += _velocity * Speed * Time.deltaTime;

        var direction = new Vector2(position.x, position.y);
        var angle = Mathf.Atan2(direction.y, direction.x);

        if(direction.magnitude > maxDistance)
        {
            var normal = -direction.normalized;

            _velocity = Vector2.Reflect(new Vector2(_velocity.x, _velocity.y), normal) * Bounciness;
            
            position = new Vector3(
                Mathf.Cos(angle) * maxDistance,
                Mathf.Sin(angle) * maxDistance,
                0f
            );
        }

        position.z = Eye.localPosition.z;
        Eye.localPosition = position;
        _lastPosition = transform.position;
    }
}

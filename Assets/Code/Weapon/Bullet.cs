using UnityEngine;

public class Bullet
{
    private BulletProvider _bulletView;
    private Vector3 _velocity;
    public Bullet(BulletProvider bulletView)
    {
        _bulletView = bulletView;
    }
    public void Update()
    {
        if (IsGrounded())
        {

            SetVelocity(_velocity.Change(y: -_velocity.y / 2));
            _bulletView.transform.position = _bulletView.transform.position.Change(y: _bulletView.BulletGroundLevel + _bulletView.BulletRadius);

            return;
        }
        else
        {
            SetVelocity(_velocity + Vector3.up * _bulletView.BulletAcceleration * Time.deltaTime);
            _bulletView.transform.position += _velocity * Time.deltaTime;
        }
    }
    public void Throw(ref Vector3 position, ref Vector3 velocity)
    {
        _bulletView.transform.position = position;
        SetVelocity(velocity);
        // _bulletView.SetVisible(true);
    }
    private void SetVelocity(Vector3 velocity)
    {
        _velocity = velocity;

        var angle = Vector3.Angle(Vector3.right, _velocity);
        var axis = Vector3.Cross(Vector3.right, _velocity);

        _bulletView.transform.rotation = Quaternion.AngleAxis(angle, axis);
    }
    private bool IsGrounded()
    {
        return _bulletView.transform.position.y <= _bulletView.BulletGroundLevel + _bulletView.BulletRadius && _velocity.y <= 0;
    }
}

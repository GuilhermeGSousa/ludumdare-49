using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void OnDamage(float damage);
}

public interface IPushable
{
    void OnPush(Vector2 pushImpulse);
}

public interface IEventListener
{
    void OnEventRaised();
}

public interface IEventListener<T>
{
    void OnEventRaised(T parameter);
}

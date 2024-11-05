using UnityEngine;
//everything a fighter 'should' have!

public interface IFighter
{
    //health & damage type
    float Health { get; set; }
    void HealthUpdate(float amount);

    //playerindex for Input
    int fighterIndex { get; }
    void SetMoveVector(Vector2 vector2);
    void TriggerPunch();
}

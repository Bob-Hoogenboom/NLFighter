using UnityEngine;
//everything a fighter 'should' have!

public interface IFighter
{
    //health & damage type

    //playerindex for Input
    int fighterIndex { get; }
    void SetMoveVector(Vector2 vector2);
    void SetPunchBool(bool value);
}

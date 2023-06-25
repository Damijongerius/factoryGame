using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldObjects;

public interface ITile
{
    public void Init(Dictionary<Vector2, GameObject> content, ITileBehavior behavior, WorldObjects.Order tpye);
    public void SetBehavior(ITileBehavior behavior);
    public ITileBehavior Getbehavior();

    public WorldObjects.Order GetType();
    public List<Vector2> GetPosition();
    public Dictionary<Vector2, GameObject> getObjectPositions();
    public bool ContainsPositions(List<Vector2> poss);
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeBehavior: MonoBehaviour
{
    [SerializeField]
    protected int offsetWeightsFactor;
}

public interface OnFixedUpdateNodeBehavior
{
    void OnFixedUpdate();
}

public interface OnInteractSendNodeBehavior
{
    void OnInteractSend(GraphyNode.Interaction interaction);
}

public interface OnInteractReceiveNodeBehavior
{
    void OnInteractReceive(GraphyNode.Interaction interaction);
}

public interface OnDeathNodeBehavior
{
    void OnDeath();
}

public interface OnBirthNodeBehavior
{
    void OnBirth();
}

public interface OnConsumeNodeBehavior
{
    void OnConsume(float amount, Food food);
}

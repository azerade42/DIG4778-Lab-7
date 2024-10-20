
using UnityEngine;

public abstract class TargetBuilder
{
    public Target Target { get; set; }

    public void Construct()
    {
        BuildSpeed();
        BuildSize();
        BuildPointValue();
        BuildColor();
    }

    protected abstract void BuildSpeed();
    protected abstract void BuildSize();
    protected abstract void BuildPointValue();
    protected abstract void BuildColor();

}

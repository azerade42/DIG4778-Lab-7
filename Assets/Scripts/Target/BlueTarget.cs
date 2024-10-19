
public class BlueTarget : TargetBuilder
{
    protected override void BuildPointValue()
    {
        Target.PointValue = 5;
    }

    protected override void BuildSize()
    {
        Target.Size = 1.25f;
    }

    protected override void BuildSpeed()
    {
        Target.MoveSpeed = 3f;
    }

    protected override void BuildColor()
    {
        Target.SpriteColor = UnityEngine.Color.blue;
    }
}

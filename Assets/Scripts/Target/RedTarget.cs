
public class RedTarget : TargetBuilder
{
    protected override void BuildPointValue()
    {
        Target.PointValue = 10;
    }

    protected override void BuildSize()
    {
        Target.Size = 0.5f;
    }

    protected override void BuildSpeed()
    {
        Target.MoveSpeed = 6f;
    }

    protected override void BuildColor()
    {
        Target.SpriteColor = UnityEngine.Color.red;
    }
}

using UnityEngine;

public class Apple : Pickup
{
    [SerializeField] float adjustChunkSpeedAmount = 3f;

    LevelGenerator levelGenerator;
    public void Init(LevelGenerator levelGenerator)
    {
        this.levelGenerator = levelGenerator;
    }
    protected override void OnPickup()
    {
        levelGenerator.ChangeChunkMoveSpeed(adjustChunkSpeedAmount);
    }
}

public class BlockData 
{
    public bool Up;
    public bool Down;
    public bool Left;
    public bool Right;
    public BlockType type;

    //Constructor
    public BlockData(BlockType bt, bool up, bool down, bool left, bool right)
    {
        type = bt;
        Up = up;
        Down = down;
        Left = left;
        Right = right;
    }
}

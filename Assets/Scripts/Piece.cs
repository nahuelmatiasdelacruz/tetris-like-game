using UnityEngine;

public class Piece : MonoBehaviour
{
    
    bool IsValidPiecePosition()
    {
        foreach(Transform block in this.transform)
        {
            Vector2 position = GridHelper.RoundVector(block.position);
            if (!GridHelper.IsInsideBorders(position))
            {
                return false;
            }
            Transform possibleObject = GridHelper.grid[(int)position.x, (int)position.y];
            if (possibleObject != null && possibleObject.parent != this.transform)
            {
                return false;
            }
        }
        return true;
    }

    void UpdateGrid()
    {
        for(int y = 0; y < GridHelper.height; y++)
        {
            for(int x = 0; x < GridHelper.width; x++)
            {
                if (GridHelper.grid[x,y] != null)
                {
                    if (GridHelper.grid[x,y].parent == this.transform)
                    {
                        GridHelper.grid[x, y] = null;
                    }
                }
            }
        }
        foreach(Transform block in this.transform)
        {
            Vector2 pos = GridHelper.RoundVector(block.position);
            GridHelper.grid[(int)pos.x, (int)pos.y] = block;
        }
    }
}

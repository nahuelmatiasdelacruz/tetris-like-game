using UnityEngine;

public class Piece : MonoBehaviour
{
    float lastFall = 0.0f;

    private void Start()
    {
        if (!IsValidPiecePosition())
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            MovePieceHorizontally(-1);
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            MovePieceHorizontally(1);
        } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            this.transform.Rotate(new Vector3(0, 0, -90));
            if (IsValidPiecePosition())
            {
                UpdateGrid();
            }
            else
            {
                this.transform.Rotate(new Vector3(0, 0, 90));
            }
        } else if (Input.GetKeyDown(KeyCode.DownArrow) || (Time.time - lastFall) > 1.0f )
        {
            this.transform.position += new Vector3(0, -1, 0);
            if (IsValidPiecePosition())
            {
                UpdateGrid();
            }
            else
            {
                this.transform.position += new Vector3(0, 1, 0);
                GridHelper.DeleteAllFullRows();
                FindFirstObjectByType<Spawner>().SpawnNextPiece();
                this.enabled = false;
            }
            lastFall = Time.time;
        }
    }
    
    void MovePieceHorizontally(int direction)
    {
        this.transform.position += new Vector3(direction, 0, 0);
        if (IsValidPiecePosition())
        {
            UpdateGrid();
        }
        else
        {
            this.transform.position += new Vector3(-direction, 0, 0);
        }
    }
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

    public void ChangeBlocksTransparency(float newValue)
    {
        foreach(SpriteRenderer blockRenderer in GetComponentsInChildren<SpriteRenderer>())
        {
            blockRenderer.color = new Color(blockRenderer.color.r, blockRenderer.color.g, blockRenderer.color.b, newValue);
        }
    }
}

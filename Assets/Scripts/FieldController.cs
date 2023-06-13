using UnityEngine;

public class FieldController : MonoBehaviour
{
    
    [SerializeField] float stepDelay;
    [SerializeField] Figure figureGhost;

    readonly FieldLogic fieldLogic = new FieldLogic();
    readonly FieldVisual fieldVisual = new FieldVisual();

    float firstStepDelay = 0.4f;

    FigureManager figureManager;
    Figure currentFigure;
    float stepTime;
    Vector3 startPosition;
    bool gameContinuing;


    public void StartGameplay()
    {
        NullingFigure();
        fieldLogic.CreateStartFieldLogic();
        fieldVisual.SetFieldVisual(fieldLogic.UpdateLogic());

        figureManager = FindObjectOfType<FigureManager>();

        stepTime = Time.time + firstStepDelay;
        gameContinuing = true;
        startPosition = new Vector3((int)((FieldLogic.Width/2) - 2), FieldLogic.VisibleHeight - 1);
    }

    void Start()
    {
        DelegateController.blockDestroing += fieldVisual.SetBlockParticleOnDestroy;
        DelegateController.gameplayStopContinue += GameStopContinuing;

        ManagersCache.instance.InputChecker.input.stepLeft += LeftPressed;
        ManagersCache.instance.InputChecker.input.stepRight += RightPressed;
        ManagersCache.instance.InputChecker.input.stepDown += DownPressed;
        ManagersCache.instance.InputChecker.input.fall += FallPressed;
        ManagersCache.instance.InputChecker.input.rotateLeft += LeftRotatePressed;
        ManagersCache.instance.InputChecker.input.rotateRight += RightRotatePressed;
    }
    void LeftPressed()
    {
        if (Time.timeScale == 1)
        {
            TryToMove(Vector3.left, currentFigure);
        }
    }
    void RightPressed()
    {
        if (Time.timeScale == 1)
        {
            TryToMove(Vector3.right, currentFigure);
        }
    }
    void DownPressed()
    {
        if (Time.timeScale == 1)
        {
            TryStepDown();
        }
    }
    void FallPressed()
    {
        if (Time.timeScale == 1 && currentFigure != null && currentFigure.gameObject.activeSelf)
        {
            Fall();
        }
    }
    void LeftRotatePressed()
    {
        if (Time.timeScale == 1)
        {
            TryToRotate(rotateIndex: -1);
        }
    }
    void RightRotatePressed()
    {
        if (Time.timeScale == 1)
        {
            TryToRotate(rotateIndex: 1);
        }
    }

    void Update()
    {
        if (!gameContinuing )
        {
            return;
        }
        
        if (currentFigure == null)
        {
            SetNewCurrentFigure();
        }

        if (!currentFigure.gameObject.activeSelf)
        {
            currentFigure.gameObject.SetActive(true);
        }
        

        if (Time.time >= stepTime)
        {
            StepDown();
        }
        if (currentFigure != null)
        {
            DisplayGhost(currentFigure);
        }

        fieldVisual.SetFieldVisual(fieldLogic.UpdateLogic());
        CheckGameOver();

        void SetNewCurrentFigure()
        {
            currentFigure = figureManager.figures[Random.Range(0, figureManager.figures.Length)];

            currentFigure.gameObject.SetActive(true);
            if (currentFigure.FigureType == FigureType.I)
            {
                currentFigure.gameObject.transform.position = new Vector3(startPosition.x, startPosition.y - 1);
            }
            else
            {
                currentFigure.gameObject.transform.position = startPosition;
            }
            
            currentFigure.figureRotateIndex = FigureRotate.First;
            Data.SetBlocksOnPosition(currentFigure, Data.GetBlocksOnPosition(currentFigure));
            figureGhost.gameObject.SetActive(true);
        }

        void CheckGameOver()
        {
            for (int x = 0; x < FieldLogic.Width; x++)
            {
                if (fieldLogic.fieldCells[FieldLogic.VisibleHeight,x].hasObj)
                {
                    GameOver();
                }
            }
        }
        
    }

    void NullingFigure()
    {
        if (currentFigure != null)
        {
            currentFigure.gameObject.SetActive(false);
        }
        currentFigure = null;
    }

    void GameOver()
    {
        GameStopContinuing();
        PoolManager.Instanse.Deactive("Block");
        if (currentFigure != null)
        {
           currentFigure.gameObject.SetActive(false); 
        }
        DelegateController.gameOver?.Invoke();
    }

    void GameStopContinuing()
    {
        gameContinuing = false;
    }

    void DisplayGhost(Figure currentFigure)
    {
        for (int i = 0; i < figureGhost.blocks.Length; i++)
        {
            figureGhost.blocks[i].gameObject.transform.position = currentFigure.blocks[i].gameObject.transform.position;
        }

        while(TryToMove(Vector3.down,figureGhost)) {}
    }

    void TryToRotate(int rotateIndex)
    {
        FigureRotate oldIndex = currentFigure.figureRotateIndex;
        currentFigure.figureRotateIndex = (FigureRotate)Wrap((int)currentFigure.figureRotateIndex + rotateIndex, 0 , 4);
        
        Block[] proxyBlocks = Data.GetBlocksOnPosition(currentFigure);
        for (int i = 0; i < proxyBlocks.Length; i++)
        {
            Vector3 posToCheck = proxyBlocks[i].transform.position;
            if (posToCheck.y < 0 || posToCheck.x < 0 || posToCheck.y >= fieldLogic.fieldCells.GetLength(0) || 
                posToCheck.x >= fieldLogic.fieldCells.GetLength(1) || fieldLogic.fieldCells[(int)posToCheck.y,(int)posToCheck.x].hasObj)
            {
                DelegateController.rotateImpossible?.Invoke();
                currentFigure.figureRotateIndex = oldIndex;
            }
        }

        Data.SetBlocksOnPosition(currentFigure, Data.GetBlocksOnPosition(currentFigure));
    }

    void StepDown()
    {
        stepTime = Time.time + stepDelay;
        TryStepDown();
        
    }
    void TryStepDown()
    {
        if(!TryToMove(Vector3.down, currentFigure))
        {
            ManagersCache.instance.AudioManager.PlaySound2D("Fall");
            NextFigure();
        }
    }

    void Fall()
    {
        while(TryToMove(Vector3.down, currentFigure)){}

        ManagersCache.instance.AudioManager.PlaySound2D("Fall");
        NextFigure();
    }

    void NextFigure()
    {
        
        for (int i = 0; i < currentFigure.blocks.Length; i++)
        {
            int yPos = (int)currentFigure.blocks[i].gameObject.transform.position.y;
            int xPos = (int)currentFigure.blocks[i].gameObject.transform.position.x;
            
            fieldLogic.fieldCells[yPos, xPos].hasObj = true;
            fieldLogic.fieldCells[yPos, xPos].colorSprite = currentFigure.figureSprite;
            
            
        }
        currentFigure.gameObject.SetActive(false);
        figureGhost.gameObject.SetActive(false);
        currentFigure = null;
        DelegateController.FigureChanged?.Invoke();
        stepTime = Time.time + firstStepDelay;
    }

    int Wrap(int input, int min, int max)
    {
        if (input < min) 
        {
            return max - (min - input) % (max - min);
        } 
        else 
        {
            return min + (input - min) % (max - min);
        }
    }

    bool TryToMove(Vector3 move, Figure figure)
    {
        for (int i = 0; i < figure.blocks.Length; i++)
        {
            Vector3 posToCheck = figure.blocks[i].transform.position + move;

            if (posToCheck.y < 0 || posToCheck.x < 0 || posToCheck.y >= fieldLogic.fieldCells.GetLength(0) || 
                posToCheck.x >= fieldLogic.fieldCells.GetLength(1) || fieldLogic.fieldCells[(int)posToCheck.y,(int)posToCheck.x].hasObj)
            {
                return false;
            }
        }
        figure.gameObject.transform.position += move;
        return true;
    }
}

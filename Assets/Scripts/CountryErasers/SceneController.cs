using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SceneController : MonoBehaviour
{
    public const int gridRows = 2;
    public const int gridCol = 2;
    public const float offSetX = 4f;
    public const float offSetY = 5f;
    public float openTimer = 3f;
    public Text openTimerText;

    [SerializeField] private MainEraser originalEraser;
    [SerializeField] private Material[] material;
    [SerializeField] private Text gameTimerText;
    public bool gameEnded;
    public float gameTimer = 10f;
    private void Start()
    {
        gameTimerText.gameObject.SetActive(false);
        Vector3 startPos = originalEraser.transform.position;
        int[] numbers = { 0, 0, 1, 1 };
        numbers = ShuffleArray(numbers);

        for (int i  = 0; i < gridCol; i++)
        {
            for (int j = 0;j<gridRows; j++)
            {
                MainEraser eraser;
                if (i == 0 && j == 0)
                {
                    eraser = originalEraser;

                }
                else
                    eraser = Instantiate(originalEraser) as MainEraser;
                int index = j * gridCol + i;
                int id = numbers[index];
                eraser.ChangeSprite(id, material[id]);

                float posX = (offSetX * i) + startPos.x;    
                float posY = (offSetY * j) + startPos.y;

                eraser.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }
    private void Update()
    {
        openTimer -= Time.deltaTime;
        openTimerText.text = "Covering in" + openTimer.ToString("F2");
        if (openTimer <= 0)
        {
            gameTimerText.text = "Time left:" + gameTimer.ToString("F2");
            openTimerText.gameObject.SetActive(false);
            gameTimerText.gameObject.SetActive(true);
            gameTimer -= Time.deltaTime;
            if (gameTimer <=0)
            {
                gameEnded = true;
                gameTimerText.gameObject.SetActive(false);
            }
        }
        
    }
    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for(int i=0;i<newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }

    private MainEraser _firstRevealed;
    private MainEraser _secondRevealed;

    private int _score = 0;
    [SerializeField] private Text scoreLabel;

    public bool canReveal
    { 
        get { return _secondRevealed == null; }
    }

    public void EraserRevealed(MainEraser eraser)
    {
        if (_firstRevealed == null)
        {
            _firstRevealed = eraser;
            print("Reavealed 1");
        }
        else
        {
            _secondRevealed = eraser;
            print("revealed 2");
            StartCoroutine("CheckMatch");
        }
    }

    private IEnumerator CheckMatch()
    {
        Debug.Log("Checking");
        if (_firstRevealed.id == _secondRevealed.id)
        {
            _score++;
            print(_score);
            scoreLabel.text = "Score: " + _score;
        }
        else
        {
            yield return new WaitForSeconds(2f);
            print("wrong");
            _firstRevealed.Cover();
            _secondRevealed.Cover();
        }
        _firstRevealed = null;
        _secondRevealed = null;
        print("Done");
    }
}

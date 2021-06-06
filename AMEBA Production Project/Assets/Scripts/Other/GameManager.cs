using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [Header("Game Manager Fields")]
    public bool inGame = false;
    public GameObject environmentInGame;
    public InGameCanvasManager igcm;

    [Header("Parameters to pass InGame")]
    [HideInInspector] public List<GameObject> enemiesPrefabs;
    [HideInInspector] public List<GameObject> defensesButtonsPrefabs;
    [HideInInspector] public float enemySpawnTime;
    [HideInInspector] public ButtonWorldMap b;
    public Image hintImage;

    [Header("Music Audio Source")]
    public AudioSource musicAudioSource;

    public delegate void OnLevelExited();
    public static event OnLevelExited LevelExited;

    public delegate void OnLevelStart();
    public static event OnLevelStart LevelStart;

    protected override void Awake()
    {
        base.Awake();

        ButtonWorldMap.ButtonWorldMapPressed += ConfiguretInGame;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(.1f);
        SoundManager.PlayMusic(SoundManager.Music.MenuMusic);
    }

    private void ConfiguretInGame()
    {
        inGame = true;
        environmentInGame.SetActive(true);
    }

    public void EndedInGame()
    {
        inGame = false;
        environmentInGame.SetActive(false);
        igcm.ActivateGameMenu();
        SoundManager.PlayMusic(SoundManager.Music.MenuMusic);

        LevelExited?.Invoke();
    }

    public void ConfigureGame(List<GameObject> enemies, List<GameObject> defensesButtons, float enemySpawnTime, Sprite hintImage, ButtonWorldMap b)
    {
        enemiesPrefabs = enemies;
        defensesButtonsPrefabs = defensesButtons;
        this.enemySpawnTime = enemySpawnTime;
        this.hintImage.sprite = hintImage;
        this.b = b;
        inGame = true;
        environmentInGame.SetActive(true);
        SoundManager.PlayMusic(SoundManager.Music.InGameMusic);

        LevelStart();
    }
}

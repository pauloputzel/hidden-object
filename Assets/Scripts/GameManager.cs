using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float musicaVolume;

    public bool mudo = false;

    private AudioListener mAudioListener;

    private AudioSource mAudioSource;

    public void Start()
    {
        if (instance == null)
        {
            mAudioListener = GetComponent<AudioListener>();
            mAudioSource = GetComponent<AudioSource>();
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void setMusicaVolume(float volume)
    {
        musicaVolume = volume;
        mAudioSource.volume = volume;
    }

    public void setMudo(bool mudo)
    {
        this.mudo = mudo;
        mAudioListener.enabled = !mudo;
    }

    public void carregarScene(string nomeScene)
    {
        SceneManager.LoadScene(nomeScene);
    }

    public void sair()
    {
        Application.Quit();
    }
}

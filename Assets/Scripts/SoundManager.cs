using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Xml;

public class SoundManager : MonoBehaviour {


    [SerializeField]
    AudioSource fxAudio;
    [SerializeField]
    AudioSource musicAudio;
    public static SoundManager instance = null;
    private bool bossBattle = false;
    private bool bossBattleCall = false;
    private bool gameplay = false;
    EvoTree<string, ArrayList> musicSoundList;

    ArrayList themeSounds;

    public bool GamePlay
    {
        get
        {
            return gameplay;
        }
        set
        {
            gameplay = value;
        }
    }

    public bool BossBattle
    {
        get
        {
            return bossBattle;
        }
        set
        {
            bossBattle = value;
        }
    }

    public bool BossBattleCall
    {
        get
        {
            return bossBattleCall;
        }
        set
        {
            bossBattleCall = value;
        }
    }

    // Use this for initialization
    void Awake () {

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        musicSoundList = new EvoTree<string, ArrayList>();
        themeSounds = new ArrayList();
        FillAudioClips();
        DontDestroyOnLoad(gameObject);

	}
	
    void playfx(string whoAmI, string type)
    {
        ArrayList aux = (ArrayList)musicSoundList[whoAmI];
        foreach(Dupla d in aux)
        {
            if(d.Type.Equals(type))
            {
                AudioClip fx = d.Sound;
                fxAudio.clip = fx;
                break;
            }
        }
        fxAudio.Play();

    }

    void playmusic(string name)
    {
        if (themeSounds.Contains(name))
        {
            foreach (AudioClip audio in themeSounds)
            {
                if (audio.name.Equals(name))
                {
                    musicAudio.clip = audio;
                    break;
                }
            }
            musicAudio.Play();
        }
        else Debug.Log("Audio inexistente");
        
    }
    void playAudio(AudioClip c)
    {
        fxAudio.clip = c;
        fxAudio.Play();

    }


    void FillAudioClips()
    {
        XmlDocument doc = new XmlDocument();
        try
        {
            doc.Load(Application.dataPath + "/Scripts/music.xml");

        }
        catch (XmlException e)
        {
            throw new XmlException("Fallo en la lectura del fichero: ", e);
        }

        XmlNodeList nodos = doc.GetElementsByTagName("Nodos");

        XmlNodeList list = ((XmlElement)nodos[0]).GetElementsByTagName("fx");

        ArrayList auxiliar = new ArrayList();

        foreach (XmlElement nodo in list)
        {
            //El nombre del audio fx
            string nombre =
                nodo.GetAttribute("nombre");
            //Hit, attack, etc
            string tipo =
                nodo.GetAttribute("tipo");
            //De quien es
            string quien = nodo.GetAttribute("who");

            AudioClip newTrack = Resources.Load("Music/fx/"+nombre, typeof(AudioClip)) as AudioClip;
            Dupla data = new Dupla(tipo,newTrack);

            if (newTrack == null)
                Debug.Log(nombre + " no cargado");
            else
                if(musicSoundList.ContainsKey(quien))
                {
                    auxiliar = (ArrayList) musicSoundList[quien];
                }
            if(newTrack != null)
            {
                auxiliar.Add(data);
                Debug.Log(data.Sound);
                musicSoundList.Add(quien, auxiliar);
            }
            


        }

        XmlNodeList musicList = ((XmlElement)nodos[0]).GetElementsByTagName("music");
        Debug.Log(musicList.Count+ " elementos");
        foreach (XmlElement nodo in musicList)
        {
            //El nombre del theme audio 
            string nombre =
                nodo.GetAttribute("nombre");
            Debug.Log("entro aqui");
            AudioClip newTrack = Resources.Load("Music/"+nombre, typeof(AudioClip)) as AudioClip;
            if(newTrack != null)
            {
                themeSounds.Add(newTrack);
                Debug.Log(newTrack);
            }
           

        }
    }


   /* void switchMusic()
    {
        if(SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByName("MainMenu")))
        {
            musicAudio.clip = Resources.Load("Music/Musica Menu", typeof(AudioClip)) as AudioClip;
            musicAudio.Play();
        }
        if(bossBattle)
        {
            musicAudio.clip = Resources.Load("Music/Boss Battle", typeof(AudioClip)) as AudioClip;
            musicAudio.Play();
        }
        
    }
    */
    // Update is called once per frame
    void Update () {
        
        if (!SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByName("ScenesTest")))
        {   
            if(!gameplay)
            {
                gameplay = true;
                musicAudio.Stop();
                foreach (AudioClip audio in themeSounds)
                {
                    if (audio.name.Equals("Musica Menu"))
                    {

                        musicAudio.clip = audio;
                        musicAudio.Play();
                        break;
                    }

                }
            }
            if(bossBattleCall)
            {
                bossBattleCall = false;
                musicAudio.Stop();
                foreach (AudioClip audio in themeSounds)
                {
                    if (audio.name.Equals("Musica Menu"))
                    {

                        musicAudio.clip = audio;
                        musicAudio.Play();
                        break;
                    }

                }
            }
            if(bossBattle)
            {
                bossBattle = false;
                musicAudio.Stop();
                foreach (AudioClip audio in themeSounds)
                {
                    if (audio.name.Equals("Musica Menu"))
                    {

                        musicAudio.clip = audio;
                        musicAudio.Play();
                        break;
                    }

                }
            }
            
            
        }
        
    }
}

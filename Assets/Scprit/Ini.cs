using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Ini : MonoBehaviour {
    
    Dictionary<int, List<Sprite>> keyValuePairsOfDescriptionImage = new Dictionary<int, List<Sprite>>();

    List<Sprite> tempSprite = new List<Sprite>();

    List<Sprite> title = new List<Sprite>();

    private ReadJson readJson;

    private CameraMovement_hemisphere cameraMovement_Hemisphere;

    private GetUDPMessage getUDPMessage;

    private SendUPDData sendUPDData;

    private MeshVideo meshVideo;

    private CanvasCtr canvasCtr;

    private MapAnimation mapAnimation;

    public List<Info> infos = new List<Info>();
    // Use this for initialization
    void Awake () {
        StartCoroutine(initialization());
	}

   public IEnumerator initialization() {
        Cursor.visible = false;

        Screen.SetResolution(1920, 1200, false);

        readJson = FindObjectOfType<ReadJson>();

        cameraMovement_Hemisphere = FindObjectOfType<CameraMovement_hemisphere>();

        sendUPDData = FindObjectOfType<SendUPDData>();

        getUDPMessage = FindObjectOfType<GetUDPMessage>();

        meshVideo = FindObjectOfType<MeshVideo>();

        mapAnimation = FindObjectOfType<MapAnimation>();

        canvasCtr = FindObjectOfType<CanvasCtr>();

        yield return StartCoroutine(readJson.initialization());

        yield return StartCoroutine(cameraMovement_Hemisphere.initialization());

        meshVideo.initialization();

        getUDPMessage.InitializationUdp();

        sendUPDData.initialization();

        canvasCtr.initialization();

        mapAnimation.initialization();

        yield return StartCoroutine(LoadImage());

        CreateInfoObject();

    }


    IEnumerator LoadImage() {

        yield return StartCoroutine(GetSpriteListFromStreamAsset("/UITexture/TitleTextures/", "jpg",title));

        for (int i = 0; i < ValueSheet.videoPath.Length; i++)
        {    

            string path = "/UITexture/DescriptionTextures/" + i.ToString() + "/";

            yield return StartCoroutine(GetSpriteListFromStreamAsset(path, "jpg", tempSprite));

            Sprite[] Sprite = new Sprite[tempSprite.Count];

            tempSprite.CopyTo(Sprite);

            keyValuePairsOfDescriptionImage.Add(i, Sprite.ToList());

            tempSprite.Clear();
        }

    }


    void CreateInfoObject() {
        for (int i = 0; i < ValueSheet.videoPath.Length; i++)
        {
            string videoPath = ValueSheet.videoPath[i];

            Sprite titleSprite = title[i];

            List<Sprite> DescriptionImage = keyValuePairsOfDescriptionImage[i];

            Sprite MenuImage = canvasCtr.videoLobbyCtr.SpritesListData[i];
    
            infos.Add(new Info(i.ToString(), videoPath, titleSprite, DescriptionImage, MenuImage));
        }
    }


    IEnumerator  GetSpriteListFromStreamAsset(string path, string suffix, List<Sprite> sprites)
    {
        List<Texture> texturesList = new List<Texture>();
        string streamingPath = Application.streamingAssetsPath + path;
        DirectoryInfo dir = new DirectoryInfo(streamingPath);

        GetAllFiles(dir, path, suffix);

        foreach (string de in jpgName)
        {
            WWW www = new WWW(Application.streamingAssetsPath + path + de);
            yield return www;
            if (www != null)
            {
                Texture texture = www.texture;
                texture.name = de;
                texturesList.Add(texture);
            }
            if (www.isDone)
            {
                www.Dispose();
            }
        }

        foreach (var texture in texturesList)
        {
            Sprite sprite = Sprite.Create(texture as Texture2D, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            sprite.name = texture.name;
            sprites.Add(sprite);
        }

        jpgName.Clear();
    }

    List<string> jpgName = new List<string>();

    public void GetAllFiles(DirectoryInfo dir, string Filepath, string suffix)
    {
        FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();   //初始化一个FileSystemInfo类型的实例
        foreach (FileSystemInfo i in fileinfo)              //循环遍历fileinfo下的所有内容
        {
            if (i is DirectoryInfo)             //当在DirectoryInfo中存在i时
            {
                GetAllFiles((DirectoryInfo)i, Filepath, suffix);  //获取i下的所有文件
            }
            else
            {
                string str = i.FullName;        //记录i的绝对路径
                string path = Application.streamingAssetsPath + Filepath;
                string strType = str.Substring(path.Length);

                if (strType.Substring(strType.Length - 3).ToLower() == suffix)
                {
                    if (jpgName.Contains(strType))
                    {
                    }
                    else
                    {
                        jpgName.Add(strType);
                    }
                }
            }
        }
    }
}



[System.Serializable]
public class Info {
   public string id;

   public  string VideoPath;

   public Sprite TitleSprite;

   public List<Sprite> DescriptionImage = new List<Sprite>();

    public Sprite MenuImage;

   public Info() { }

  public  Info(string _id, string _VideoPath, Sprite _TitleSprite, List<Sprite> _DescriptionImage, Sprite _MenuImage) {
        id = _id;

        VideoPath = _VideoPath;

        TitleSprite = _TitleSprite;

        DescriptionImage = _DescriptionImage;

        MenuImage = _MenuImage;
    }
}

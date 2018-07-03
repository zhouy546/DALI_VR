using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;
using UnityEngine.Jobs;

public class MeshAnimationECS : MonoBehaviour
{
    [SerializeField] private int m_FrameRate = 25;                  // The number of times per second the image should change.
    [SerializeField] private MeshRenderer m_ScreenMesh;             // The mesh renderer who's texture will be changed.
    [SerializeField] private Texture[] m_AnimTextures;              // The textures that will be looped through.
    public static List<Texture> texturesList;
    private WaitForSeconds m_FrameRateWait;                         // The delay between frames.
    private int m_CurrentTextureIndex;                              // The index of the textures array.
    private bool m_Playing;                                         // Whether the textures are currently being looped through.

    LoadJob loadJob;
    JobHandle LoadJobHandle;

    struct LoadJob : IJobParallelFor
    {
        public NativeArray<float> JpgName;

        public void Execute(int index)
        {
      //      StartCoroutine(loadPice(index));
        }

        IEnumerator loadPice(int i)
        {

            WWW www = new WWW(Application.streamingAssetsPath + "/Seq/c/" + JpgName[i]);
            Debug.Log(www);

            yield return www;
            if (www != null)
            {
                texturesList.Add(www.texture);
                //              startTime = (double)Time.time - startTime;
            }
            if (www.isDone)
            {
                www.Dispose();
            }

        }
    }

 

    void Awake()
    {

        m_FrameRateWait = new WaitForSeconds(1f / m_FrameRate);

        string streamingPath = Application.streamingAssetsPath + "/Seq/c/";
        DirectoryInfo dir = new DirectoryInfo(streamingPath);//初始化一个DirectoryInfo类的对象
        GetAllFiles(dir);

        loadJob = new LoadJob();
        //loadJob.JpgName = jpgName.ToArray();


        //LoadJobHandle = loadJob.Schedule();


    }

    private void Start()
    {
        LoadJobHandle.Complete();
        //m_AnimTextures= loadJob.TexturesList.ToArray();
    }


    public void PlayVideo()
    {
        m_Playing = true;
        StartCoroutine(PlayTextures());
    }

    private void StopVideo()
    {
        // When the user looks away from the VRInteractiveItem the textures should no longer be playing.
        m_Playing = false;
    }


    private IEnumerator PlayTextures()
    {
        // So long as the textures should be playing...
        while (m_Playing)
        {
            // Set the texture of the mesh renderer to the texture indicated by the index of the textures array.
            m_ScreenMesh.material.mainTexture = m_AnimTextures[m_CurrentTextureIndex];

            // Then increment the texture index (looping once it reaches the length of the textures array.
            m_CurrentTextureIndex = (m_CurrentTextureIndex + 1) % m_AnimTextures.Length;

            // Wait for the next frame.
            yield return m_FrameRateWait;
        }
    }
    IEnumerator  initialization()
    {
        // The delay between frames is the number of seconds (one) divided by the number of frames that should play during those seconds (frame rate).
        m_FrameRateWait = new WaitForSeconds(1f / m_FrameRate);
        yield return StartCoroutine(LoadSeq());
        m_AnimTextures = texturesList.ToArray();
        PlayVideo();
    }

    IEnumerator LoadSeq()
    {

        string streamingPath = Application.streamingAssetsPath + "/Seq/c/";
        DirectoryInfo dir = new DirectoryInfo(streamingPath);//初始化一个DirectoryInfo类的对象
        GetAllFiles(dir);
        double startTime = (double)Time.time;
        foreach (string de in jpgName)
        {
            WWW www = new WWW(Application.streamingAssetsPath + "/Seq/c/" + de);
            yield return www;
            if (www != null)
            {
                texturesList.Add(www.texture);
                startTime = (double)Time.time - startTime;
            }
            if (www.isDone)
            {
                www.Dispose();
            }
        }

        Debug.Log("WWW use time: " + startTime + "   pictures count: " + texturesList.Count);

    }


    List<string> jpgName = new List<string>();
    public void GetAllFiles(DirectoryInfo dir)
    {
        FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();   //初始化一个FileSystemInfo类型的实例
        foreach (FileSystemInfo i in fileinfo)              //循环遍历fileinfo下的所有内容
        {
            if (i is DirectoryInfo)             //当在DirectoryInfo中存在i时
            {
                GetAllFiles((DirectoryInfo)i);  //获取i下的所有文件
            }
            else
            {
                string str = i.FullName;        //记录i的绝对路径
                string path = Application.streamingAssetsPath+ "/Seq/c/";
                string strType = str.Substring(path.Length);
 //               Debug.Log(strType);
                if (strType.Substring(strType.Length - 3).ToLower() == "jpg")
                {
                    
                    if (jpgName.Contains(strType))
                    {
                    }
                    else
                    {
                         // Debug.Log(strType);
                        jpgName.Add(strType);
                    }
                }
            }
        }
    }
}
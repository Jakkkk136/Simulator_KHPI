using System.IO;
using System.Runtime.InteropServices;
using _Scripts.LevelCreator;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SFB;
using System.Text;
using _Scripts.Controllers;

[RequireComponent(typeof(Button))]
public class CanvasSampleSaveFileText : MonoBehaviour, IPointerDownHandler {

    // Sample text data
    private string _data;
    

#if UNITY_WEBGL && !UNITY_EDITOR
    //
    // WebGL
    //
    [DllImport("__Internal")]
    private static extern void DownloadFile(string gameObjectName, string methodName, string filename, byte[] byteArray, int byteArraySize);

    // Broser plugin should be called in OnPointerDown.
    public void OnPointerDown(PointerEventData eventData) {
        CreatorWindow.Instance.FillInLevelDataToConfig();
        _data = LevelSerializer.SerializeLevel(LevelManager.Instance.levelSo);
        var bytes = Encoding.UTF8.GetBytes(_data);
        DownloadFile(gameObject.name, "OnFileDownload", "level.txt", bytes, bytes.Length);
    }

    // Called from browser
    public void OnFileDownload() {
    }
#else
    //
    // Standalone platforms & editor
    //
    public void OnPointerDown(PointerEventData eventData) { }

    protected void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        CreatorWindow.Instance.FillInLevelDataToConfig();
        _data = LevelSerializer.SerializeLevel(LevelManager.Instance.levelSo);

        var bytes = Encoding.UTF8.GetBytes(_data);
        
        var path = StandaloneFileBrowser.SaveFilePanel("Title", "", "sample", "txt");
        if (!string.IsNullOrEmpty(path)) {
            File.WriteAllBytes(path, bytes);
        }
    }
#endif
}
using System.Text;
using System.Collections;
using System.Linq;
using System.Runtime.InteropServices;
using _Scripts.Controllers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SFB;

[RequireComponent(typeof(Button))]
public class CanvasSampleOpenFileText : MonoBehaviour, IPointerDownHandler
{
    private string loadedString;
    
#if UNITY_WEBGL && !UNITY_EDITOR
    //
    // WebGL
    //
    [DllImport("__Internal")]
    private static extern void UploadFile(string gameObjectName, string methodName, string filter, bool multiple);

    public void OnPointerDown(PointerEventData eventData) {
        UploadFile(gameObject.name, "OnFileUpload", ".txt", false);
    }

    // Called from browser
    public void OnFileUpload(string url) {
        StartCoroutine(OutputRoutine(url));
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

    private void OnClick() {
        var paths = StandaloneFileBrowser.OpenFilePanel("Title", "", "txt", false);
        if (paths.Length > 0) {
            StartCoroutine(OutputRoutine(new System.Uri(paths[0]).AbsoluteUri));
        }
    }
#endif

    private IEnumerator OutputRoutine(string url) {
        var loader = new WWW(url);
        yield return loader;
        byte[] loadedBytes = loader.bytes;

        string levelName = url.Split('/').Last().Split('.').First();
        
        loadedString = Encoding.UTF8.GetString(loadedBytes);
        JsonUtility.FromJsonOverwrite(loadedString, LevelManager.Instance.levelSo);

        LevelManager.Instance.levelSo.levelName = levelName;
        
        OnLevelFileOpened();
    }

    protected virtual void OnLevelFileOpened() { }
}
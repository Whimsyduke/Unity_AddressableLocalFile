using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class TestScript : MonoBehaviour
{
    private enum EnumLanguage
    {
        zhCN,
        enUS,
    }

    private string Msg = "";
    private UnityEngine.Object TempObj;
    Vector3 pos = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        Addressables.LoadAssetsAsync<TextAsset>(new List<string> { "GameStrings", "zhCN" }, null, Addressables.MergeMode.Intersection).Completed += Language_Completed;
        Addressables.LoadAssetsAsync<TextAsset>(new List<string> { "GameStrings", "enUS" }, null, Addressables.MergeMode.Intersection).Completed += Language_Completed;
        Addressables.LoadAssetsAsync<UnityEngine.Object>(new List<string> { "GameObject", "A" }, null, Addressables.MergeMode.Intersection).Completed += Object_Completed;
        Addressables.LoadAssetsAsync<UnityEngine.Object>(new List<string> { "GameObject", "B" }, null, Addressables.MergeMode.Intersection).Completed += Object_Completed;
    }

    private void Language_Completed(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<IList<TextAsset>> obj)
    {
        Msg = obj.Result[0].ToString();
    }


    private void Object_Completed(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<IList<UnityEngine.Object>> obj)
    {
        TempObj = obj.Result[0];
        Instantiate(TempObj, pos, Quaternion.identity);
        pos += Vector3.one;
    }

    private void ChangeLanguage(EnumLanguage languag)
    {
        Addressables.LoadAssetsAsync<TextAsset>(new List<string> { "GameStrings", Enum.GetName(typeof(EnumLanguage), languag) }, null, Addressables.MergeMode.Intersection).Completed += Language_Completed;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        GUI.TextArea(new Rect(10, 10, 522, 522), Msg);
        if (GUI.Button(new Rect(530, 10, 100, 20), "enUS"))
        {
            ChangeLanguage(EnumLanguage.enUS);
        }
        if (GUI.Button(new Rect(530, 35, 100, 20), "zhCN"))
        {
            ChangeLanguage(EnumLanguage.zhCN);
        }
    }
}

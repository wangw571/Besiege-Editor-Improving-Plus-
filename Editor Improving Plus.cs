using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.IO;
using UnityEngine;
using spaar.ModLoader;

namespace Editor_Function_Plus_Mod
{

    public class BesiegeModLoader : Mod
    {
        public override string Name { get { return "Editor_Function_Plus_Mod"; } }
        public override string DisplayName { get { return "Editor Function Plus Mod"; } }
        public override string BesiegeVersion { get { return "v0.23"; } }
        public override string Author { get { return "覅是"; } }
        public override Version Version { get { return new Version("0.02"); } }
        public override bool CanBeUnloaded { get { return true; } }

        public GameObject temp;
        public override void OnLoad()
        {


            GameObject temp = new GameObject();
            temp.AddComponent<Editor_Function_Plus_Mod_Script>();
            GameObject.DontDestroyOnLoad(temp);

        }
        public override void OnUnload()
        {
            GameObject.Destroy(temp);
        }
    }

    public class Editor_Function_Plus_Mod_Script : MonoBehaviour
    {
        private bool ResizeMode = true;
        private int currentFrameCount = 0;
        void Start()
        {
            /*StartCoroutine(AutoSaverCleaner());*/
            Commands.RegisterCommand("SetOffArrowResize", (args, notUses) =>
            {
                if (ResizeMode) { ResizeMode = false; return "The Resize has been turned off."; }
                else { ResizeMode = true; return "The Resize has been turned on."; }

            }, "Set arrow to resize or not.");
          }
            /*IEnumerator AutoSaverCleaner()
        {
                yield return new WaitForSeconds(0.01f);
            
                int tempMaxFrameNumber = 0;
                foreach (GameObject go in FindObjectsOfType<GameObject>())
                {
                    if (go.transform.parent == GameObject.Find("AUTO SAVER").transform)
                    {
                        Debug.Log(go.name + "AAAAA");
                        if (int.Parse(go.name.Split('|')[0].Trim()) < tempMaxFrameNumber) { Debug.Log("Destroyed " + go.name + "\n" + int.Parse(go.name.Split('|')[0].Trim()) + "Trimed"); Destroy(go); }
                        else { tempMaxFrameNumber = int.Parse(go.name.Split('|')[0].Trim()); Debug.Log("Max Frame Count is " + tempMaxFrameNumber); }
                    }
                }

                StartCoroutine(AutoSaverCleaner());
            }*/
        
        void Update()
        {
            currentFrameCount += 1;
            try
            {
                try
                {
                    Game.AddPiece.machineParent.name = currentFrameCount + "|CurrentMachine";
                }
                catch { }
                if (ResizeMode)
                {
                    float dise = Vector3.Distance(GameObject.Find("Main Camera").transform.position, GameObject.Find("ArrowsCenter").transform.position);
                    //Debug.Log(dise);
                    if (dise < 3) { dise = 3; }
                    if (dise > 100) { dise = 100; }
                    GameObject.Find("ArrowsCenter").transform.localScale = new Vector3(dise / 30, dise / 30, dise / 30);
                    GameObject.Find("Main Camera").GetComponent<MouseOrbit>().filmCamSmooth = 999999999;
                }
                
            }
            catch { }
        }
    }
}

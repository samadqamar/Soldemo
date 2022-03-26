using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using FCG;

public class FCityGenerator : EditorWindow
{

    private CityGenerator cityGenerator;

    private bool withDowntownArea = true;
    private float downTownSize = 100;
        
    private bool withSatteliteCity = false;

    private int trafficLightHand = 0;
    private string[] selStrings = { "Right Hand", "Left Hand" };
    private bool japanTrafficLight = false;


    [MenuItem("Window/Fantastic City Generator")]
    static void Init()
    {

        FCityGenerator window = (FCityGenerator)EditorWindow.GetWindow(typeof(FCityGenerator));

        window.Show();

    }

    int enableUpdate = 0;

#if UNITY_EDITOR
    void Update()
    {

        if (enableUpdate == 0) return;

        enableUpdate++;

        if (enableUpdate <= 5)
            HideLadders();

        if (enableUpdate >= 5)
            enableUpdate = 0;

    }
#endif

    public void LoadAssets(bool force = false)
    {
        cityGenerator = null;

        if (!cityGenerator)
            cityGenerator = (CityGenerator)AssetDatabase.LoadAssetAtPath("Assets/Fantastic City Generator/Generate.prefab", (typeof(CityGenerator)));

        if (!cityGenerator)
        {
            Debug.LogError("Generate.prefab was not found/Loaded in 'Assets/Fantastic City Generator'");
            return;
        }
        
        string[] s;

        //BB - Street buildings in suburban areas (not in the corner)
        s = System.IO.Directory.GetFiles("Assets/Fantastic City Generator/Buildings/Prefabs/BB", "*.prefab");
        if (force || cityGenerator.BB == null || cityGenerator.BB.Length != s.Length)
            cityGenerator.BB = LoadAssets_sub(s);

        //BC - Down Town Buildings(Not in the corner)
        s = System.IO.Directory.GetFiles("Assets/Fantastic City Generator/Buildings/Prefabs/BC", "*.prefab");
        if (force || cityGenerator.BC == null || cityGenerator.BC.Length != s.Length)
            cityGenerator.BC = LoadAssets_sub(s);

        //BK - Buildings that occupy an entire block
        s = System.IO.Directory.GetFiles("Assets/Fantastic City Generator/Buildings/Prefabs/BK", "*.prefab");
        if (force || cityGenerator.BK == null || cityGenerator.BK.Length != s.Length)
            cityGenerator.BK = LoadAssets_sub(s);

        //BR - Residential buildings in suburban areas (not in the corner)
        s = System.IO.Directory.GetFiles("Assets/Fantastic City Generator/Buildings/Prefabs/BR", "*.prefab");
        if (force || cityGenerator.BR == null || cityGenerator.BR.Length != s.Length)
            cityGenerator.BR = LoadAssets_sub(s);

        //DC - Corner buildings that occupy both sides of the block
        s = System.IO.Directory.GetFiles("Assets/Fantastic City Generator/Buildings/Prefabs/DC", "*.prefab");
        if (force || cityGenerator.DC == null || cityGenerator.DC.Length != s.Length)
            cityGenerator.DC = LoadAssets_sub(s);

        //EB - Corner buildings in suburban areas
        s = System.IO.Directory.GetFiles("Assets/Fantastic City Generator/Buildings/Prefabs/EB", "*.prefab");
        if (force || cityGenerator.EB == null || cityGenerator.EB.Length != s.Length)
            cityGenerator.EB = LoadAssets_sub(s);

        //EC - Down Town Corner Buildings 
        s = System.IO.Directory.GetFiles("Assets/Fantastic City Generator/Buildings/Prefabs/EC", "*.prefab");
        if (force || cityGenerator.EC == null || cityGenerator.EC.Length != s.Length)
            cityGenerator.EC = LoadAssets_sub(s);

        //MB - Buildings that occupy both sides of the block
        s = System.IO.Directory.GetFiles("Assets/Fantastic City Generator/Buildings/Prefabs/MB", "*.prefab");
        if (force || cityGenerator.MB == null || cityGenerator.MB.Length != s.Length)
            cityGenerator.MB = LoadAssets_sub(s);

        //SB - Large buildings that occupy larger blocks
        s = System.IO.Directory.GetFiles("Assets/Fantastic City Generator/Buildings/Prefabs/SB", "*.prefab");
        if (force || cityGenerator.SB == null || cityGenerator.SB.Length != s.Length)
            cityGenerator.SB = LoadAssets_sub(s);

        //BBS - Buildings on slopes (neighborhood)
        s = System.IO.Directory.GetFiles("Assets/Fantastic City Generator/Buildings/Prefabs/BBS", "*.prefab");
        if (force || cityGenerator.BBS == null || cityGenerator.BBS.Length != s.Length)
            cityGenerator.BBS = LoadAssets_sub(s);

        //BCS - Down Town Buildings on slopes
        s = System.IO.Directory.GetFiles("Assets/Fantastic City Generator/Buildings/Prefabs/BCS", "*.prefab");
        if (force || cityGenerator.BCS == null || cityGenerator.BCS.Length != s.Length)
            cityGenerator.BCS = LoadAssets_sub(s);

    }



    private GameObject[] LoadAssets_sub(string[] s)
    {

        int i = s.Length;
        GameObject[] g = new GameObject[i];

        for (int h = 0; h < i; h++)
            g[h] = AssetDatabase.LoadAssetAtPath(s[h], typeof(GameObject)) as GameObject;

        if (g == null)
            Debug.LogError("Error in LoadAssets");

        return g;

    }

    private void GenerateCity(int size)
    {

        LoadAssets();

        cityGenerator.GenerateCity(size, withSatteliteCity);

        if (trafficSystem)
        {
            InverseCarDirection((trafficLightHand == 1 && japanTrafficLight) ? 2 : trafficLightHand);

            trafficSystem.UpdateAllWayPoints();

        }


        DestroyImmediate(GameObject.Find("CarContainer"));


    }



    public void HideLadders()
    {

        RaycastHit hit;

        GameObject[] tempArray = GameObject.FindObjectsOfType(typeof(GameObject)).Select(g => g as GameObject).Where(g => g.name == "RayCast-HideLadder").ToArray();
        foreach (GameObject ray in tempArray)
        {

            if (Physics.Raycast(ray.transform.position, ray.transform.forward, out hit, 1.5f))
                ray.transform.GetChild(0).gameObject.SetActive(false);
            else
                ray.transform.GetChild(0).gameObject.SetActive(true);

        }


    }


    void OnGUI()
    {

        GUILayout.Space(10);

        GUILayout.Label("Fantastic City Generator", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();

        if (!cityGenerator)
            cityGenerator = (CityGenerator)AssetDatabase.LoadAssetAtPath("Assets/Fantastic City Generator/Generate.prefab", (typeof(CityGenerator)));

        if (!cityGenerator)
            Debug.LogError("Generate.prefab was not found in 'Assets/Fantastic City Generator'");

        EditorGUILayout.EndHorizontal();

        GUILayout.Space(5);

        GUILayout.BeginVertical("box");

        GUILayout.Space(5);
        GUILayout.Label(new GUIContent("Generate Streets", "Make City"));

        GUILayout.Space(5);

        GUILayout.BeginHorizontal("box");

        if (GUILayout.Button("Small"))
            GenerateCity(1);


        if (GUILayout.Button("Medium"))
            GenerateCity(2);

        if (GUILayout.Button("Large"))
            GenerateCity(3);

        if (GUILayout.Button("Very Large"))
            GenerateCity(4);


        GUILayout.Space(5);


        GUILayout.EndHorizontal();


        withSatteliteCity = GUILayout.Toggle(withSatteliteCity, "With Sattelite City?", GUILayout.Width(240));

        GUILayout.Space(10);


        if (GUILayout.Button("Clear Streets "))
        {
            cityGenerator.ClearCity();
        }

        GUILayout.Space(10);

        GUILayout.EndVertical();

        GUILayout.Space(10);



        GUILayout.BeginVertical("box");

        GUILayout.Space(5);

        GUILayout.Label(new GUIContent("Buildings", "Make or Clear Buildings"));

        GUILayout.Space(5);

        GUILayout.BeginHorizontal("box");


        GUILayout.Space(5);

        if (GUILayout.Button("Generate Buildings"))
        {
            if (!GameObject.Find("Marcador")) return;

            LoadAssets(true);

            cityGenerator.GenerateAllBuildings(withDowntownArea, downTownSize);
            enableUpdate = 1;



        }


        if (GUILayout.Button("Clear Buildings"))
        {
            if (!GameObject.Find("Marcador")) return;
            cityGenerator.DestroyBuildings();
            //DestroyImmediate(GameObject.Find("CarContainer"));
        }






        GUILayout.EndHorizontal();

        withDowntownArea = GUILayout.Toggle(withDowntownArea, "With Downtown Area?", GUILayout.Width(240));

        if (withDowntownArea)
        {
            GUILayout.Space(10);
            GUILayout.Label(new GUIContent("DownTown Size:", "DownTown Size"));
            downTownSize = EditorGUILayout.Slider(downTownSize, 50, 200);
            GUILayout.Space(10);
        }

        GUILayout.EndVertical();




        GUILayout.Space(10);



        GUILayout.BeginVertical("box");

        GUILayout.Space(5);

        GUILayout.Label(new GUIContent("Traffic System", "Make or Clear Traffic System"));

        GUILayout.Space(5);


        GUILayout.BeginHorizontal("box");

        GUILayout.Space(5);

        if (GUILayout.Button("Add Traffic System"))
        {
            AddVehicles(trafficLightHand);
        }


        if (GUILayout.Button("Remove Traffic System"))
        {

            DestroyImmediate(GameObject.FindObjectOfType<TrafficSystem>().gameObject);
            DestroyImmediate(GameObject.Find("CarContainer"));
        }

        GUILayout.Space(5);

        GUILayout.EndHorizontal();

        GUILayout.Space(5);


        GUILayout.Space(5);

        GUILayout.BeginVertical("box");
        GUILayout.Label(new GUIContent("Traffic Hand", "Hand Right/Left"));
        int rh = trafficLightHand;
        trafficLightHand = GUILayout.SelectionGrid(trafficLightHand, selStrings, 2);
        GUILayout.EndVertical();

        bool japanTL = japanTrafficLight;

        if (trafficLightHand != 0)
        {
            japanTrafficLight = GUILayout.Toggle(japanTrafficLight, "Japan Traffic Light (blue)", GUILayout.Width(240));
        }


        if (rh != trafficLightHand || japanTL != japanTrafficLight)
        {
            rh = trafficLightHand;
            japanTL = japanTrafficLight;

            if (GameObject.Find("CarContainer"))
                AddVehicles((trafficLightHand == 1 && japanTrafficLight) ? 2 : trafficLightHand);
            else
                InverseCarDirection((trafficLightHand == 1 && japanTrafficLight) ? 2 : trafficLightHand);

        }


        GUILayout.EndVertical();


        GUILayout.Space(10);



    }


    private TrafficSystem trafficSystem;

    private void AddVehicles(int right_Hand = 0)
    {

        trafficSystem = FindObjectOfType<TrafficSystem>();

        if (!trafficSystem)
        {
            Instantiate((GameObject)AssetDatabase.LoadAssetAtPath("Assets/Fantastic City Generator/Traffic System/Traffic System.prefab", (typeof(GameObject))));
            trafficSystem = FindObjectOfType<TrafficSystem>();

        }

        if (!trafficSystem)
        {
            Debug.LogError("Add the Traffic System.prefab to Hierarchy");
            return;
        }
        else trafficSystem.name = "Traffic System";

        if (trafficSystem)
        {
            DestroyImmediate(GameObject.Find("CarContainer"));
            trafficSystem.LoadCars(right_Hand);
        }
    }

    private void InverseCarDirection(int trafficHand)
    {

        if (FindObjectOfType<TrafficSystem>())
            trafficSystem = FindObjectOfType<TrafficSystem>();

        if (!trafficSystem)
        {
            //Instantiate((GameObject)AssetDatabase.LoadAssetAtPath("Assets/Fantastic City Generator/Traffic System/Traffic System.prefab", (typeof(GameObject))));
            trafficSystem = AssetDatabase.LoadAssetAtPath("Assets/Fantastic City Generator/Traffic System/Traffic System.prefab", (typeof(TrafficSystem))) as TrafficSystem;
        }

        if (!trafficSystem)
        {
            Debug.LogError("Not Found System.prefab");
            return;
        }

        trafficSystem.DeffineDirection(trafficHand);

        if (GameObject.Find("CarContainer"))
            AddVehicles( (trafficLightHand == 1 && japanTrafficLight) ? 2 : trafficLightHand);

    }



}
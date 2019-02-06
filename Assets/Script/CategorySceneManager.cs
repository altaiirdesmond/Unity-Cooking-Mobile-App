﻿using Assets.Script.DatabaseModel;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CategorySceneManager : MonoBehaviour {

    [SerializeField] private GameObject result;
    [SerializeField] private GameObject[] panels;
    [SerializeField] private GameObject[] buttons;

    public GameObject Result {
        get {
            return result;
        }

        set {
            result = value;
        }
    }

    public GameObject[] Panels {
        get {
            return panels;
        }

        set {
            panels = value;
        }
    }

    public GameObject[] Buttons {
        get {
            return buttons;
        }

        set {
            buttons = value;
        }
    }

    private void Update() {
        
    }
}

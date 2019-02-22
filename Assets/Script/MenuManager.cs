﻿using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Assets.Script.DatabaseModel;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuManager : MonoBehaviour {

    /// <summary>
    /// Reference of Ingredients scene for food ingredients
    /// </summary>
    public static string FoodIngredients { get; set; }

    /// <summary>
    /// Reference of Cooking scene for Food instance
    /// </summary>
    public static Food Food { get; set; }

    public static bool IsPaused { get; set; }

    public void Exit() {
        Application.Quit();
    }

    public void GoToCooking() {
        SceneManager.LoadScene("Cooking");
    }

    public void GotoCategories() {
        SceneManager.LoadScene("Categories");
    }

    public void GoToHome() {
        SceneManager.LoadScene("Home");
    }

    public void GoToInstruction(GameObject gameObject) {
        // Query food ingredients before proceeding
        DatabaseManager databaseManager = new DatabaseManager();

        foreach (var item in databaseManager.GetFood(gameObject.GetComponent<TextMeshProUGUI>().text)) {
            // Get food instance for Cooking and Ingredients scene
            Food = item;
        }

        SceneManager.LoadScene("Ingredients");
    }

    public void PopulateCategoryResult(string category) {
        CategorySceneManager dataObserver = GameObject.Find("SceneManager").GetComponent<CategorySceneManager>();
        dataObserver.Buttons[0].SetActive(true);
        dataObserver.Buttons[1].SetActive(false);
        dataObserver.Panels[5].SetActive(true);
        dataObserver.Panels[4].SetActive(false);

        DatabaseManager databaseManager = new DatabaseManager();
        int i = 0;
        // Fetch data from database
        foreach (var item in databaseManager.GetFoodsByCategory(category)) {
            dataObserver.Result.GetComponentInChildren<TextMeshProUGUI>().text = item.FoodName;
            // Instantiate first before setting the image
            Instantiate(dataObserver.Result, dataObserver.Panels[5].transform);
            // Reference for database image(blob) file
            Texture2D texture2D = new Texture2D(2, 2);
            // Load retrieved image(byte)
            texture2D.LoadImage(item.Image);
            // Set image as sprite for each prefab. Canvas/CategoryResult_Panel/Result/
            GameObject.Find("Canvas/CategoryResult_Panel").transform.GetChild(i++).GetChild(0).GetChild(0).GetComponent<Image>().sprite = 
                Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2());
        }
    }

    public void ShowSearch() {
        GameObject.Find("SceneManager").GetComponent<CategorySceneManager>().Panels[1].GetComponent<UIAnimation>().Animator.SetBool("show", true);
        GameObject.Find("SceneManager").GetComponent<CategorySceneManager>().Panels[6].SetActive(true);

        DatabaseManager databaseManager = new DatabaseManager();

        // resultPrefab will hold the image and text of a food instance
        CategorySceneManager resultPrefab = GameObject.Find("SceneManager").GetComponent<CategorySceneManager>();

        int i = 0;
        // Fetch data from database
        foreach (var item in databaseManager.GetFoods()) {
            resultPrefab.Result.GetComponentInChildren<TextMeshProUGUI>().text = item.FoodName;
            // Instantiate first before setting the image
            Instantiate(resultPrefab.Result, resultPrefab.Panels[0].transform);
            // Reference for database image(blob) file
            Texture2D texture2D = new Texture2D(2, 2);
            // Load retrieved image(byte)
            texture2D.LoadImage(item.Image);
            // Set image as sprite for each prefab. 
            resultPrefab.Panels[0].transform.GetChild(i++).GetChild(0).GetChild(0).GetComponent<Image>().sprite =
                Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2());
        }
    }

    public void HideSearch() {
        GameObject.Find("SceneManager").GetComponent<CategorySceneManager>().Panels[1].GetComponent<UIAnimation>().Animator.SetBool("show", false);
        GameObject.Find("SceneManager").GetComponent<CategorySceneManager>().Panels[6].SetActive(false);
    }

    public void ShowOptions() {
        GameObject.Find("SceneManager").GetComponent<CategorySceneManager>().Panels[2].GetComponent<UIAnimation>().Animator.SetBool("show", true);
    }

    public void HideOptions() {
        GameObject.Find("SceneManager").GetComponent<CategorySceneManager>().Panels[2].GetComponent<UIAnimation>().Animator.SetBool("show", false);
    }

    public void ShowQuit() {
        GameObject.Find("SceneManager").GetComponent<CategorySceneManager>().Panels[4].GetComponent<UIAnimation>().Animator.SetBool("show", true);
    }

    public void HideQuit() {
        GameObject.Find("SceneManager").GetComponent<CategorySceneManager>().Panels[4].GetComponent<UIAnimation>().Animator.SetBool("show", false);
    }

    public void ShowBlockingPanel() {
        GameObject.Find("SceneManager").GetComponent<CategorySceneManager>().Panels[3].SetActive(true);

        // If we are at Cooking scene
        //if (SceneManager.GetActiveScene().buildIndex == 3) {
        //    GameObject.Find("Canvas/TimerText").GetComponent<Timer>().Start = false;
        //}
    }

    public void HideBlockingPanel() {
        GameObject.Find("SceneManager").GetComponent<CategorySceneManager>().Panels[3].SetActive(false);

        // If we are at Cooking scene
        //if (SceneManager.GetActiveScene().buildIndex == 3) {
        //    GameObject.Find("Canvas/TimerText").GetComponent<Timer>().Start = true;
        //}
    }
}
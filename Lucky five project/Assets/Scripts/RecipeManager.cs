using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeManager : MonoBehaviour
{
    [Header("Scroll view")]
    [SerializeField] private Transform scrollViewContent;

    [Header("Recipe document")]
    [SerializeField] private string filePath;
    [SerializeField] private string myDocument;

    [Header("Preparation")]
    [SerializeField] private GameObject sectionPrefab;
    [SerializeField] private GameObject ingredientsPrefab;
    [SerializeField] private GameObject recipePrefab;

    // Initialize document path
    void Start()
    {
        filePath = Application.dataPath + "/Recipes/" + myDocument + ".txt";
    }

    // Read and print the recipe
    public void CheckRecipe()
    {
        // Translate txt file into string array
        string[] lines = File.ReadAllLines(filePath);

        // Delete previous recipe
        while(scrollViewContent.childCount > 0)
            DestroyImmediate(scrollViewContent.GetChild(0).gameObject);

        // Print every line of recipe and formatting
        foreach (string line in lines)
        {
            // Check how the line is to properly format and print the recipe
            if (line == "Ingredients" || line.StartsWith("For") || line == "Method")
            {
                //Instantiate title into scene
                sectionPrefab.GetComponent<TextMeshProUGUI>().text = line;
                Instantiate(sectionPrefab, scrollViewContent);
            } else if (line.StartsWith("-"))
            {
                //Instantiate ingredient into scene
                ingredientsPrefab.GetComponentInChildren<TextMeshProUGUI>().text = line.Replace("-", string.Empty);
                Instantiate(ingredientsPrefab, scrollViewContent);
            } else
            {
                // Subdivide the recipe method into step
                if (line.StartsWith("Step"))
                {
                    //Instantiate subtitle into scene
                    recipePrefab.GetComponent<TextMeshProUGUI>().text = "<size=40><color=#3C3C3C>   " + line + "</color></size>";
                    Instantiate(recipePrefab, scrollViewContent);
                } else
                {
                    //Instantiate body into scene
                    recipePrefab.GetComponent<TextMeshProUGUI>().text = line;
                    Instantiate(recipePrefab, scrollViewContent);
                }
            }
        }
    }
}

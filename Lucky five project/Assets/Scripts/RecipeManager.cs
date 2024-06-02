using System.IO;
using TMPro;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    [Header("Scroll view")]
    [SerializeField] private Transform scrollViewContent;
    [SerializeField] private TMP_Text recipeTitle;

    [Header("Preparation")]
    [SerializeField] private GameObject sectionPrefab;
    [SerializeField] private GameObject ingredientsPrefab;
    [SerializeField] private GameObject recipePrefab;

    // Read and print the recipe
    public void CheckRecipe(string myDocument)
    {
        // Initialize document's path
        string filePath = Application.dataPath + "/Recipes/" + myDocument + ".txt";

        // Set title of the recipe
        recipeTitle.text = myDocument;
        // Translate txt file into string array
        string[] lines = File.ReadAllLines(filePath);

        // Delete previous recipe
        while(scrollViewContent.childCount > 0)
            DestroyImmediate(scrollViewContent.GetChild(0).gameObject);
        
        // Clear previous body
        recipePrefab.GetComponent<TextMeshProUGUI>().text = "";

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
                    //Set body text
                    recipePrefab.GetComponent<TextMeshProUGUI>().text += line + "\n";
            }
        }
        // Instantiate body into scene
        GameObject myRecipe = Instantiate(recipePrefab, scrollViewContent);
    }
}

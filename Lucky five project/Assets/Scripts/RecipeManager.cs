using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeManager : MonoBehaviour
{
    [Header("Scroll view")]
    [SerializeField] private Transform scrollViewContent;
    [SerializeField] private TMP_Text recipeTitle;

    [Header("Preparation")]
    [SerializeField] private GameObject sectionPrefab;
    [SerializeField] private GameObject ingredientsPrefab;
    [SerializeField] private GameObject recipePrefab;

    [Header("Shopping list")]
    [SerializeField] private Transform shoppingListContent;
    [SerializeField] private GameObject productPrefab;
    [SerializeField] private ProductsScriptableObject productsData;

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
                GameObject ingredient = Instantiate(ingredientsPrefab, scrollViewContent);
                
                // Check if ingredient is already in shopping list
                if (IsInShoppingList(line.Replace("-", string.Empty)))
                {
                    // Set ingredient as already owned
                    ingredient.GetComponent<Toggle>().isOn = true;
                }

                // Delegate toggle behaviour to ChangeShoppingList script
                ingredient.GetComponent<Toggle>().onValueChanged.AddListener(delegate {ChangeShoppingList();});
            } else
            {
                    //Set body text
                    recipePrefab.GetComponent<TextMeshProUGUI>().text += line + "\n";
            }
        }
        // Instantiate body into scene
        GameObject myRecipe = Instantiate(recipePrefab, scrollViewContent);
    }

    // Update shopping list
    public void ChangeShoppingList()
    {
        // Check if toggle is on
        if (this.GetComponent<Toggle>().isOn && IsInShoppingList(this.GetComponentInChildren<TextMeshProUGUI>().text))
        {
            // Deactivate toggle
            this.GetComponent<Toggle>().isOn = false;

            // Look for product to eliminate in shopping list
            for (int i = 0; i < shoppingListContent.childCount; i++)
            {
                if (shoppingListContent.GetChild(i).name == this.GetComponentInChildren<TextMeshProUGUI>().text)
                {
                    // Reset product's data and destroy it
                    productsData.totalPrice[i] = productsData.prices[i];
                    productsData.amount[i] = 0;
                    DestroyImmediate(shoppingListContent.GetChild(i));
                }
            }
        } else
        {
            // Activate toggle
            this.GetComponent<Toggle>().isOn = true;

            // Look for product from data
            for (int i = 0; i < productsData.names.Length; i++)
            {
                if (productsData.names[i] == this.GetComponentInChildren<TextMeshProUGUI>().text)
                {
                    // Add product into shopping list
                    GameObject product = Instantiate(productPrefab, shoppingListContent);
                    product.name = this.GetComponentInChildren<TextMeshProUGUI>().text;
                    
                    // Adjust its data
                    if (product.TryGetComponent<ScrollViewItem>(out ScrollViewItem item))
                    {
                        item.ChangeData(productsData.sprites[i], product.name, productsData.prices[i], ++productsData.amount[i]);
                    }
                }
            }
        }
    }

    // Check if in shopping list
    public bool IsInShoppingList(string productName)
    {
        for (int i = 0; i < shoppingListContent.childCount; i++)
        {
            if (shoppingListContent.GetChild(i).name.ToUpper() == productName.ToUpper())
            {
                return true;
            }
        }
        return false;
    }
}

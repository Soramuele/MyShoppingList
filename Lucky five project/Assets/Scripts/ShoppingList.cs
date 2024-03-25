using System.Collections.Generic;
using UnityEngine;

public class ShoppingList : MonoBehaviour
{
    private Dictionary<int, string> products;

    /// Awake is called when the script instance is being loaded
    void Awake()
    {
        products = new Dictionary<int, string>() {
            {00, "Apple Bandit"},   {01, "Banana"},
            {02, "Beer"},           {03, "Cabbage"},
            {04, "Chips"},          {05, "Cookie"},
            {06, "Ground meat"},    {07, "Tofu"},
            {08, "Yogurt"},         {09, "Tomatoes"},
            {10, "Milk"},           {11, "Bread"},
            {12, "Flour"},          {13, "Yeast"},
            {14, "Tomato sauce"},   {15, "Pasta"},
            {16, "Spaghetti"},      {17, "Cheese"},
            {18, "Mushrooms"},      {19, "Eggs"},
            {20, "Mozzarella"},     {21, "Spinach"},
            {22, "Cooking cream"},  {23, "Fresh cream"},
            {24, "Whipped cream"},  {25, "Onions"},
            {26, "Garlic"},         {27, "Butter"}
        };
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

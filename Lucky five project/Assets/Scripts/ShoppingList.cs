using UnityEngine;
using TMPro;

public class ShoppingList : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputProducts;
    private string[] products;

    [SerializeField] private string[] recipeBook =  {
                                                        "eggs,butter,muffin mix" ,
                                                        "pasta,tomato sauce,bacon,spices",
                                                        "flour,yeast",
                                                        "onions,carrots,ground meat,tomato sauce"
                                                    };

    public void AnAbnormalCheckingList()
    {
        string[] check;
        int i;
        products = _inputProducts.text.Split(',');

        for(i = 0; i < recipeBook.Length; i++)
        {
            check = recipeBook[i].Split(',');

            foreach (var key in products)
                foreach (var prod in check)
                    if (key == prod)
                    {
                        for (int j = 0; j < check.Length; j++)
                            Debug.Log("Recipe with:" + check[j]);
                    }
        }
    }
}

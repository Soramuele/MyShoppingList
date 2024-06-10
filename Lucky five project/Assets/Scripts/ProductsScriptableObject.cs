using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Products Data", fileName = "Products Data")]
public class ProductsScriptableObject : ScriptableObject
{
    // Product data
    [Header("Products data")]
    public Sprite[] sprites;
    public string[] names;
    public float[] prices;
    public float[] totalPrice;
    public int[] amount;

    // Recipe data
    [Header("Recipes data")]
    public Sprite[] recipeImage;
    public string[] recipeName;
    public int[] recipeTime;
}

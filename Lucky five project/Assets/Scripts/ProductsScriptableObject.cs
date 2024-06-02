using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Products Data", fileName = "Products Data")]
public class ProductsScriptableObject : ScriptableObject
{
    // Product data
    public Sprite[] sprites;
    public string[] names;
    public float[] prices;
    public float[] totalPrice;
    public int[] amount;
}

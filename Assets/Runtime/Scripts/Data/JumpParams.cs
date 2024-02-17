using UnityEngine;



[CreateAssetMenu(menuName = "ScriptableObject/JumpParams")]
public class JumpParams : ScriptableObject
{
    public float jumpDistanceZ = 5;
    public float jumpHeightY = 2;
    public float jumpLerpSpeed = 10;

}

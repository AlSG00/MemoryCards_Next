using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "New card data")]
public class CardData : ScriptableObject
{
    public enum Type
    { 
        StandartDot,
        StandartStick,
        StandartCircle,
        StandartTriangle,
        StandartSquare,
        StandartCross,
        StandartStar,
        StandartSnake,
        StandartSymbol,
        StandartEarth,
        StandartSun,
        StandartDark,
        StandartMoon, 
        StandartEye,
        StandartDevil,
        StandartGod,
        StandartBeast,
        DarkEye,
        DarkGod,
        DarkSun,
        DarkStick
    }

    public Type type;
    public Sprite[] cardSprite;
    public int scoreValue;
    public bool isDarkCard;
    public bool isStrangeCard;
}

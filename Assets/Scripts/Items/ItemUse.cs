using UnityEngine;

public class ItemUse : MonoBehaviour
{
    public int ID;
    public float healthToGive;
    public float manaToGive;
    public float weapondamageModifier;

    public ItemType itemType;
    public void Use()
    {
        PlayerHealth.instance.currentHealth += healthToGive;

        if (itemType == ItemType.WEAPON)
        {

            if (transform.GetChild(1).gameObject.activeSelf)
            {
                PlayerStats.instance.weaponStat.GetComponent<WeaponStat>().weaponAttack = 0;
                PlayerStats.instance.weaponStat.GetComponent<WeaponStat>().weaponAttack = weapondamageModifier;
            }
            else
            {
                PlayerStats.instance.weaponStat.GetComponent<WeaponStat>().weaponAttack = 0;
            }
            
        }
    }
}

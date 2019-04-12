using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public int equipped = 0;

    // Start is called before the first frame update
    void Start()
    {
        Equip();
    }

    // Update is called once per frame
    void Update()
    {
        int previouslyEquipped = equipped;

        // scroll up to get next weapon
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (equipped >= transform.childCount - 1)
            {
                equipped = 0;
            }
            else
            {
                equipped++;
            }
        }
        // scroll down to get previous weapon
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (equipped <= 0)
            {
                equipped = transform.childCount - 1;
            }
            else
            {
                equipped--;
            }
        }

        if (previouslyEquipped != equipped)
        {
            Equip();
        }
    }

    void Equip()
    {
        //enable current weapon and disable others
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == equipped)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}

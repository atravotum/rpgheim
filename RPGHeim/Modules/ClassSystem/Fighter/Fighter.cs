using UnityEngine;

namespace RPGHeim
{

    internal class RPGHeimFighterClass : MonoBehaviour
    {
        public static void InitializePlayer(Player player)
        {
            // create a new action bar for the fighter skills
            ActionBar newActionBar = new ActionBar();
            newActionBar.CreateSlot();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPGHeim.Modules.StatusEffects
{
    public class SE_Slow : SE_Stats
    {
        // Token: 0x0600000F RID: 15 RVA: 0x00002A7C File Offset: 0x00000C7C
        public SE_Slow()
        {
            base.name = "SE_VL_Slow";
            this.m_tooltip = "Slow";
            this.m_name = "Slow";
            this.m_ttl = SE_Slow.m_baseTTL;
        }

        // Token: 0x06000010 RID: 16 RVA: 0x00002AEA File Offset: 0x00000CEA
        public override void ModifySpeed(ref float speed)
        {
            speed *= this.speedAmount;
            base.ModifySpeed(ref speed);
        }

        // Token: 0x06000011 RID: 17 RVA: 0x00002B00 File Offset: 0x00000D00
        public override bool CanAdd(Character character)
        {
            return true;
        }


        // Token: 0x0400000D RID: 13
        public static GameObject GO_SEFX;

        // Token: 0x0400000E RID: 14
        [Header("SE_VL_Slow")]
        public static float m_baseTTL = 4f;

        // Token: 0x0400000F RID: 15
        public float speedDuration = 3f;

        // Token: 0x04000010 RID: 16
        public float speedAmount = 0.4f;

        // Token: 0x04000011 RID: 17
        private float m_timer = 1f;
    }
}

using RPGHeim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RPGHeim.Managers
{
    public static class ProjectileManager
    {
        public static List<PrefabToLoad<bool>> ProjectilesPrefabs { get; set; } = new List<PrefabToLoad<bool>>();

        public static int ProjectileIndex { get; set; }

        public static void Register(PrefabToLoad<bool> prefab)
        {
            if (prefab.IsProjectile)
            {
                ProjectilesPrefabs.Add(prefab);
            }

            if (prefab.ProjectileEnum != Managers.ProjectileManager.RPGHeimProjectile.None)
            {
                ProjectileManager.ProjectilesByEnum.Add(prefab.ProjectileEnum, prefab);
            }

            if (prefab.AuraEnum != Managers.ProjectileManager.RPGHeimAura.None)
            {
                ProjectileManager.AurasByEnum.Add(prefab.AuraEnum, prefab);
            }

            if (prefab.ExplosionEnum != Managers.ProjectileManager.RPGHeimExplosion.None)
            {
                ProjectileManager.ExplosionsByEnum.Add(prefab.ExplosionEnum, prefab);
            }

            if (prefab.IsProjectile)
            {
                Jotunn.Logger.LogInfo($"Projectile Added! - {prefab.LoadedPrefab.name}");
                ProjectilesPrefabs.Add(prefab);
            }
        }

        // Prob redudant -- Unless we have specific explosions != Projectile couter parts.
        public enum RPGHeimExplosion
        {
            None = -1,

            // --
            MagicMissile = 0,

            // Dark
            Darkbolt,
            Darkblast,

            // Fire
            Firebolt,
            Fireball,
            Magmablast,
            Firetornado,

            // Water
            Waterblast,
            Iceblast,
            Icewave,

            // Lightning
            Lightningblast,

            // Poison
            Poisonbolt,
        }

        public enum RPGHeimProjectile
        {
            None = -1,

            // --
            MagicMissile = 0,

            // Dark
            Darkbolt,
            Darkblast,

            // Fire
            Firebolt,
            Fireball,
            Magmablast,
            Firetornado,

            // Water
            Waterblast,
            Iceblast,
            Icewave,

            // Lightning
            Lightningblast,

            // Poison
            Poisonbolt,
        }

        public enum RPGHeimAura
        {
            None = -1,

            Cleanse,
            FireAura,
            HealingAura,

        }

        public static Dictionary<RPGHeimProjectile, PrefabToLoad<bool>> ProjectilesByEnum = new Dictionary<RPGHeimProjectile, PrefabToLoad<bool>>();
        public static Dictionary<RPGHeimExplosion, PrefabToLoad<bool>> ExplosionsByEnum = new Dictionary<RPGHeimExplosion, PrefabToLoad<bool>>();
        public static Dictionary<RPGHeimAura, PrefabToLoad<bool>> AurasByEnum = new Dictionary<RPGHeimAura, PrefabToLoad<bool>>();

        public static void NextProjectile()
        {
            ProjectileIndex++;
            if (ProjectileIndex >= ProjectilesPrefabs.Count)
            {
                ProjectileIndex = 0;
            }
        }

        public static void LastProjectile()
        {
            ProjectileIndex--;
            if (ProjectileIndex < 0)
            {
                ProjectileIndex = ProjectilesPrefabs.Count - 1;
            }
        }

        public static void SetProjectileAt(int index)
        {
            if (index > 0 && index < ProjectilesPrefabs.Count)
            {
                ProjectileIndex = index;
            }
        }

        public static PrefabToLoad<bool> GetProjectileAtIndex()
        {
            if (ProjectileManager.ProjectilesPrefabs.Count > 0)
            {
                return ProjectilesPrefabs[ProjectileIndex];
            }
            return null;
        }

        public static PrefabToLoad<bool> GetProjectile(RPGHeimProjectile projectiles)
        {
            return ProjectilesByEnum[projectiles];
        }
    }
}

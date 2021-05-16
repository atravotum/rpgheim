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
            if(index > 0 && index < ProjectilesPrefabs.Count)
            {
                ProjectileIndex = index;
            }
        }

        public static PrefabToLoad<bool> GetProjectile()
        {
            if (ProjectileManager.ProjectilesPrefabs.Count > 0)
            {
                return ProjectilesPrefabs[ProjectileIndex];
            }
            return null;
        }
    }
}

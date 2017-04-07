using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace FS_Hack
{
    public class Loading : MonoBehaviour
    {
        private static GameObject hookObject = null;

        private static void runHack()
        {
            if (hookObject == null)
            {
                hookObject = new GameObject();
                hookObject.AddComponent<FS_Hack.Menu>();
                DontDestroyOnLoad(hookObject);
            }
        }
    }
}

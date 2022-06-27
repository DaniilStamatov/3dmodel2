using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Components.Utils
{
    public class AudioUtils
    {
        public const string SfxSource = "SfxAudioSource";

        public static AudioSource FindSfxSource()
        {
            return GameObject.FindWithTag(SfxSource).GetComponent<AudioSource>();
        }
    }
}

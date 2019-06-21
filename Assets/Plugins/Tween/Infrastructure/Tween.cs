using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TweenService
{
    public class Tween
    {
        private static TweenProcessor processor;
        private static TweenProcessor Processor 
        { 
            get 
            { 
                if(processor == null)
                {
                    processor = new TweenProcessor();
                }

                return processor;
            } 
        }

        public static void TweenVector4(ITweenable processor, Vector4 startPoint, Vector4 endPoint, float time, float delay, AnimationCurve curve)
        {
            Processor.Process(processor, startPoint, endPoint, time, delay, curve);
        }
    }
}


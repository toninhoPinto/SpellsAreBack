using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PDollarGestureRecognizer;

public class SpellRecognizer : MonoBehaviour {


	// Use this for initialization
	void Start () {
    }
	
	public string RecognizeSpell(List<Gesture> gestures)
    {
        for (int i = 0; i < gestures.Count; i++) {
            if (gestures[i].Name.Equals("square"))
            {
            }
            if (gestures[i].Name.Equals("triangle"))
            {
            }
            if (gestures[i].Name.Equals("circle"))
            {
            }
            if (gestures[i].Name.Equals("semicircle"))
            {
            }
            if (gestures[i].Name.Equals("fire"))
            {
            }
            if (gestures[i].Name.Equals("rock"))
            {
            }
            if (gestures[i].Name.Equals("water"))
            {
            }
            if (gestures[i].Name.Equals("wind"))
            {
            }
            if (gestures[i].Name.Equals("lightning"))
            {
            }
            if (gestures[i].Name.Equals("poison"))
            {
            }
            if (gestures[i].Name.Equals("arcane"))
            {
            }
            if (gestures[i].Name.Equals("death"))
            {
            }
        }
        return "";
    } 



}

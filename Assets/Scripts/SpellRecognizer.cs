using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PDollarGestureRecognizer;

public class SpellRecognizer : MonoBehaviour {

    List<SpellRecipe> SavedRecipes;

	// Use this for initialization
	void Start () {
        SavedRecipes = new List<SpellRecipe>();
    }
	
	public string RecognizeSpell(List<Gesture> gestures)
    {
        for(int i = 0; i < SavedRecipes.Count; i++)
        {
            if (SavedRecipes[i].gestures.Count != gestures.Count) //tem o mesmo numero de gestos
                break;

            //cada gesto tem o mesmo numero de aparencias
            for(int j = 0; j < SavedRecipes[i].gestures.Count; j++)
            {

            }

            //distancia entre gestos, gestos dentro de gestos, e outros
        }

        return "";
    } 



}

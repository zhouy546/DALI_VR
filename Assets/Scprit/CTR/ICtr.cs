using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICtr : MonoBehaviour {
    public List<NImage> AllNimages = new List<NImage>();
    public List<Ntext> Alltexts = new List<Ntext>();


    public  void initialization()
    {
        NImage[] tempImages = GetComponentsInChildren<NImage>();
        AllNimages.AddRange(tempImages);

        Ntext[] temptexts = GetComponentsInChildren<Ntext>();
        Alltexts.AddRange(temptexts);
    }

    public void ShowAll(float time = 1)
    {
        foreach (var item in AllNimages)
        {
            //item.ShowAll(time);
            item.ChangeColor(item.PervioudColor, 1f);
        }


        foreach (var item in Alltexts)
        {
          //  item.ShowAll(time);
            item.ChangeColor(item.DefaultColor, 1f);
        }
    }

    public void HideAll(float time = 1)
    {
        foreach (var item in AllNimages)
        {
            item.HideAll(time);
        }

        foreach (var item in Alltexts)
        {
            item.HideAll(time);
        }
    }

    public void InteractionToggle(bool b) {
        foreach (var item in AllNimages)
        {
            if (item.isInteractionObject) {
                item.setRarCastTarget(b);
            }


        }

        foreach (var item in Alltexts)
        {
            if (item.isInteractionObject) {
                item.setRarCastTarget(b);
            }

        }
    }


}

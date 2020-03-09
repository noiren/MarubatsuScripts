using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckClear : MonoBehaviour
{
    public static int count = 0;
    public static int UNanameCount = 0;
    public static int DNanameCount = 0;

    public static bool CheckStatus(int color)
    {
        for (int i = 0; i < 5; i++)//横を全部見る
        {
            for (int j = 0; j < 5; j++)
            {
                if (GameManager.instance.position[i, j] == GameManager.EMPTY || GameManager.instance.position[i, j] != color)
                {
                    count = 0;

                }
                else
                {
                    count++;
                }

                if (count == 3)
                {
                    if (color == GameManager.WHITE)
                    {
                        Debug.Log("白横おｋ");

                    }
                    else
                    {
                        Debug.Log("黒横おｋ");
                    }
                    return true;
                }
            }
        }


        count = 0;


        for (int i = 0; i < 5; i++)//縦を全部見る
        {
            for (int j = 0; j < 5; j++)
            {


                if (GameManager.instance.position[j, i] == GameManager.EMPTY || GameManager.instance.position[j, i] != color)
                {

                    count = 0;
                }
                else
                {

                    count++;
                }


                if (count == 3)
                {

                    if (color == GameManager.WHITE)
                    {
                        Debug.Log("白縦おｋ");
                    }
                    else
                    {
                        Debug.Log("黒縦おｋ");
                    }


                    return true;
                }
            }
        }


        if (GameManager.instance.position[0, 0] == color && GameManager.instance.position[1, 1] == color && GameManager.instance.position[2, 2] == color)
        {
            return true;
        }
        if (GameManager.instance.position[0, 1] == color && GameManager.instance.position[1, 2] == color && GameManager.instance.position[2, 3] == color)
        {
            return true;
        }
        if (GameManager.instance.position[0, 2] == color && GameManager.instance.position[1, 3] == color && GameManager.instance.position[2, 4] == color)
        {
            return true;
        }
        if (GameManager.instance.position[1, 0] == color && GameManager.instance.position[2, 1] == color && GameManager.instance.position[3, 2] == color)
        {
            return true;
        }
        if (GameManager.instance.position[1, 1] == color && GameManager.instance.position[2, 2] == color && GameManager.instance.position[3, 3] == color)
        {
            return true;
        }
        if (GameManager.instance.position[1, 2] == color && GameManager.instance.position[2, 3] == color && GameManager.instance.position[3, 4] == color)
        {
            return true;
        }
        if (GameManager.instance.position[2, 0] == color && GameManager.instance.position[3, 1] == color && GameManager.instance.position[4, 2] == color)
        {
            return true;
        }
        if (GameManager.instance.position[2, 1] == color && GameManager.instance.position[3, 2] == color && GameManager.instance.position[4, 3] == color)
        {
            return true;
        }
        if (GameManager.instance.position[2, 2] == color && GameManager.instance.position[3, 3] == color && GameManager.instance.position[4, 4] == color)
        {
            return true;
        }


        //こっから斜め下精査
        if (GameManager.instance.position[4, 0] == color && GameManager.instance.position[3, 1] == color && GameManager.instance.position[2, 2] == color)
        {
            return true;
        }
        if (GameManager.instance.position[4, 1] == color && GameManager.instance.position[3, 2] == color && GameManager.instance.position[2, 3] == color)
        {
            return true;
        }
        if (GameManager.instance.position[4, 2] == color && GameManager.instance.position[3, 3] == color && GameManager.instance.position[2, 4] == color)
        {
            return true;
        }

        if (GameManager.instance.position[3, 0] == color && GameManager.instance.position[2, 1] == color && GameManager.instance.position[1, 2] == color)
        {
            return true;
        }
        if (GameManager.instance.position[3, 1] == color && GameManager.instance.position[2, 2] == color && GameManager.instance.position[1, 3] == color)
        {
            return true;
        }
        if (GameManager.instance.position[3, 2] == color && GameManager.instance.position[2, 3] == color && GameManager.instance.position[1, 4] == color)
        {
            return true;
        }

        if (GameManager.instance.position[2, 0] == color && GameManager.instance.position[1, 1] == color && GameManager.instance.position[0, 2] == color)
        {
            return true;
        }
        if (GameManager.instance.position[2, 1] == color && GameManager.instance.position[1, 2] == color && GameManager.instance.position[0, 3] == color)
        {
            return true;
        }
        if (GameManager.instance.position[2, 2] == color && GameManager.instance.position[1, 3] == color && GameManager.instance.position[0, 4] == color)
        {
            return true;
        }
        return false;
    }
    }


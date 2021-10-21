using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [System.Serializable]
    public struct ParaLevel
    {
        public Transform layerConteiner;
        public Transform[] layerPart;
        public Vector3 speedFactor;
        public float halfOfDisBetweenNeighbor;
        public int currentPart;
    }

    public cam_smooth CM;
    public ParaLevel[] paraLevel;

    void LateUpdate()
    {
        for (int i = 1; i < paraLevel.Length; i++)
        {
            paraLevel[i].layerConteiner.transform.localPosition -= Vector3.Scale(CM.shift, paraLevel[i].speedFactor);

            if (paraLevel[i].layerPart[paraLevel[i].currentPart].transform.position.x < Camera.main.transform.position.x - paraLevel[i].halfOfDisBetweenNeighbor)
            {
                int left = (paraLevel[i].currentPart - 1) < 0 ? (paraLevel[i].layerPart.Length - 1) : (paraLevel[i].currentPart - 1);
                int right = (paraLevel[i].currentPart + 1) >= paraLevel[i].layerPart.Length ? 0 : (paraLevel[i].currentPart + 1);
                paraLevel[i].layerPart[left].transform.position = new Vector2(paraLevel[i].layerPart[right].transform.position.x + paraLevel[i].halfOfDisBetweenNeighbor * 2, paraLevel[i].layerPart[right].transform.position.y);
                paraLevel[i].currentPart = right;
                continue;
                
            }
            if (paraLevel[i].layerPart[paraLevel[i].currentPart].transform.position.x > Camera.main.transform.position.x + paraLevel[i].halfOfDisBetweenNeighbor)
            {
                int left = (paraLevel[i].currentPart - 1) < 0 ? (paraLevel[i].layerPart.Length - 1) : (paraLevel[i].currentPart - 1);
                int right = (paraLevel[i].currentPart + 1) >= paraLevel[i].layerPart.Length ? 0 : (paraLevel[i].currentPart + 1);
                paraLevel[i].layerPart[right].transform.position = new Vector2(paraLevel[i].layerPart[left].transform.position.x - paraLevel[i].halfOfDisBetweenNeighbor * 2, paraLevel[i].layerPart[left].transform.position.y);
                paraLevel[i].currentPart = left;
                continue;
            }
               
        }
        
    }

}

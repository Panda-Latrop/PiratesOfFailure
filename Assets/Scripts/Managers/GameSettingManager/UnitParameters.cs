using UnityEngine;

public class UnitParameters : MonoBehaviour 
{
    [System.Serializable]
    public struct StandardUnitParameters
    {
        public float
            speed,
            jumpHeightMax,
            jumpHeightMin,
            horizJumpAcc,
            minCommDisMax,
            minCommDisMin;
    }
    public StandardUnitParameters standardUnitParameters;
}

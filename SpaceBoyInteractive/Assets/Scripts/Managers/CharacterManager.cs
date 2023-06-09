using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HomeomorphicGames
{
    public class CharacterManager : AbstractManager
    {
        [SerializeField] private SpaceBoiChar boiChar;

        public SpaceBoiChar BoiChar { get { return boiChar; } }

        public void MoveTo(PointOfInterest poi)
        {
            BoiChar.MoveTo(poi.InterestPosition());
        }

        public float DistanceFromCamera()
        {
            return Vector3.Distance(boiChar.GetDofTargetPos(), Camera.main.transform.position);
        }
    }
}

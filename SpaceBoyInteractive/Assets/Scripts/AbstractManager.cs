using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HomeomorphicGames.Sny.Core
{
    public abstract class AbstractManager : MonoBehaviour 
    {
        private bool _isBusy;
        public bool IsBusy { get { return _isBusy; } protected set { _isBusy = value; } }

    }
}

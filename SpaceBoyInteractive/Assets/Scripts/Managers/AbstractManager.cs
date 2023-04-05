using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace HomeomorphicGames
{
    public abstract class AbstractManager : MonoBehaviour 
    {
        private bool _isBusy;
        public bool IsBusy { get { return _isBusy; } protected set { _isBusy = value; } }
        public GameManager Manager { get { return GameManager.Instance; } } 

        public virtual async Task Prepare()
        {
            await Task.Yield();
        }

    }
}

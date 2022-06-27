using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Components.Utils.Disposables
{
    public class CompositeDisposable : IDisposable
    {
        private readonly List<IDisposable> _disposables = new List <IDisposable>();


        //Add event to the list of disposables
        public void Retain(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }

        //Clear the list of disposables
        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
            _disposables.Clear();
        }
    }
}

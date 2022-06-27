using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Assets.Scripts.UI.Widgets
{
    public class DataGroup<TDataType , TItemType> where TItemType : MonoBehaviour, ItemRenderer<TDataType>
    {
        private readonly List<TItemType> _createdItems = new List<TItemType>();

        private readonly TItemType _prefab;
        private readonly Transform _container;

        public DataGroup(TItemType prefab, Transform container)
        {
            _prefab = prefab;
            _container = container;
        }
        public void SetData(IList<TDataType> data)
        {
            //load prefabs to the container
            for (var i = _createdItems.Count; i < data.Count(); i++)
            {
                var item = UnityEngine.Object.Instantiate(_prefab, _container);
                _createdItems.Add(item);
            }
            //load items to the inventory
            for (var i = 0; i < data.Count; i++)
            {
                _createdItems[i].SetData(data[i], i);
                _createdItems[i].gameObject.SetActive(true);
            }
            //disactivate gameObjects > inventory
            for (var i = data.Count; i < _createdItems.Count; i--)
            {
                _createdItems[i].gameObject.SetActive(false);
            }
        }

    }

    public interface ItemRenderer<TDataType>
    {
        void SetData(TDataType data, int index);
    }
}

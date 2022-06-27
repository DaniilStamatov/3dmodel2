using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Windows;

namespace Assets.Scripts.Components.Model.Definitions.Localization
{
    [CreateAssetMenu(menuName = "Defs/LocaleDef", fileName = "LocaleDef")]
    public class LocaleDef : ScriptableObject
    {
        [SerializeField] private string _url;
        [SerializeField] private List<LocaleItem> _localeItems;

        private UnityWebRequest _request;

        public Dictionary<string, string> GetData()
        {
            var dictionary = new Dictionary<string, string>();
            foreach (var item in _localeItems)
            {
                dictionary.Add(item.Key, item.Value);
            }
            return dictionary;
        }

        //load from url
        [ContextMenu("UpdateLocale")]

        public void UpdateLocale()
        {
            if (_request != null) return;

            _request = UnityWebRequest.Get(_url);
            _request.SendWebRequest().completed += OnDataLoaded;
        }

        [ContextMenu("Update Locale from file")]

        public void UpdateLocaleFromFile()
        {
            var path = UnityEditor.EditorUtility.OpenFilePanel("Choose locale file", "", "tsv");
            if (path.Length != 0)
            {
                var data = System.IO.File.ReadAllText(path);
                ParseData(data);
            }
        }


        private void OnDataLoaded(AsyncOperation operation)
        {
            if (operation.isDone)
            {
                var data = _request.downloadHandler.text;
                ParseData(data);
            }
        }

        private void ParseData(string data)
        {
            var rows = data.Split('\n');
            _localeItems.Clear();
            foreach (var row in rows)
            {
                AddLocaleItem(row);
            }
        }

        private void AddLocaleItem(string row)
        {
            try
            {
                var parts = row.Split('\t');
                //parse 1st element is key, 2nd is value
                _localeItems.Add(new LocaleItem {Key = parts[0], Value = parts[1]});
            }
            catch (Exception ex)
            {
                Debug.LogError($"Can't parse row{row}.\n{ex}");
            }
        }

        [Serializable]
        private class LocaleItem
        {
            public string Key;
            public string Value;
        }
    }
}

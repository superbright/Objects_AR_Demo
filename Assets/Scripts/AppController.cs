using GoogleARCore.Examples.HelloAR;
using SB.Objects;
using SB.Objects.Model;
using SB.Objects.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppController : MonoBehaviour
{

    [SerializeField]
    HelloARController _ARController;

    [SerializeField]
    ObjectsListController _listItemController;

    [SerializeField]
    ObjectsManager _objectsManager;

    [SerializeField] GameObject _prefabToLoad;
    [SerializeField] BundleData _currentDataModel;

    // Use this for initialization
    void Start()
    {
        _ARController = FindObjectOfType<HelloARController>();
        _listItemController = FindObjectOfType<ObjectsListController>();
        _objectsManager = FindObjectOfType<ObjectsManager>();


        _listItemController.ListItemSelected += (id) =>
        {
            var dataModel = _objectsManager.GetBundleData(id);

            if (dataModel != null)
                _ARController.CurrentDataModel = dataModel;
            else
                Debug.LogError("Can't find datamodel: " + id);
        };

        _listItemController.ClearSceneButtonPressed += () =>
        {
            _ARController.DestroyAllAssets();
        };
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleARKit()
    {
        _ARController.enabled = !_ARController.enabled;
    }

}

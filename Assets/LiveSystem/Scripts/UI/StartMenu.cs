// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace LiveSystem.UI
{
    public class StartMenu : MonoBehaviour
    {
        [SerializeField] private Dropdown liveModeDropdown;
        [SerializeField] private Button startButton;

        private void Start()
        {
            foreach (var item in Enum.GetNames(typeof(LiveMode)))
            {
                liveModeDropdown.options.Add(new Dropdown.OptionData(item));
            }
            startButton.onClick.AddListener(() => { StartCoroutine(LoadSceneAsync(liveModeDropdown.value + 1)); });
        }

        private IEnumerator LoadSceneAsync(int sceneBuildIndex)
        {
            var sceneLoadReq = SceneManager.LoadSceneAsync(sceneBuildIndex);
            yield return new WaitUntil(() => sceneLoadReq.isDone);
        }
    }
}

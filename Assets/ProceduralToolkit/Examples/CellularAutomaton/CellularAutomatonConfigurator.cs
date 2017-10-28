﻿using System.Collections.Generic;
using ProceduralToolkit.Examples.UI;
using UnityEngine;
using UnityEngine.UI;

namespace ProceduralToolkit.Examples
{
    public class CellularAutomatonConfigurator : ConfiguratorBase
    {
        public RectTransform leftPanel;
        public ToggleGroup toggleGroup;
        public RawImage image;
        [Space]
        public CellularAutomaton.Config config = new CellularAutomaton.Config();

        private enum RulesetName
        {
            Life,
            Mazectric,
            Coral,
            WalledCities,
            Coagulations,
            Anneal,
            Majority,
        }

        private Color[] pixels;
        private Texture2D texture;
        private CellularAutomaton automaton;
        private Color deadColor;
        private Color aliveColor;
        private TextControl header;

        private Dictionary<RulesetName, Ruleset> nameToRuleset = new Dictionary<RulesetName, Ruleset>
        {
            {RulesetName.Life, Ruleset.life},
            {RulesetName.Mazectric, Ruleset.mazectric},
            {RulesetName.Coral, Ruleset.coral},
            {RulesetName.WalledCities, Ruleset.walledCities},
            {RulesetName.Coagulations, Ruleset.coagulations},
            {RulesetName.Anneal, Ruleset.anneal},
            {RulesetName.Majority, Ruleset.majority},
        };

        private void Awake()
        {
            pixels = new Color[config.width*config.height];
            texture = new Texture2D(config.width, config.height, TextureFormat.ARGB32, false, true)
            {
                filterMode = FilterMode.Point
            };
            texture.Clear(Color.clear);
            texture.Apply();
            image.texture = texture;

            header = InstantiateControl<TextControl>(leftPanel);
            header.transform.SetAsFirstSibling();

            var currentRulesetName = RulesetName.Life;
            SelectRuleset(currentRulesetName);

            InstantiateToggle(RulesetName.Life, currentRulesetName);
            InstantiateToggle(RulesetName.Mazectric, currentRulesetName);
            InstantiateToggle(RulesetName.Coral, currentRulesetName);
            InstantiateToggle(RulesetName.WalledCities, currentRulesetName);
            InstantiateToggle(RulesetName.Coagulations, currentRulesetName);
            InstantiateToggle(RulesetName.Anneal, currentRulesetName);
            InstantiateToggle(RulesetName.Majority, currentRulesetName);

            InstantiateControl<SliderControl>(leftPanel).Initialize("Start noise", 0, 1, config.startNoise, value =>
            {
                config.startNoise = value;
                Generate();
            });

            InstantiateControl<ToggleControl>(leftPanel).Initialize("Alive borders", config.aliveBorders, value =>
            {
                config.aliveBorders = value;
                Generate();
            });

            InstantiateControl<ButtonControl>(leftPanel).Initialize("Generate", Generate);

            Generate();
            SetupSkyboxAndPalette();
        }

        private void Update()
        {
            automaton.Simulate();
            DrawCells();
            UpdateSkybox();
        }

        private void SelectRuleset(RulesetName rulesetName)
        {
            config.ruleset = nameToRuleset[rulesetName];

            header.Initialize("Rulestring: " + config.ruleset);
        }

        private void Generate()
        {
            automaton = new CellularAutomaton(config);

            GeneratePalette();

            deadColor = GetMainColorHSV().WithSV(0.3f, 0.2f).ToColor();
            aliveColor = GetMainColorHSV().ToColor();
        }

        private void DrawCells()
        {
            for (int x = 0; x < config.width; x++)
            {
                for (int y = 0; y < config.height; y++)
                {
                    if (automaton.cells[x, y] == CellState.Alive)
                    {
                        pixels[y*config.width + x] = aliveColor;
                    }
                    else
                    {
                        pixels[y*config.width + x] = deadColor;
                    }
                }
            }

            texture.SetPixels(pixels);
            texture.Apply();
        }

        private void InstantiateToggle(RulesetName rulesetName, RulesetName selectedRulesetName)
        {
            var toggle = InstantiateControl<ToggleControl>(toggleGroup.transform);
            toggle.Initialize(
                header: rulesetName.ToString(),
                value: rulesetName == selectedRulesetName,
                onValueChanged: isOn =>
                {
                    if (isOn)
                    {
                        SelectRuleset(rulesetName);
                        Generate();
                    }
                },
                toggleGroup: toggleGroup);
        }
    }
}
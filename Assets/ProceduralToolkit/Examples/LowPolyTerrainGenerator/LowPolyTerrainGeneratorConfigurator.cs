﻿using ProceduralToolkit.Examples.UI;
using UnityEngine;

namespace ProceduralToolkit.Examples
{
    public class LowPolyTerrainGeneratorConfigurator : ConfiguratorBase
    {
        public MeshFilter terrainMeshFilter;
        public RectTransform leftPanel;
        public bool constantSeed = false;
        public LowPolyTerrainGenerator.Config config = new LowPolyTerrainGenerator.Config();

        private const int minXSize = 10;
        private const int maxXSize = 30;
        private const int minYSize = 1;
        private const int maxYSize = 5;
        private const int minZSize = 10;
        private const int maxZSize = 30;
        private const float minCellSize = 0.3f;
        private const float maxCellSize = 2;
        private const int minNoiseScale = 1;
        private const int maxNoiseScale = 20;

        private Mesh terrainMesh;

        private void Awake()
        {
            Generate();
            SetupSkyboxAndPalette();

            InstantiateControl<SliderControl>(leftPanel)
                .Initialize("Terrain size X", minXSize, maxXSize, (int) config.terrainSize.x, value =>
                {
                    config.terrainSize.x = value;
                    Generate();
                });

            InstantiateControl<SliderControl>(leftPanel)
                .Initialize("Terrain size Y", minYSize, maxYSize, (int) config.terrainSize.y, value =>
                {
                    config.terrainSize.y = value;
                    Generate();
                });

            InstantiateControl<SliderControl>(leftPanel)
                .Initialize("Terrain size Z", minZSize, maxZSize, (int) config.terrainSize.z, value =>
                {
                    config.terrainSize.z = value;
                    Generate();
                });

            InstantiateControl<SliderControl>(leftPanel)
                .Initialize("Cell size", minCellSize, maxCellSize, config.cellSize, value =>
                {
                    config.cellSize = value;
                    Generate();
                });

            InstantiateControl<SliderControl>(leftPanel)
                .Initialize("Noise scale", minNoiseScale, maxNoiseScale, (int) config.noiseScale, value =>
                {
                    config.noiseScale = value;
                    Generate();
                });

            InstantiateControl<ButtonControl>(leftPanel).Initialize("Generate", () => Generate());
        }

        private void Update()
        {
            UpdateSkybox();
        }

        public void Generate(bool randomizeConfig = true)
        {
            if (constantSeed)
            {
                Random.InitState(0);
            }

            if (randomizeConfig)
            {
                GeneratePalette();

                config.gradient = ColorE.Gradient(from: GetMainColorHSV(), to: GetSecondaryColorHSV());
            }

            var draft = LowPolyTerrainGenerator.TerrainDraft(config);
            draft.Move(Vector3.left*config.terrainSize.x/2 + Vector3.back*config.terrainSize.z/2);
            AssignDraftToMeshFilter(draft, terrainMeshFilter, ref terrainMesh);
        }
    }
}
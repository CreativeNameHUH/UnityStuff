using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Game
{
    public class GetBlock : MonoBehaviour
    {
        public Button generateButton;
        public TMP_Dropdown blocksDropdown;

        public GameObject baseBlock;

        public int numberOfBlocks = 15;
    
        private GameObject[] _blocks;
        private GameObject _baseBlockCopy;

        public Initialize _gameSettings;

        private int _blockIndex;
        private float _maxSize;
        private float _minSize;

        public void Get()
        {
            if (_blockIndex < numberOfBlocks)
            {
                switch (blocksDropdown.value)
                {
                    case 0:
                        GenerateCube();
                        break;
                    case 1:
                        GenerateRectangle();
                        break;
                    case 2:
                        GenerateCylinder();
                        break;
                    case 3:
                        GenerateSphere();
                        break;
                }
            }
            else
            {                
                generateButton.enabled = false;
                generateButton.interactable = false;
                
            }
        }

        public void Clear()
        {
            foreach (GameObject block in _blocks)
            {
                Destroy(block);
            }

            _blockIndex = 0;
            generateButton.enabled = true;
            generateButton.interactable = true;
        }
        
        private void GenerateCube()
        {
            int size = RandomSize();
            GenerateBlock(size);
        }

        private void GenerateRectangle()
        {
            bool isEqual = true;
            int x = RandomSize(), y = RandomSize(), z = RandomSize();

            while (isEqual)
            {
                if (x != y)
                {
                    if (x != z || y != z)
                    {
                        isEqual = false;
                    }
                    else
                    {
                        z = RandomSize();
                    }
                }
                else
                {
                    y = RandomSize();
                }
            }
            
            GenerateBlock(x, y, z);
        }

        private void GenerateCylinder()
        {
            int x = RandomSize(), y = RandomSize(), z = RandomSize();
            
            GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            cylinder.AddComponent<Rigidbody>();
            baseBlock = cylinder;

            Destroy(cylinder);
            GenerateBlock(x, y, z);
        }

        private void GenerateSphere()
        {
            int size = RandomSize();

            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.AddComponent<Rigidbody>();
            baseBlock = sphere;
            
            Destroy(sphere);
            GenerateBlock(size);
        }
        
        private void GenerateBlock(int xSize, int ySize, int zSize)
        {
            baseBlock = Instantiate(baseBlock, GameObject.Find("Blocks").transform, true);
            baseBlock.transform.localScale = new Vector3(xSize, ySize, zSize);
            baseBlock.transform.localPosition = RandomPosition();
                
            Renderer blockRenderer = baseBlock.GetComponent<Renderer>();
            blockRenderer.material.SetColor("_Color", GenerateColor());

            Rigidbody rb = baseBlock.GetComponent<Rigidbody>();
            rb.mass = RandomMass();
                
            _blocks[_blockIndex] = baseBlock;
            _blockIndex++;
            baseBlock = _baseBlockCopy;
        }

        private void GenerateBlock(int size)
        {
            GenerateBlock(size, size, size);
        }

        private Color GenerateColor()
        {
            float r = RandomColorValue(), g = RandomColorValue(), b = RandomColorValue();
            return new Color(r, g, b);
        }

        private void RandomNewSeed()
        {
            int randomSeed = Convert.ToInt32(Random.Range(0f, 1000f));
            Random.InitState(randomSeed);
        }

        private int RandomSize()
        {
            return Convert.ToInt32(Random.Range(_minSize, _maxSize));
        }

        private Vector3 RandomPosition()
        {
            float x = Random.Range(-855f, 855f);
            float z = Random.Range(-435f, 435f);

            return new Vector3(x, 210f, z);
        }

        private float RandomMass()
        {
            return Random.Range(100f, 500f);
        }

        private float RandomColorValue()
        {
            return Random.Range(0f, 1f);
        }

        private void Start()
        {
            Random.InitState(69);
            RandomNewSeed();
            _gameSettings = GetComponentInParent<Initialize>();
            _maxSize = _gameSettings.GetSettings.MaxSize;
            _minSize = _gameSettings.GetSettings.MinSize;
            numberOfBlocks = _gameSettings.GetSettings.NumberOfBlocks;
            _baseBlockCopy = baseBlock;
            _blocks = new GameObject[numberOfBlocks];
        }
    }
}

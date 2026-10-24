using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelEditorWindow : EditorWindow
{
    private int _size = 5;
    
    private IntegerField _gridSizeField;
    
    private VisualElement _grid;
    private VisualElement _palette;
    
    private List<BlockData> _blockPalette;
    private BlockData _currentBlock;
    
    private LevelData _levelData;
    
    [MenuItem("Tools/Level Editor")]
    public static void Open()
    {
        GetWindow<LevelEditorWindow>("Level Editor");
    }
    
    public void CreateGUI()
    {
        rootVisualElement.Clear();
        
        //Размер поля
        _gridSizeField = new IntegerField(_size);
        _gridSizeField.RegisterValueChangedCallback(OnGridSizeChanged);

        //Данные уровня
        var levelField = new ObjectField("Level");

        levelField.objectType = typeof(LevelData);
        levelField.RegisterValueChangedCallback(OnLevelDataChanged);
        
        //Палитра
        _palette = new VisualElement();
        
        _palette.style.flexDirection = FlexDirection.Row;
        _palette.style.flexWrap =  Wrap.Wrap;

        var paletteSource = new ObjectField("Palette Source");
        
        paletteSource.objectType = typeof(BlockPalette);
        paletteSource.RegisterValueChangedCallback(OnBlockPaletteChanged);
        
        //Сетка
        _grid = new VisualElement();
        _grid.style.flexDirection = FlexDirection.Row;
        
        _grid.style.marginTop = 20;
        _grid.style.alignSelf = Align.Center; 

        //Добавление элементов
        rootVisualElement.Add(_gridSizeField);
        rootVisualElement.Add(levelField);
        rootVisualElement.Add(paletteSource);
        rootVisualElement.Add(_palette);
        rootVisualElement.Add(_grid);
    }
    
    //Обработка изменения файла данных уровня
    private void OnLevelDataChanged(ChangeEvent<Object> evt)
    {
        if (evt.newValue is LevelData value) _levelData = value;
        else return;
        
        _gridSizeField.value = _levelData.GridSize;
        DrawGrid(_levelData.GridSize);
    }
    
    //Обработка смены размера сетки
    private void OnGridSizeChanged(ChangeEvent<int> evt)
    {
        if(evt.newValue > 0)
        {
            if (_levelData) _levelData.GridSize = evt.newValue;
            DrawGrid(evt.newValue);
        }
    }
    
    //Отрисовка сетки
    private void DrawGrid(int gridSize)
    {
        _grid.Clear();

        //Столбцы
        for (var i = 0; i < gridSize; i++)
        {
            var column = new VisualElement();

            column.style.flexWrap = Wrap.Wrap;
            column.style.flexShrink = 1;

            //Ячейки
            for (var j = gridSize; j > 0; j--)
            {
                var cell = new VisualElement
                {
                    style =
                    {
                        width = 40,
                        height = 40,
                        
                        backgroundColor = new StyleColor(new Color(0.25f, 0.25f, 0.25f)),

                        marginTop = 1,
                        marginBottom = 1,
                        marginRight = 1,
                        marginLeft = 1
                    }
                };
                
                if (_levelData);
                {
                    BlockData blockData = _levelData.GetBlock(i, j - 1);
                    cell.style.backgroundImage = new StyleBackground(blockData.Sprite);
                }

                cell.RegisterCallback<MouseDownEvent, int[]>(OnCellClick, new[] { i + 1, j });

                column.Add(cell);
            }

            _grid.Add(column);
        }
    }
    
    //Обработка смены палитры
    private void OnBlockPaletteChanged(ChangeEvent<Object> evt)
    {
        _palette.Clear();
        _palette.Add(new Label(" Palette: "));

        _blockPalette = evt.newValue is BlockPalette value ? value.Blocks : new List<BlockData>();

        for (int i = 0; i < _blockPalette.Count; i++)
        {
            BlockData block = _blockPalette[i];
            
            var cell = new VisualElement();
                
            cell.style.width = 20;
            cell.style.height = 20;
            
            cell.style.marginRight = 2;
            cell.style.marginLeft = 2;

            cell.style.backgroundImage = new StyleBackground(block.Sprite);
            cell.RegisterCallback<MouseDownEvent>(_ => _currentBlock = block);
            
            _palette.Add(cell);
        }
    }

    //Обработка нажатия на клетку поля в редакторе
    private void OnCellClick(MouseDownEvent evt, int[] coordinates)
    {
        if (_currentBlock is null)
        {
            Debug.Log("Тип блока не выбран");
        }
        else if (evt.target is VisualElement cell)
        {
            cell.style.backgroundImage = new StyleBackground(_currentBlock.Sprite);
            
            _levelData.UpdateBlock(coordinates[0] - 1, coordinates[1] - 1, _currentBlock);
        }
    }
}

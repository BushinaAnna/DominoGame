using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows.Forms;
using DrawingImage = System.Drawing.Image;

namespace Domino
{
    public partial class Form1 : Form
    {
        private Dictionary<string, DrawingImage> imageCache = new Dictionary<string, DrawingImage>();
        private Game game;
        private Tile selectedPlayerTile;
        private bool isAITurnRunning = false;
        public bool IsPlayerFirst { get; set; }
        public bool IsNewGame { get; set; }

        public Form1()
        {
            InitializeComponent();

            this.Load += Form1_Load;            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (IsNewGame)
            {
                StartGame();
            }
        }        
        
        private void UpdateBoard()
        {
            foreach (var image in imageCache.Values)
            {
                image.Dispose();
            }
            imageCache.Clear();

            panelBoard.Refresh();            
        }

        private void UpdatePlayerTiles()
        {
            panelPlayerTiles.Controls.Clear();
            panelPlayerTiles.AutoScroll = true;
            int xPos = 10;

            foreach (var tile in game.humanPlayer.Tiles)
            {
                PictureBox pictureBox = new PictureBox
                {
                    Image = DrawingImage.FromFile(tile.GetImagePath()), // Загружаем изображение
                    Location = new Point(xPos, 10), // Позиция на панели
                    Size = new Size(60, 30), // Размер
                    SizeMode = PictureBoxSizeMode.Zoom, // Подгон изображения
                    Tag = tile // Сохраняем объект Tile для обработки кликов
                };

                pictureBox.Click += TilePictureBox_Click;

                panelPlayerTiles.Controls.Add(pictureBox);

                xPos += 70;
            }
        }

        private void TilePictureBox_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            Tile selectedTile = pictureBox.Tag as Tile;
            selectedPlayerTile = selectedTile;

            UpdatePlayerTiles();

            List<int> positions = GetAvailablePositions(game.Board);
            HighlightAvailablePositions(positions);
        }        

        private void btnRestart_Click(object sender, EventArgs e)
        {
            MainMenuForm mainMenu = new MainMenuForm();
            mainMenu.Show();

            this.Hide();

            btnRestart.Enabled = false;
        }

        private void btnDrawFromBank_Click(object sender, EventArgs e)
        {
            if (!Game.isPlayerTurn)
            {
                lblStatus.Text = "Сейчас ход компьютера";
            }
            else if (!game.DrawTile(1, game.TileBank))
            {
                lblStatus.Text = "Банк пуст. Вы не можете взять фишку.";
            }
            else
            {
                UpdatePlayerTiles();
                lblStatus.Text = "Вы взяли фишку из банка. Теперь ход компьютера";
                Game.isPlayerTurn = false;

                AImove();
                Debug.WriteLine("AImove вызван в Взять из банка");
            }            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var image in imageCache.Values)
            {
                image.Dispose();
            }
            imageCache.Clear();

            if (btnRestart.Enabled)
            {
                Application.Exit();
            }
        }

        private void panelBoard_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.Clear(panelBoard.BackColor);
            
            int xPos = 55;
            int yPos = 315;
            int verticalOffset = 45;

            for (int i = 0; i < game.Board.Count; i++)
            {
                Tile tile = game.Board[i];
                string imagePath = tile.GetImagePath();

                if (!imageCache.TryGetValue(imagePath, out var image))
                {
                    image = DrawingImage.FromFile(imagePath);
                    imageCache[imagePath] = image;
                }

                if (i > 0)
                {
                    if (!tile.IsVertical)
                    {
                        yPos = 315;
                        graphics.DrawImage(image, new Rectangle(xPos, yPos, 40, 20));
                        xPos += 45;
                    }
                    else if (tile.IsDouble)
                    {
                        yPos = 305;
                        graphics.DrawImage(image, new Rectangle(xPos, yPos, 20, 40));
                        xPos += 25;

                        for (int j = i - 1; j > 0; j--)
                        {
                            Tile currentTile = game.Board[j];
                            imagePath = currentTile.GetImagePath();

                            if (!imageCache.TryGetValue(imagePath, out var currentImage))
                            {
                                currentImage = DrawingImage.FromFile(imagePath);
                                imageCache[imagePath] = currentImage;
                            }

                            if (game.Board[j].IsVertical)
                            {                                
                                graphics.DrawImage(currentImage, new Rectangle(xPos - 25, yPos + (i - j) * verticalOffset, 20, 40));
                            }
                            else
                            {
                                break;
                            }
                        }

                        for (int j = i + 1; j < game.Board.Count; j++)
                        {
                            Tile currentTile = game.Board[j];
                            imagePath = currentTile.GetImagePath();

                            if (!imageCache.TryGetValue(imagePath, out var currentImage))
                            {
                                currentImage = DrawingImage.FromFile(imagePath);
                                imageCache[imagePath] = currentImage;
                            }
                            if (game.Board[j].IsVertical)
                            {
                                graphics.DrawImage(currentImage, new Rectangle(xPos - 25, yPos + (i - j) * verticalOffset, 20, 40));
                            }
                            else
                            {
                                break;
                            }
                        }
                    }                     
                }
                else
                {
                    if (tile.IsVertical)
                    {
                        yPos = 305;
                        graphics.DrawImage(image, new Rectangle(xPos, yPos, 20, 40)); 
                        xPos += 25;
                    }
                    else
                    {
                        graphics.DrawImage(image, new Rectangle(xPos, yPos, 40, 20));
                        xPos += 45;
                    }
                }                
            }
        }

        private async void AImove()
        {
            isAITurnRunning = true;

            lblStatus.Text = "Компьютер делает ход";

            await Task.Delay(500);      // Задержка перед ходом компьютера (1 секунда)

            Tile aiMove = game.MakeComputerMove(game.Board);
            
            if (aiMove != null)
            {
                if (aiMove.Side1 == 7 && aiMove.Side2 == 7)
                {
                    Debug.WriteLine("Взятие фишки из банка");
                    lblStatus.Text = "Компьютер взял фишку из банка. Теперь ваш ход.";
                    Game.isPlayerTurn = true;
                    UpdateBoard(); 
                    ShowGameOverMessage();
                    return;
                }

                UpdateBoard(); // Обновляем отображение игрового поля
                lblStatus.Text = "Компьютер сделал ход";
                Game.isPlayerTurn = true;
                ShowGameOverMessage();                
            }            
        }

        public void StartGame()
        {
            game = new Game();
            panelBoard.Paint += panelBoard_Paint;

            if (IsPlayerFirst)
            {
                lblStatus.Text = "Ваш ход.";
                Game.isPlayerTurn = true;
            }
            else
            {
                Game.isPlayerTurn = false;

                if (!isAITurnRunning)
                {
                    AImove();
                    Debug.WriteLine("AImove вызван в StartGame");
                }
            }

            UpdateBoard();
            UpdatePlayerTiles();

            btnRestart.Enabled = true;
        }

        public void ShowGameOverMessage()
        {
            string resultMessage = game.IsGameOver();
            if (resultMessage != null)
            {
                var result = CustomMessageBox.Show(resultMessage);

                Application.Exit();
            }
        }

        public void SaveGame(string filePath)
        {
            GameState state = new GameState
            {
                Board = game.Board.Select(tile => new Tile(tile.Side1, tile.Side2)
                {
                    IsDouble = tile.IsDouble,
                    IsVertical = tile.IsVertical
                }).ToList(),
                HumanPlayerTiles = game.humanPlayer.Tiles.ToList(),
                AIPlayerTiles = game.aiPlayer.Tiles.ToList(),
                IsPlayerTurn = Game.isPlayerTurn
            };

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            }
        }

        public void LoadGame(string filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                using (FileStream stream = new FileStream(filePath, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    GameState state = (GameState)formatter.Deserialize(stream);

                    game = new Game
                    {
                        Board = state.Board.Select(tile => new Tile(tile.Side1, tile.Side2)
                        {
                            IsDouble = tile.IsDouble,
                            IsVertical = tile.IsVertical
                        }).ToList()
                    };
                    // Восстанавливаем состояние игры                    

                    game.humanPlayer.Tiles = state.HumanPlayerTiles.Select(tile => new Tile(tile.Side1, tile.Side2)
                    {
                        IsDouble = tile.IsDouble
                    }).ToList();

                    game.aiPlayer.Tiles = state.AIPlayerTiles.Select(tile => new Tile(tile.Side1, tile.Side2)
                    {
                        IsDouble = tile.IsDouble
                    }).ToList();

                    Game.isPlayerTurn = state.IsPlayerTurn;

                    // Обновляем интерфейс
                    panelBoard.Paint += panelBoard_Paint;
                    UpdateBoard();
                    UpdatePlayerTiles(); // Обновляем фишки игрока
                    lblStatus.Text = Game.isPlayerTurn ? "Ваш ход" : "Ход компьютера";

                    MessageBox.Show("Игра успешно загружена!", "Загрузка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Сохранённый файл не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveGame_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Game Save File|*.sav",
                Title = "Сохранить игру"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveGame(saveFileDialog.FileName);
                MessageBox.Show("Игра сохранена!", "Сохранение");
            }
        }

        public List<int> GetAvailablePositions(List<Tile> board)
        {
            List<int> availablePositions = new List<int>();

            if (board.Count == 0)
            {
                availablePositions.Add(0);
            }
            else
            {
                availablePositions.Add(0);
                availablePositions.Add(board.Count);

                for (int i = 1; i < board.Count - 1; i++)
                {
                    if (board[i].IsDouble)
                    {
                        if (!board[i + 1].IsVertical && !board[i - 1].IsVertical)
                        {
                            availablePositions.Add(i + 1);
                            availablePositions.Add(i);
                        }
                        else
                        {
                            for (int j = i; j < board.Count; j++)
                            {
                                if (!board[j].IsVertical)
                                {
                                    break;
                                }

                                if (board[j].IsVertical && !board[j + 1].IsVertical)
                                {
                                    availablePositions.Add(j + 1);
                                }

                            }
                            for (int j = i; j > 0; j--)
                            {
                                if (!board[j].IsVertical)
                                {
                                    break;
                                }
                                else if (!board[j - 1].IsVertical)
                                {
                                    availablePositions.Add(j);
                                }
                                
                            }
                        }                        
                    }
                }
            }
            Debug.WriteLine("Возможные позиции для хода: " + string.Join(", ", availablePositions));
            return availablePositions;
        }

        private Rectangle GetTileRectangle(int position)
        {
            int tileWidth = 40;  // Ширина фишки
            int tileHeight = 20; // Высота фишки

            int xPos = 55;
            int yPos = 315;

            // Вычисляем координаты в зависимости от позиции
            if (position == 0)
            {
                return new Rectangle(10, yPos, tileWidth, tileHeight);
            }
            else if (position == game.Board.Count)
            {
                xPos += GetBoardWidght(0, game.Board.Count);
                return new Rectangle(xPos, yPos, tileWidth, tileHeight);
            }
            if (position > 0 && position < game.Board.Count - 1)
            {
                if (!game.Board[position].IsVertical)
                {
                    xPos = xPos + GetBoardWidght(0, position) - 25;
                    yPos -= GetBoardHight(position, position, 0);
                }
                else if (!game.Board[position-1].IsVertical)
                {
                    xPos += GetBoardWidght(0, position);
                    yPos += GetBoardHight(position, position, game.Board.Count);
                }

                return new Rectangle(xPos, yPos, tileHeight, tileWidth);
            }

            // Если позиция выходит за пределы доски, возвращаем пустой прямоугольник
            return Rectangle.Empty;
        }

        private int GetBoardWidght(int start, int end)
        {
            int widght = 0;
            for (int i = start; i < end; i++)
            {
                Tile tile = game.Board[i];
                if (tile.IsDouble)
                {
                    widght += 25;
                }
                else if (!tile.IsVertical)
                {
                    widght += 45;
                }
            }
            return widght;
        }

        private int GetBoardHight(int position, int start, int end)
        {
            int hight = 0;
            if(!game.Board[position].IsVertical)
            {
                for (int i = start - 1; i > end; i--)
                {
                    Tile tile = game.Board[i];
                    if (tile.IsVertical)
                    {
                        hight += 45;
                    }

                    if (tile.IsDouble)
                    {
                        hight += 10;
                        break;
                    }
                }
            }
            else
            {
                for (int i = start; i < end; i++)
                {
                    Tile tile = game.Board[i];
                    if (tile.IsVertical)
                    {
                        hight += 45;
                    }

                    if (tile.IsDouble)
                    {
                        hight -= 10;
                        break;
                    }
                }
            }
            
            return hight;
        }

        private void HighlightAvailablePositions(List<int> positions)
        {
            Color customBorderColor = Color.FromArgb(70, 134, 81, 89); // Цвет рамки
            Color customFillColor = Color.FromArgb(70, 248, 240, 227); // Полупрозрачный цвет заливки

            using (Graphics g = panelBoard.CreateGraphics())
            using (Pen customPen = new Pen(customBorderColor, 3.0f)) // Перо для рамки
            using (Brush customBrush = new SolidBrush(customFillColor)) // Кисть для заливки
            {
                foreach (int position in positions)
                {
                    Rectangle rect = GetTileRectangle(position);
                    if (!rect.IsEmpty)
                    {
                        g.DrawRectangle(customPen, rect); // Рисуем рамку указанного цвета
                        g.FillRectangle(customBrush, rect); // Полупрозрачный фон
                    }
                }
            }
        }

        private void PanelBoard_Click(object sender, MouseEventArgs e)
        {
            // Получаем координаты клика
            Point clickPoint = e.Location;

            // Проверяем, попал ли клик в одну из возможных позиций
            foreach (int position in GetAvailablePositions(game.Board))
            {
                Rectangle rect = GetTileRectangle(position);
                if (rect.Contains(clickPoint))
                {
                    Debug.WriteLine($"Клик по позиции: {position}");

                    // Обрабатываем размещение фишки
                    HandleTilePlacement(position);
                    break;
                }
            }
        }

        private void HandleTilePlacement(int position)
        {
            // Получаем выбранную фишку игрока

            if (selectedPlayerTile != null && CanPlayTileAtPosition(selectedPlayerTile, position, out bool vertical, out bool flip))
            {
                selectedPlayerTile.IsVertical = vertical;
                if(flip)
                {
                    selectedPlayerTile.Flip(selectedPlayerTile);
                }

                game.Board.Insert(position, selectedPlayerTile);
                game.humanPlayer.Tiles.Remove(selectedPlayerTile);

                Debug.WriteLine($"Фишка размещена на позиции {position}");

                // Перерисовываем игровое поле
                UpdatePlayerTiles();
                UpdateBoard();

                ShowGameOverMessage(); 

                // Передаём ход компьютеру
                Game.isPlayerTurn = false;
                AImove();
                Debug.WriteLine("AImove вызван после хода игрока");
            }
            else
            {
                Debug.WriteLine($"Невозможно разместить фишку на позиции {position}");
                lblStatus.Text = "Ход невозможен.";
            }
        }

        private bool CanPlayTileAtPosition(Tile tile, int position, out bool vertical, out bool flip)
        {
            if (game.Board.Count == 0)
            {
                vertical = tile.IsDouble; ;
                flip = false;
                return true;
            }
            if (position == 0)
            {
                Tile firstTile = game.Board.First();
                vertical = tile.IsDouble;
                if(tile.Side1 == firstTile.Side1)
                {
                    flip = true;
                    return true;
                }
                if(tile.Side2 == firstTile.Side1)
                {
                    flip = false;
                    return true;
                }
            }
            else if (position == game.Board.Count)
            {
                Tile lastTile = game.Board.Last();
                vertical = tile.IsDouble;
                if (tile.Side2 == lastTile.Side2)
                {
                    flip = true;
                    return true;
                }
                if (tile.Side1 == lastTile.Side2)
                {
                    flip = false;
                    return true;
                }
            }
            else
            {
                if (!game.Board[position].IsVertical)
                {
                    Tile currentTile = game.Board[position - 1];
                    vertical = true;                    

                    if (tile.Side1 == currentTile.Side2)
                    {
                        flip = false;
                        if (tile.IsDouble)
                        {
                            tile.IsDouble = false;
                        }
                        return true;
                    }
                    if (tile.Side2 == currentTile.Side2)
                    {
                        flip = true;
                        if (tile.IsDouble)
                        {
                            tile.IsDouble = false;
                        }
                        return true;
                    }
                }
                if (!game.Board[position - 1].IsVertical)
                {
                    Tile currentTile = game.Board[position];
                    vertical = true;

                    if (tile.Side2 == currentTile.Side1)
                    {
                        flip = false;
                        if (tile.IsDouble)
                        {
                            tile.IsDouble = false;
                        }
                        return true;
                    }
                    if (tile.Side1 == currentTile.Side1)
                    {
                        flip = true;
                        if (tile.IsDouble)
                        {
                            tile.IsDouble = false;
                        }
                        return true;
                    }
                }
            }
            vertical = false;
            flip = false;
            return false;
        }


    }
}

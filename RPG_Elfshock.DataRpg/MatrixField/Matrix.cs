using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static RPG_Elfshock.Common.CharacterConstants;

namespace RpgData.MatrixField
{
    public class Matrix
    {
        private char[,] matrixField;

        private int[] playerPos;

        private char playerSymbol;

        public char[,] MatrixField
        {
            get { 
                return matrixField;
            }
            set { 
                if (value != null)
                {
                    matrixField = value;
                } else
                {
                    throw new InvalidOperationException(nameof(matrixField));
                }
            }
        }

        public int[] PlayerPos
        {
            get
            {
                return playerPos;
            }
            set
            {
                if (!IsPosArrayInvalid(value) || !IsPosEnemy(value))
                {
                    playerPos = value;
                } else
                {
                    throw new InvalidOperationException("Position is invalid.");
                }
            }
        }

        public IList<Enemy> EnemiesInMatrix { get; set; }

        public int EnemiesCount => EnemiesInMatrix.Count;

        public const int Size = 10;

        public const char EnemySymbol = '!';

        public const char MatrixSymbol = '-';

        public char PlayerSymbol
        {
            get
            {
                return this.playerSymbol;
            }
            set
            {
                if (pcEntitySymbol.Contains(value))
                {
                    this.playerSymbol = value;
                }
            }
        }

        public Matrix() {
            this.EnemiesInMatrix = new List<Enemy>();
            this.MatrixField = new char[Size, Size];
            this.PlayerPos = new int[2];
        }

        public void PlacePlayer(int[] startPlayerPos) { 
            this.PlayerPos = startPlayerPos;
            this.MatrixField[this.PlayerPos[0], this.PlayerPos[1]] = PlayerSymbol; 
        }

        public void GenerateEnemyPos()
        {
            Random rand = new Random();
            while (true)
            {
                Enemy enemy = new Enemy();
                int[] enemyCoords = new int[] { rand.Next(0, Size -  1), rand.Next(0, Size - 1) };

                if (!IsPosPlayer(enemyCoords) || !IsPosEnemy(enemyCoords))
                {
                    enemy.Pos = enemyCoords;  
                    this.EnemiesInMatrix.Add(enemy);
                    this.MatrixField[enemyCoords[0], enemyCoords[1]] = EnemySymbol;
                    break;
                }
            }
        }

        public string DrawField(char playerSymbol)
        {
            StringBuilder sb = new StringBuilder();

            for (int row = 0; row < this.MatrixField.GetLength(0); row++)
            {
                for (int col = 0; col < this.MatrixField.GetLength(1); col++)
                {
                    if (row == this.PlayerPos[0] && col == this.PlayerPos[1])
                    {
                        sb.Append(playerSymbol);
                    }
                    else if (this.MatrixField[row, col] == EnemySymbol)
                    {
                        sb.Append(EnemySymbol);
                    }
                    else
                    {
                        sb.Append(MatrixSymbol);
                    }
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }

        public int[] CalcPlayerPos(char direction)
        {
            int[] oldPlayerPos = this.PlayerPos;
            int row = oldPlayerPos[0];
            int col = oldPlayerPos[1];

            switch (direction)
            {
                case 'w': row--; break;
                case 's': row++; break;
                case 'd': col++; break;
                case 'a': col--; break;
                case 'e': row--; col++; break;
                case 'x': row++; col++; break;
                case 'q': row--; col--; break;
                case 'z': row++; col--; break;
            }

            int[] newPos = new int[] {row,col};

            this.PlayerPos = newPos;
            this.MatrixField[oldPlayerPos[0], oldPlayerPos[1]] = MatrixSymbol;
            this.MatrixField[newPos[0], newPos[1]] = this.PlayerSymbol;
            return newPos;
        }

        public int[] CalcEnemiesPos(int[] currEnemyCoords)
        {
            int[] oldCoordinates = new int[] { currEnemyCoords[0], currEnemyCoords[1] };
            int[] newCoordinates = new int[2];

            int row_diff = Math.Abs(this.PlayerPos[0] - currEnemyCoords[0]);
            int col_diff = Math.Abs(this.PlayerPos[1] - currEnemyCoords[1]);

            if (col_diff == Math.Max(row_diff, col_diff))
            {
                int newCol = oldCoordinates[1];
                if (playerPos[1] > col_diff)
                {
                    newCol++;
                }
                else
                {
                    newCol--;
                }

                newCoordinates[0] = currEnemyCoords[0];
                newCoordinates[1] = newCol;
            }
            else
            {
                int newRow = oldCoordinates[0];
                if (playerPos[0] > row_diff)
                {
                    newRow++;
                }
                else
                {
                    newRow--;
                }

                newCoordinates[0] = newRow;
                newCoordinates[1] = currEnemyCoords[1];

            }

            if (!IsPosArrayInvalid(newCoordinates) && !IsPosEnemy(newCoordinates))
            {
                UpdateEnemyPos(newCoordinates, oldCoordinates);
                return newCoordinates;
            }

            return oldCoordinates;
        }

        private void UpdateEnemyPos(int[] newCoords, int[] oldCoord)
        {
            this.MatrixField[oldCoord[0], oldCoord[1]] = MatrixSymbol;
            this.MatrixField[newCoords[0], newCoords[1]] = EnemySymbol;
        }

        public bool EntityInRange(int[] pos, int range) => Math.Abs(this.PlayerPos[0] - pos[0]) <= range && Math.Abs(this.PlayerPos[1] - pos[1]) <= range;

        public void DeleteEntityFromMatrix(Enemy enemy)
        {
            Console.WriteLine($"Killed enemy at position [{string.Join(",", enemy.Pos)}]!");
            this.MatrixField[enemy.Pos[0], enemy.Pos[1]] = MatrixSymbol;
            this.EnemiesInMatrix.Remove(enemy);
        }

        private bool IsPosArrayInvalid(int[] arr) => (arr[0] < 0 || arr[0] >= Size) || (arr[1] < 0 || arr[1] >= Size);

        private bool IsPosEnemy(int[] arr) => this.MatrixField[arr[0], arr[1]] == EnemySymbol;

        private bool IsPosPlayer(int[] arr) => this.MatrixField[arr[0], arr[1]] == PlayerSymbol;

    }
}

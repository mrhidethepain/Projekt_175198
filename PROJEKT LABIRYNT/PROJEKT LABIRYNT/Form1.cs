using System.Drawing;
using System.Windows.Forms;

namespace PROJEKT_LABIRYNT
{
    public partial class Form1 : Form
    {
        private Graph graph;
        private NodeG currentNode;
        private NodeG goalNode;
        private const int fieldSize = 8;
        private Button[,] field;
        private int suggestionCounter = 0;
        private int moveCounter = 0;

        public Form1()
        {
            InitializeComponent();
            Game();
            Instructions();
        }
        private void Game()
        {
            graph = new Graph();
            Labirynt(graph);
            currentNode = graph.GetNode(0, 0);
            goalNode = graph.GetNode(7, 7);

            field = new Button[fieldSize, fieldSize];
            for (int row = 0; row < fieldSize; row++)
            {
                for (int col = 0; col < fieldSize; col++)
                {
                    Button button = new Button();
                    button.Size = new Size(50, 50);
                    button.Location = new Point(50 * col, 50 * row);
                    button.BackColor = Color.White;
                    field[row, col] = button;
                    Controls.Add(button);
                }
            }
            updateField();
        }

        private void Labirynt(Graph graph)
        {
            for (int row = 0; row < fieldSize; row++)
            {
                for (int col = 0; col < fieldSize; col++)
                {
                    graph.AddNode(new NodeG(row, col));
                }
            }
            graph.AddEdge(0, 0, 0, 1);
            graph.AddEdge(0, 2, 0, 3);
            graph.AddEdge(0, 3, 0, 4);
            graph.AddEdge(0, 5, 0, 6);
            graph.AddEdge(0, 6, 0, 7);
            graph.AddEdge(1, 3, 1, 4);
            graph.AddEdge(1, 4, 1, 5);
            graph.AddEdge(1, 6, 1, 7);
            graph.AddEdge(2, 0, 2, 1);
            graph.AddEdge(2, 1, 2, 2);
            graph.AddEdge(2, 4, 2, 5);
            graph.AddEdge(2, 5, 2, 6);
            graph.AddEdge(2, 6, 2, 7);
            graph.AddEdge(3, 0, 3, 1);
            graph.AddEdge(3, 3, 3, 4);
            graph.AddEdge(3, 5, 3, 6);
            graph.AddEdge(4, 0, 4, 1);
            graph.AddEdge(4, 1, 4, 2);
            graph.AddEdge(4, 2, 4, 3);
            graph.AddEdge(4, 3, 4, 4);
            graph.AddEdge(4, 6, 4, 7);
            graph.AddEdge(5, 1, 5, 2);
            graph.AddEdge(5, 3, 5, 4);
            graph.AddEdge(5, 4, 5, 5);
            graph.AddEdge(5, 6, 5, 7);
            graph.AddEdge(6, 0, 6, 1);
            graph.AddEdge(6, 3, 6, 4);
            graph.AddEdge(6, 4, 6, 5);
            graph.AddEdge(6, 5, 6, 6);
            graph.AddEdge(7, 0, 7, 1);
            graph.AddEdge(7, 1, 7, 2);
            graph.AddEdge(7, 3, 7, 4);
            graph.AddEdge(7, 4, 7, 5);
            graph.AddEdge(7, 5, 7, 6);
            graph.AddEdge(7, 6, 7, 7);
            graph.AddEdge(0, 0, 1, 0);
            graph.AddEdge(1, 0, 2, 0);
            graph.AddEdge(3, 0, 4, 0);
            graph.AddEdge(4, 0, 5, 0);
            graph.AddEdge(5, 0, 6, 0);
            graph.AddEdge(6, 0, 7, 0);
            graph.AddEdge(0, 1, 1, 1);
            graph.AddEdge(2, 1, 3, 1);
            graph.AddEdge(0, 2, 1, 2);
            graph.AddEdge(2, 2, 3, 2);
            graph.AddEdge(4, 2, 5, 2);
            graph.AddEdge(5, 2, 6, 2);
            graph.AddEdge(6, 2, 7, 2);
            graph.AddEdge(0, 3, 1, 3);
            graph.AddEdge(1, 3, 2, 3);
            graph.AddEdge(2, 3, 3, 3);
            graph.AddEdge(3, 3, 4, 3);
            graph.AddEdge(6, 3, 7, 3);
            graph.AddEdge(1, 4, 2, 4);
            graph.AddEdge(3, 5, 4, 5);
            graph.AddEdge(4, 5, 5, 5);
            graph.AddEdge(0, 6, 1, 6);
            graph.AddEdge(2, 6, 3, 6);
            graph.AddEdge(3, 6, 4, 6);
            graph.AddEdge(4, 6, 5, 6);
            graph.AddEdge(5, 6, 6, 6);
            graph.AddEdge(1, 7, 2, 7);
            graph.AddEdge(2, 7, 3, 7);
            graph.AddEdge(5, 7, 6, 7);



        }

        private void updateField()
        {
            for (int row = 0; row < fieldSize; row++)
            {
                for (int col = 0; col < fieldSize; col++)
                {
                    var button = field[row, col];

                    if (currentNode.Row == row && currentNode.Col == col) {
                        button.BackColor = Color.Green;
                    }
                    else if (goalNode.Row==row&&goalNode.Col==col)
                    {
                        button.BackColor = Color.Blue;
                    }
                    else//if(button.BackColor != Color.Red)
                    {
                        button.BackColor = Color.White;
                    } 
                                
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.W)
            {
                moveTo(-1, 0);
            }
            if (keyData == Keys.A)
            {
                moveTo(0, -1);
            }     
            if (keyData == Keys.S)
            {
                moveTo(1, 0);
            }  
            if (keyData == Keys.D)
            {
                moveTo(0, 1);
            }   
            if (keyData == Keys.I) {
                Suggestion();
                    }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void moveTo(int dRow, int dCol)
        {
            moveCounter++;
            var nextNode = graph.GetNeighbor(currentNode, dRow, dCol);
            if (nextNode != null)
            {
                currentNode = nextNode;
                updateField();

                if (currentNode == goalNode)
                {
                    MessageBox.Show("Gratulacje! Dotarłeś do celu!\nLiczba ruchów: "+moveCounter+ "\nLiczba użytych podpowiedzi: " + suggestionCounter);
                }
            }
            else
            {
                int targetRow = currentNode.Row + dRow;
                int targetCol = currentNode.Col + dCol;

                if (targetRow >= 0 && targetRow < fieldSize && targetCol >= 0 && targetCol < fieldSize)
                {
                    field[targetRow, targetCol].BackColor = System.Drawing.Color.Red;
                }
                MessageBox.Show("Nie można tam przejść!");
            }
        }

        private void Suggestion()
        {
            suggestionCounter++;
            var shortestPath = graph.Dijkstra(currentNode, goalNode);
            if (shortestPath.Count > 1)
            {
                var nextNode = shortestPath[1];
                if (nextNode.Row > currentNode.Row)
                {
                    MessageBox.Show("Idź w dół");
                }
                else if (nextNode.Row < currentNode.Row)
                {
                    MessageBox.Show("Idź do góry");
                }
                else if(nextNode.Row == currentNode.Row)
                {
                    if (nextNode.Col > currentNode.Col)
                    {
                        MessageBox.Show("Idź w prawo");
                    }
                    else if (nextNode.Col < currentNode.Col)
                    {
                        MessageBox.Show("Idź w lewo");
                    }
                }
            }
            else
            {
                MessageBox.Show("Brak dostępnych podpowiedzi!", "Podpowiedź");
            }
        }
        private void Instructions()
        {
            Label instructionsLabel = new Label();
            instructionsLabel.Text = "Sterowanie:\nW S A D - poruszanie się\nI - podpowiedź";
            instructionsLabel.Font = new Font("Arial", 12);
            instructionsLabel.Location = new Point(420, 50);
            instructionsLabel.Size = new Size(200, 100);
            Controls.Add(instructionsLabel);
        }
    }
}


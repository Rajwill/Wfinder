﻿using static WordFinder.API.Helpers.DirectionWord;

namespace WordFinder.API.Helpers
{
    public class FinderHelper
    {

        private char[][] board;

        /// <summary>
        ///  constructor to initialize the object.
        /// </summary>
        /// <param name="matrix">List of strings to initialize the board</param>
        /// <exception cref="ArgumentException">If the matrix doesn't respect the defined rules</exception>
        /// <exception cref="ArgumentNullException">If the matrix is either null or empty.</exception>
        public FinderHelper(IEnumerable<string> matrix)
        {
            if (matrix == null || matrix.Count() == 0)
                throw new ArgumentNullException("matrix", "The board can't be null or empty");
            if (InputIsWrong(matrix))
                throw new ArgumentException("The value of the board exceed the maximum width or height 64*64");

            this.board = new char[matrix.Count()][];
            for (int i = 0; i < matrix.Count(); i++)
            {
                this.board[i] = new char[matrix.ElementAt(i).Length];
                int count = 0;
                foreach (var item in matrix.ElementAt(i))
                {
                    this.board[i][count] = item;
                    count++;
                }
            }
        }

        ~FinderHelper()
        {
            board = null;
        }

        public IEnumerable<string> Find(IEnumerable<string> stream)
        {
            var retval = new List<string>();
            foreach (var item in stream)
            {
                if (string.IsNullOrEmpty(item) || string.IsNullOrWhiteSpace(item))
                    continue;
                for (int i = 0; i < board.Length; i++)
                {
                    for (int j = 0; j < board[i].Length; j++)
                    {
                        if (board[i][j] == item[0] && FindWithDirection(i, j, 0, item))
                            retval.Add(item);
                    }
                }
            }

            //filter the most repeated words
            return retval.GroupBy(x => x)
                       .OrderByDescending(x => x.Count())
                       .Select(x => x.Key)
                       .Take(10);
        }

        /// <summary>
        /// This finder method it's responsible to iterate over the board in order to find the rest of the word.
        /// </summary>
        /// <param name="i">Current row position</param>
        /// <param name="j">Current column position</param>
        /// <param name="count">Number of found characters</param>
        /// <param name="word">Given word to search</param>
        /// <param name="direction">An instance of <seealso cref="Direction"/> which represents the direction to search.</param>
        /// <returns>True or False whether it found or not the word.</returns>
        private bool FindWithDirection(int i, int j, int count, string word, Direction direction = Direction.NONE)
        {
            if (count == word.Length)
                return true;

            if (i < 0 || i >= board.Length || j < 0 || j >= board[i].Length || board[i][j] != word[count])
                return false;

            char tmp = board[i][j];
            board[i][j] = ' ';
            var found = false;
            switch (direction)
            {
                case Direction.NONE:
                    found = FindWithDirection(i + 1, j, count + 1, word, Direction.DOWN)
                         || FindWithDirection(i - 1, j, count + 1, word, Direction.UP)
                         || FindWithDirection(i, j + 1, count + 1, word, Direction.RIGHT)
                         || FindWithDirection(i, j - 1, count + 1, word, Direction.LEFT);
                    break;
                case Direction.LEFT:
                    found = FindWithDirection(i, j - 1, count + 1, word, Direction.LEFT);
                    break;
                case Direction.RIGHT:
                    found = FindWithDirection(i, j + 1, count + 1, word, Direction.RIGHT);
                    break;
                case Direction.UP:
                    found = FindWithDirection(i - 1, j, count + 1, word, Direction.UP);
                    break;
                case Direction.DOWN:
                    found = FindWithDirection(i + 1, j, count + 1, word, Direction.DOWN);
                    break;
            }

            board[i][j] = tmp;

            return found;
        }

        /// <summary>
        /// This method validates that the input matrix it's correct. We need to validate it doesn't exceed the
        /// board's max width and height 64x64.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>True or False whether the input is correct or wrong.</returns>
        private bool InputIsWrong(IEnumerable<string> matrix)
        {
            return (matrix.Count() > 64 || matrix.Any(x => x.Length > 64));
        }
    }
}

using System.Collections.Generic;
using System.Linq;

namespace charl.test.stringExtensions.Extensions
{
    public static class BracketValidator
    {
        private static readonly Dictionary<char, char> Brackets = new Dictionary<char, char>()
        {
            { '(', ')' },
            { '[', ']' },
            { '{', '}' },
            { '<', '>' }
        };

        public static bool ValidateBrackets(this string input)
        {
            var openStack = new Stack<char>();

            foreach (var c in input)
            {
                if (Brackets.Keys.Contains(c))
                {
                    //Add if a bracket is opened.
                    openStack.Push(Brackets[c]);
                }

                //Continue loop if not on closing tag.
                if (!Brackets.Values.Contains(c)) continue;

                //If stack does not contain closing bracket it was never opened and should return false.
                if (!openStack.Contains(c))
                    return false;

                //If the stack is empty continue the loop.
                if (!openStack.Any())
                    continue;

                //If the stack is not empty and the expected bracket is on the stack pop it from the stack.
                if (openStack.Peek() == c)
                    openStack.Pop();
            }

            //If the stack is empty all open brackets have been closed and we can return true else false.
            return !openStack.Any();
        }
    }
}
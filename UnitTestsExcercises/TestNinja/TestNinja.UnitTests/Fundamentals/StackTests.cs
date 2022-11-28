using System;
using TestNinja.Fundamentals;
using Xunit;

namespace TestNinja.UnitTests.Fundamentals
{
    public class StackTests
    {

        [Fact]
        public void Push_PushOneItemToEmptyStack_StackCountEquals1()
        {
            Stack<int> stack = new Stack<int>();

            stack.Push(1);
            int result = stack.Count;

            Assert.Equal(1, result);
        }

        [Fact]
        public void Push_PushNull_ThrowArgumentNullException()
        {
            Stack<string> stack = new Stack<string>();

            Exception ex = Record.Exception(() => stack.Push(null));

            Assert.IsType<ArgumentNullException>(ex);
        }

        [Fact]
        public void Pop_PopsFromStackThatContains1Item_StackCountEquals0()
        {
            Stack<int> stack = new Stack<int>();
            stack.Push(1);

            stack.Pop();
            int result = stack.Count;

            Assert.Equal(0, result);
        }

        [Fact]
        public void Pop_PopsFromStackThatContains3Items_ReturnValueAtTheTop()
        {
            Stack<int> stack = new Stack<int>();

            stack.Push(0);
            stack.Push(2);
            stack.Push(4);

            
            int result = stack.Pop();

            Assert.Equal(4, result);
        }

        [Fact]
        public void Pop_PopsFromStackThatContains3Items_RemovesObjectFromStack()
        {
            Stack<int> stack = new Stack<int>();

            stack.Push(0);
            stack.Push(2);
            stack.Push(4);

            stack.Pop();
            int result = stack.Count;

            Assert.Equal(2, result);
        }

        [Fact]
        public void Pop_PopsFromEmptyStacks_ThrowInvalidOperationException()
        {
            Stack<int> stack = new Stack<int>();

            Exception ex = Record.Exception(() => stack.Pop());
            
            Assert.IsType<InvalidOperationException>(ex);
        }

        [Fact]
        public void Peek_PeeksFromStackThatContains1Item_StackCountEquals1()
        {
            Stack<int> stack = new Stack<int>();
            stack.Push(1);

            stack.Peek();
            int result = stack.Count;

            Assert.Equal(1, result);
        }

        [Fact]
        public void Peek_PeeksFromStackThatContains3Items_ReturnItemFromTop()
        {
            Stack<int> stack = new Stack<int>();

            stack.Push(0);
            stack.Push(2);
            stack.Push(4);

            
            int result = stack.Peek();

            Assert.Equal(4, result);
        }

        [Fact]
        public void Peek_PeeksFromEmptyStacks_ThrowInvalidOperationException()
        {
            Stack<int> stack = new Stack<int>();

            Exception ex = Record.Exception(() => stack.Peek());

            Assert.IsType<InvalidOperationException>(ex);
        }

        [Fact]
        public void Count_CountStackThatContains1Item_StackCountEquals1()
        {
            Stack<int> stack = new Stack<int>();
            stack.Push(1);

            int result = stack.Count;

            Assert.Equal(1, result);
        }

        [Fact]
        public void Count_EmptyStack_StackCountEquals0()
        {
            Stack<int> stack = new Stack<int>();

            int result = stack.Count;

            Assert.Equal(0, result);
        }


    }
}

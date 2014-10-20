using Combinator.Debugging;

namespace Combinator.Common
{
    /// <summary>
    /// ��������� ���������� ��� ��������
    /// </summary>
    public interface IArgumentsProvider
    {
        DebugInfo debugInfo { get; }
        
        object Pop();

        void Push(object obj);

        object Peek();
    }
}
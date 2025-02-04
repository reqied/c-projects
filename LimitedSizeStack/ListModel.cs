using System.Collections.Generic;

namespace LimitedSizeStack;

public interface ICommand
{
    void Undo();
}

public class AddCommand<TItem> : ICommand
{
    private ListModel<TItem> listModel;
    private TItem item;

    public AddCommand(ListModel<TItem> listModel, TItem item)
    {
        this.listModel = listModel;
        this.item = item;
    }

    public void Undo()
    {
        listModel.Items.Remove(item);
    }
}

public class RemoveCommand<TItem> : ICommand
{
    private ListModel<TItem> listModel;
    private TItem item;
    private int index;

    public RemoveCommand(ListModel<TItem> listModel, TItem item, int index)
    {
        this.listModel = listModel;
        this.item = item;
        this.index = index;
    }

    public void Undo()
    {
        listModel.Items.Insert(index, item);
    }
}

public class ListModel<TItem>
{
    public List<TItem> Items;
    public int UndoLimit;
    private LimitedSizeStack<ICommand> actionStack;

    public ListModel(int undoLimit) : this(new List<TItem>(), undoLimit)
    {
        Items = new List<TItem>(undoLimit);
    }
    public ListModel(List<TItem> items, int undoLimit)
    {
        Items = new List<TItem>(items);
        UndoLimit = undoLimit;
        actionStack = new LimitedSizeStack<ICommand>(undoLimit);
    }

    public void AddItem(TItem item)
    {
        Items.Add(item);
        actionStack.Push(new AddCommand<TItem>(this, item));
    }

    public void RemoveItem(int index)
    {
        if (index < 0 || index >= Items.Count) return;
        var item = Items[index];
        Items.RemoveAt(index);
        actionStack.Push(new RemoveCommand<TItem>(this, item, index));
    }

    public bool CanUndo()
    {
        return actionStack.Count > 0;
    }

    public void Undo()
    {
        if (CanUndo())
        {
            actionStack.Pop().Undo();
        }
    }
}
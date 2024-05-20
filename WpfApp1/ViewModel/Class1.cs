using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

public class LinearListViewModel<T> : INotifyPropertyChanged
{
    private readonly LinearList<T> _linearList;
    private T _newItem;
    private T _currentItem;

    public event PropertyChangedEventHandler PropertyChanged;

    public LinearListViewModel()
    {
        _linearList = new LinearList<T>();
        AddCommand = new RelayCommand(AddItem);
        RemoveCommand = new RelayCommand(RemoveItem, CanRemoveItem);
 
        MoveToBeginningCommand = new RelayCommand(MoveToBeginning);
    }

    public ObservableCollection<T> Items { get; } = new ObservableCollection<T>();

    public T NewItem
    {
        get => _newItem;
        set
        {
            _newItem = value;
            OnPropertyChanged();
        }
    }

    public T CurrentItem
    {
        get => _currentItem;
        set
        {
            _currentItem = value;
            OnPropertyChanged();
        }
    }

    public int Count => _linearList.Count;

    public ICommand AddCommand { get; }
    public ICommand RemoveCommand { get; }
    public ICommand MoveNextCommand { get; }
    public ICommand MoveToBeginningCommand { get; }

    private void AddItem()
    {
        _linearList.Add(NewItem);
        Items.Add(NewItem);
        NewItem = default;
        CurrentItem = _linearList.Current;
        OnPropertyChanged(nameof(Count));
    }

    private bool CanRemoveItem() => !_linearList.IsEmpty;

    private void RemoveItem()
    {
        if (_linearList.Remove())
        {
            Items.Remove(CurrentItem);
            CurrentItem = _linearList.IsEmpty ? default : _linearList.Current;
            OnPropertyChanged(nameof(Count));
        }
    }




    private void MoveNextItem()
    {
        if (_linearList.MoveToNext())
        {
            CurrentItem = _linearList.Current;
        }
    }

    private void MoveToBeginning()
    {
        _linearList.MoveToBeginning();
        CurrentItem = _linearList.Current;
    }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

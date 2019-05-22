using BoxesManger.DataStructers.Generics;
using System;

namespace BoxesManger.DataStructers
{
    public class BoxesManager
    {
        BST<BoxBase> _mainTree;
        MyLinkedList<BoxData> _boxesList;
        readonly int _maxBoxesNumber;
        readonly int _lowAmountAlert;
        readonly TimeSpan _period;
        INotifier _notifier { get; set; }

        public BoxesManager(INotifier noti = null, int MaxBoxes = 30, int MinBoxes = 5,
            int seconds = 10)
        {
            _boxesList = new MyLinkedList<BoxData>();
            _mainTree = new BST<BoxBase>();
            _maxBoxesNumber = MaxBoxes;
            _lowAmountAlert = MinBoxes;
            _period = new TimeSpan(0, 0, 0, seconds);
            _notifier = noti;
        }


        public void UpdateSupply(double x, double y, int amount)
        {
            if (x <= 0 || y <= 0 || amount <= 0)
            {
                _notifier.OnError("Invalid box size or amount");
            }
            else
            {
                if (amount > _maxBoxesNumber) amount = _maxBoxesNumber;
                BoxBase tmp = new BoxBase(x);
                if (_mainTree.Search(tmp, out BoxBase foundedBase))
                {
                    BoxHeight node = new BoxHeight(y);
                    if (foundedBase.HeightTree.Search(node, out BoxHeight foundedHeight))
                    {
                        //box exist
                        if (foundedHeight.Count + amount < _maxBoxesNumber)
                        {
                            foundedHeight.Count += amount;
                            _notifier.OnSuccess($"{amount} units of Box ({x},{y}) was added successfully");
                        }
                        else
                        {
                            foundedHeight.Count = _maxBoxesNumber;
                            _notifier.OnSuccess($"Box ({x},{y}) has reach the limit of {_maxBoxesNumber}");
                        }
                    }
                    else
                    {
                        _boxesList.AddLast(new BoxData(foundedBase));
                        foundedBase.HeightTree.Add(new BoxHeight(y, false, amount, _boxesList.End));
                        _boxesList.End.data.HeightData = foundedBase.HeightTree.LastUpdate.data;
                        _notifier.OnSuccess($"{amount} units of Box ({x},{y}) was added successfully");
                    }
                }
                else
                {
                    BoxBase node = new BoxBase(x, false);
                    _boxesList.AddLast(new BoxData(node));
                    node.HeightTree.Add(new BoxHeight(y, false, amount, _boxesList.End));
                    _boxesList.End.data.HeightData = node.HeightTree.LastUpdate.data;
                    _mainTree.Add(node);
                    _notifier.OnSuccess($"{amount} units of Box ({x},{y}) was added successfully");
                }
            }
        }

        public void GetBoxAmount(double x, double y, out int amount)
        {
            amount = 0;
            if (x <= 0 || y <= 0)
            {
                _notifier.OnError("Invalid box size");
            }
            else
            {
                if (_mainTree.Search(new BoxBase(x, true), out BoxBase found))
                {
                    if (found.HeightTree.Search(new BoxHeight(y), out BoxHeight foundHeight))
                    {
                        amount = foundHeight.Count;
                        _notifier.OnSuccess($"There are {amount} Boxes of size ({x},{y})");
                        return;
                    }
                }
                _notifier.OnError($"Boxes of size ({x},{y}) are not avialible");
            }
        }

        public bool GetGiftBox(double x, double y, out double xx, out double yy)
        {
            xx = 0;
            yy = 0;
            if (x <= 0 || y <= 0)
            {
                _notifier.OnError("Invalid box size");
                return false;
            }
            BoxBase val = new BoxBase(x, true);
            _mainTree.SearchSmallestValue(val, out BoxBase founded);
            while (founded != null)
            {
                if (founded.HeightTree.SearchSmallestValue(new BoxHeight(y), out BoxHeight foundedHeight))
                {
                    xx = founded.BaseSize;
                    yy = foundedHeight.Height;
                    foundedHeight.Count--;
                    if (foundedHeight.Count == 0)
                    {
                        _notifier.OnEmptyStock($"Boxes stock of ({founded.BaseSize},{foundedHeight.Height}) is over");
                        //delete from list.
                        _boxesList.RemoveNode(foundedHeight.DataNode, out MyLinkedList<BoxData>.Node save);
                        //delete from height tree
                        founded.HeightTree.Remove(foundedHeight);
                        if (founded.HeightTree.IsEmpty())
                            _mainTree.Remove(founded);
                        return true;
                    }
                    if (foundedHeight.Count < _lowAmountAlert)
                        _notifier.OnLowSuplly($"Boxes stock of ({founded.BaseSize},{foundedHeight.Height}) is low, please renew the supply");
                    //update time;
                    _boxesList.UpdateNode(foundedHeight.DataNode, dn =>
                    {
                        dn.data.LastUpdate = DateTime.Now;
                    });
                    return true;
                }
                if (_mainTree.SearchSmallestBigger(founded, out BoxBase newVal))
                    founded = newVal;
                else
                {
                    _notifier.OnError("Suited box not available");
                    return false;
                }
            }
            _notifier.OnError("Suited box not available");
            return false;
        }

        public void GetRidOfBoxes()
        {
            TimeSpan interval;
            DateTime now = DateTime.Now;
            if (_boxesList.Start == null)
            {
                _notifier.OnEmptyStock("Storage is Empty!");
                return;
            }
            interval = now - _boxesList.Start.data.LastUpdate;
            while (interval > _period)
            {
                _boxesList.Start.data.BaseData.HeightTree.Remove(_boxesList.Start.data.HeightData);
                if (_boxesList.Start.data.BaseData.HeightTree.IsEmpty()) _mainTree.Remove(_boxesList.Start.data.BaseData);
                _boxesList.RemoveFirst(out MyLinkedList<BoxData>.Node node);
                _notifier.OnBoxRemoval($"Last purchase of Box ({node.data.BaseData.BaseSize},{node.data.HeightData.Height}) " +
                    $"was on {node.data.LastUpdate}. Therefore, removed from stock");
                if (_boxesList.Start == null)
                {
                    _notifier.OnEmptyStock("Storage is Empty!");
                    return;
                }
                interval = now - _boxesList.Start.data.LastUpdate;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;

namespace MarketKing.Game.DataModels
{
    public class Statistics : ObservableCollection<PlayerStatistics>
    {
        private readonly Dispatcher _uiDispatcher;
        public Statistics(Dispatcher disapatcher)
        {
            _uiDispatcher = disapatcher;
        }


        new public void Add(PlayerStatistics player)
        {
            _uiDispatcher.Invoke(() => base.Add(player));
        }

        new public PlayerStatistics this[int playerId]
        {
            get
            {
                return this.SingleOrDefault(v => v.Id.Equals(playerId));
            }
        }

        new public void RemoveAt(int playerId)
        {
            _uiDispatcher.Invoke(() => Remove(this[playerId]));
        }
    }

    public class PlayerStatistics : ViewModelBase
    {
        private readonly int _id;
        public PlayerStatistics(int id)
        {
            _id = id;
        }

        public int Id
        {
            get { return _id; }
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _Hexagons;
        public int Hexagons
        {
            get { return _Hexagons; }
            set
            {
                if (_Hexagons != value)
                {
                    _Hexagons = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _Resources;
        public int Resources
        {
            get { return _Resources; }
            set
            {
                if (_Resources != value)
                {
                    _Resources = value;
                    OnPropertyChanged();
                }
            }
        }

        private Color _Color;
        public Color Color
        {
            get { return _Color; }
            set
            {
                if (_Color != value)
                {
                    _Color = value;
                    OnPropertyChanged("Color");
                }
            }
        }

        private int _BugCount;
        public int BugCount
        {
            get { return _BugCount; }
            set
            {
                if (_BugCount != value)
                {
                    _BugCount = value;
                    OnPropertyChanged("BugCount");
                }
            }
        }
    }
}

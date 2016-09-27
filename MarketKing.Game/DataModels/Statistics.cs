using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketKing.Game.DataModels
{
    public class Statistics : ObservableCollection<PlayerStatistics>
    {
        new public PlayerStatistics this[int playerId]
        {
            get
            {
                return this.SingleOrDefault(v => v.Id.Equals(playerId));
            }
        }

        new public void RemoveAt(int playerId)
        {
            Remove(this[playerId]);
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
    }
}

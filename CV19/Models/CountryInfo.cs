using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CV19.Models
{
    internal class CountryInfo : PlaceInfo
    {
        private Point? _Location;

        private IEnumerable<ConfirmedCount> _counts;

        public override Point Location
        {
            get
            {
                if (_Location != null)
                    return (Point)_Location;
                if (ProvincesCount is null) return default;

                var average_x = ProvincesCount.Average(p => p.Location.X);
                var average_y = ProvincesCount.Average(p => p.Location.Y);

                return (Point)(_Location = new Point(average_x, average_y));
            }
            set => _Location = value;
        }

        public IEnumerable<PlaceInfo> ProvincesCount { get; set; }

        public override IEnumerable<ConfirmedCount> Counts
        {
            get
            {
                if (_counts != null) return _counts;

                var pointsCount = ProvincesCount.FirstOrDefault()?.Counts.Count() ?? 0;
                if (pointsCount == 0) return Enumerable.Empty<ConfirmedCount>();

                var provincesPoints = ProvincesCount.Select(p => p.Counts.ToArray()).ToArray();

                var points = new ConfirmedCount[pointsCount];
                foreach (var provinces in provincesPoints)
                {
                    for (var i = 0; i < pointsCount; i++)
                    {
                        if (points[i].Date == default)
                        {
                            points[i] = provinces[i];
                        }
                        else
                        {
                            points[i].Count += provinces[i].Count;
                        }
                    }
                }

                return _counts = points;
            }
            set => _counts = value;
        }
    }
}
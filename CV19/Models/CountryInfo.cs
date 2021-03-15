using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CV19.Models
{
    internal class CountryInfo : PlaceInfo
    {
        private Point? _location;

        public override Point Location
        {
            get
            {
                if (_location != null)
                    return (Point)_location;

                if (Provinces is null) return default;

                var averageX = Provinces.Average(p => p.Location.X);
                var averageY = Provinces.Average(p => p.Location.Y);

                return (Point)(_location = new Point(averageX, averageY));
            }
            set => _location = value;
        }

        public IEnumerable<PlaceInfo> Provinces { get; set; }

        private IEnumerable<ConfirmedCount> _counts;

        public override IEnumerable<ConfirmedCount> Counts
        {
            get
            {
                if (_counts != null) return _counts;

                var pointsCount = Provinces.FirstOrDefault()?.Counts?.Count() ?? 0;
                if (pointsCount == 0) return Enumerable.Empty<ConfirmedCount>();

                var provincePoints = Provinces.Select(p => p.Counts.ToArray()).ToArray();

                var points = new ConfirmedCount[pointsCount];
                foreach (var province in provincePoints)
                    for (var i = 0; i < pointsCount; i++)
                    {
                        if (points[i].Date == default)
                            points[i] = province[i];
                        else
                            points[i].Count += province[i].Count;
                    }

                return _counts = points;
            }
            set => _counts = value;
        }
    }
}
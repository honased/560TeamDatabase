using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel;

namespace NetflixData.Models
{
    public class PaginatedShows : INotifyPropertyChanged
    {
        private IReadOnlyList<Show> _shows;

        private const int PAGE_ITEM_COUNT = 100;

        private int page;

        public event PropertyChangedEventHandler PropertyChanged;

        public PaginatedShows(IReadOnlyList<Show> shows)
        {
            _shows = shows;
            page = 0;
        }

        public int PageCount => Math.Max((_shows.Count / PAGE_ITEM_COUNT), 1);
        public string PageCountDisplay => $"/ {PageCount}";

        public string PageDisplay => $"{Page}";

        public int Page
        {
            get => page + 1;
            set
            {
                int oldPage = page;
                page = Math.Clamp(value - 1, 0, PageCount - 1);
                if(oldPage != page) PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Shows"));
            }
        }

        public IReadOnlyList<Show> Shows
        {
            get
            {
                int count = Math.Min(_shows.Count - (page * (PAGE_ITEM_COUNT)), PAGE_ITEM_COUNT);
                if (count == 0) return _shows;
                var arr = new ReadOnlySpan<Show>(_shows.ToArray(), page * PAGE_ITEM_COUNT, count).ToArray();
                return arr;
            }
        }
    }
}

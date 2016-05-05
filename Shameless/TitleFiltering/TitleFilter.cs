namespace Shameless.TitleFiltering
{
	using System.Collections.Generic;
	using System.Linq;

	using Shameless.Tickets;

	public class TitleFilter
	{
		public TitleFilter(
			Dictionary<string, bool> regionFilter, 
			Dictionary<string, bool> typeFilter, 
			Dictionary<string, bool> otherFilter)
		{
			this.RegionFilter = regionFilter;
			this.TypeFilter = typeFilter;
			this.OtherFilter = otherFilter;
		}

		public TitleFilter()
		{
			var defaultRegions = new Dictionary<string, bool>
									{
										{ "ALL", true }, 
										{ "EUR", true }, 
										{ "USA", true }, 
										{ "JPN", true }
									};

			var defaultTypes = new Dictionary<string, bool>
									{
										{ "eShop", true }, 
										{ "System", false }, 
										{ "Demo", true }, 
										{ "DLC", true }, 
										{ "Update", true }, 
										{ "DSIWare", true }
									};

			var defaultOther = new Dictionary<string, bool> { { "Unknown", false } };

			this.RegionFilter = defaultRegions;
			this.TypeFilter = defaultTypes;
			this.OtherFilter = defaultOther;
		}

		public Dictionary<string, bool> RegionFilter { get; }

		public Dictionary<string, bool> TypeFilter { get; }

		public Dictionary<string, bool> OtherFilter { get; }

		public static Nintendo3DSTitle[] FilterTitles(Nintendo3DSTitle[] titles, TitleFilter filter)
		{
			var includedRegions = filter.RegionFilter.Where(a => a.Value).ToDictionary(a => a.Key, a => a.Value);
			var includedTypes = filter.TypeFilter.Where(a => a.Value).ToDictionary(a => a.Key, a => a.Value);

			var shouldAddUnknown = filter.OtherFilter["Unknown"];

			var filtered = new List<Nintendo3DSTitle>();

			foreach (var title in titles)
			{
				var validType = includedTypes.Any(pair => title.Type.Contains(pair.Key));

				if (shouldAddUnknown && title.Name == "Unknown")
				{
					if (validType)
					{
						filtered.Add(title);
					}
				}
				else
				{
					var validRegion = includedRegions.Where(pair => pair.Value).Any(pair => title.Region == pair.Key);
					if (validRegion && validType)
					{
						filtered.Add(title);
					}
				}
			}

			return filtered.ToArray();
		}

		public override bool Equals(object other)
		{
			var otherFilter = other as TitleFilter;

			if (otherFilter != null)
			{
				var regionsEqual = this.RegionFilter.SequenceEqual(otherFilter.RegionFilter);
				var typesEqual = this.TypeFilter.SequenceEqual(otherFilter.TypeFilter);
				var othersEqual = this.OtherFilter.SequenceEqual(otherFilter.OtherFilter);

				return regionsEqual && typesEqual && othersEqual;
			}

			return base.Equals(other);
		}

		protected bool Equals(TitleFilter other)
		{
			return Equals(this.RegionFilter, other.RegionFilter) && Equals(this.TypeFilter, other.TypeFilter) && Equals(this.OtherFilter, other.OtherFilter);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = (this.RegionFilter != null ? this.RegionFilter.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (this.TypeFilter != null ? this.TypeFilter.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (this.OtherFilter != null ? this.OtherFilter.GetHashCode() : 0);
				return hashCode;
			}
		}

		public TitleFilter Clone()
		{
			var newRegionFilter = this.RegionFilter.ToDictionary(e => e.Key, e => e.Value);
			var newTypeFilter = this.TypeFilter.ToDictionary(e => e.Key, e => e.Value);
			var newOtherFilter = this.OtherFilter.ToDictionary(e => e.Key, e => e.Value);

			var newFilter = new TitleFilter(newRegionFilter, newTypeFilter, newOtherFilter);
			return newFilter;
		}
	}
}
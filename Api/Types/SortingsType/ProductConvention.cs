using System;
using HotChocolate.Data.Sorting;

namespace Api.Types.SortingsType
{
	public class ProductConvention : SortConvention
	{
	
        protected override void Configure(ISortConventionDescriptor descriptor)
        {
            descriptor.AddDefaults().DefaultBinding<AscOnlySortEnumType>();
        }

    }
}


using System;
using System.Security.Cryptography.X509Certificates;
using Core.Entities;

namespace Core.Specifications;

public class ProductSpecification : BaseSpecification<Product>
{
	public ProductSpecification(ProductSpecParams specparams)
		: base(x =>
		(string.IsNullOrEmpty(specparams.Search) || x.Name.ToLower().Contains(specparams.Search)) &&
			(!specparams.Brands.Any() || specparams.Brands.Contains(x.Brand)) &&
			(!specparams.Types.Any() || specparams.Types.Contains(x.Type))
		)
	{


		ApplyPaging(specparams.pagesize * (specparams.PageIndex - 1), specparams.pagesize);



		switch (specparams.Sort)
		{
			case "priceAsc":
				AddOrderBy(x => x.Price);
				break;
			case "priceDesc":
				AddOrderByDescending(x => x.Price);
				break;
			default:
				AddOrderBy(x => x.Name);
				break;
		}

	}
}

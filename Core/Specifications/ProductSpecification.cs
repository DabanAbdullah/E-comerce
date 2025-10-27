using System;
using System.Security.Cryptography.X509Certificates;
using Core.Entities;

namespace Core.Specifications;

public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification(ProductSpecParams specparams) : base
    (x => (specparams.Brands.Count == 0 || specparams.Brands.Contains(x.Brand)) &&
    (specparams.Types.Count == 0 || specparams.Types.Contains(x.Type)))
    {

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

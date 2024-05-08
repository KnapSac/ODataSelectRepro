#region

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

#endregion

namespace ODataSelectRepro;

public class CustomersController : ODataController
{
	private static readonly List< Customer > s_Customers =
	[
		..Enumerable.Range(
			1,
			3 ).Select(
			idx => new Customer
			       {
				       Id = idx,
				       Name = $"Customer {idx}",
			       } )
	];

	public ActionResult< IQueryable > Get(
		ODataQueryOptions< Customer > queryOptions )
	{
		IQueryable< Customer > values = s_Customers.AsQueryable( );

		return Ok(
			queryOptions.SelectExpand.ApplyTo(
				values,
				new ODataQuerySettings( ) ) );
	}
}
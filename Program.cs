#region

using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.Json;
using Microsoft.OData.ModelBuilder;

#endregion

namespace ODataSelectRepro;

public class Customer
{
	public int Id { get; set; }
	public string Name { get; set; }
}

static class Program
{
	private static void Main( )
	{
		WebApplicationBuilder builder = WebApplication.CreateBuilder( );

		builder.Services.AddControllers( ).AddOData(
			options => { options.Select( ).AddRouteComponents( GetEdmModel( ) ); } );

		WebApplication app = builder.Build( );

		app.UseRouting( );
		app.MapControllers( );

		app.Run( );
	}

	private static IEdmModel GetEdmModel( )
	{
		ODataConventionModelBuilder builder = new( );
		builder.EntitySet< Customer >( "Customers" );
		return builder.GetEdmModel( );
	}
}
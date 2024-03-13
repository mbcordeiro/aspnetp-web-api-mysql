using APICatalog.Repositories;
using GraphQL;
using GraphQL.Types;

namespace APICatalog.GraphQL
{
    public class CategoryQuery : ObjectGraphType
    {
        public CategoryQuery(IUnitOfWork _context)
        {
            Field<CategoryType>("category",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType>() { Name = "id" }),
                    resolve: context =>
                    {
                        var id = context.GetArgument<int>("id");
                        return _context.CategoryRepostory
                                       .GetById(c => c.CategoryId == id);
                    });
            Field<ListGraphType<CategoryType>>("categories",
                resolve: context =>
                {
                    return _context.CategoryRepostory.Get();
                });
        }
    }
}

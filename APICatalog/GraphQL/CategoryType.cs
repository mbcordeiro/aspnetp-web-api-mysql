using APICatalog.Models;
using GraphQL.Types;

namespace APICatalog.GraphQL
{
    public class CategoryType : ObjectGraphType<Category>
    {
        public CategoryType()
        {
            Field(x => x.CategoryId);
            Field(x => x.Name);
            Field(x => x.ImageUrl);
            Field<ListGraphType<CategoryType>>("categories");
        }
    }
}

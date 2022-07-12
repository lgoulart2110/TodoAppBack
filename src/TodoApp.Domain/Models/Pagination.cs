using TodoApp.Domain._Common.Params;

namespace TodoApp.Domain.Models
{
    public class Pagination : IPaginable
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }

        public Pagination()
        {
            Take = 100;
        }
    }
}
